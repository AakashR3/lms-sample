#region usings
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.NLogTarget;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Graph.ExternalConnectors;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Config;
using NLog.Web;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Tata.IGetIT.Learner.Repository;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Implementation;
using Tata.IGetIT.Learner.Repository.Implementation.AccountManagement;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Interface.AccountManagement;
using Tata.IGetIT.Learner.Repository.Models.Configurations;
using Tata.IGetIT.Learner.Service;
using Tata.IGetIT.Learner.Service.Implementation;
using Tata.IGetIT.Learner.Service.Implementation.AccountManagement;
using Tata.IGetIT.Learner.Service.Interfaces;
using Tata.IGetIT.Learner.Service.Interfaces.AccountManagement;
using LogLevel = NLog.LogLevel;
#endregion

namespace Tata.IGetIT.Learner.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {


            Func<string> CombinedLogLevels = () =>
           {
               return $"{LoggingConstants.LOG_LEVEL_TRACE},{LoggingConstants.LOG_LEVEL_DEBUG},{LoggingConstants.LOG_LEVEL_INFO},{LoggingConstants.LOG_LEVEL_WARN},{LoggingConstants.LOG_LEVEL_ERROR},{LoggingConstants.LOG_LEVEL_FATAL}";
           };

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            var ApplicationsInsightInsKey = Environment.GetEnvironmentVariable(EnvironmentConstants.ENV_VAR_AZ_AI_INS_KEY);

            if (ApplicationsInsightInsKey.IsNullOrEmptyOrWhiteSpace())
            {
                var aiTarget = LogManager.Configuration.FindRuleByName("aiTarget");
                LogManager.Configuration.LoggingRules.Remove(aiTarget);

            }
            else
            {
                var filelogTarget = LogManager.Configuration.FindRuleByName("filelogTarget");
                LogManager.Configuration.LoggingRules.Remove(filelogTarget);
            }

            LogManager.Configuration.Reload();

            var AppName = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");
            NLog.LogManager.Configuration.Variables["appName"] = AppName.IsNullOrEmptyOrWhiteSpace() ? "localhost" : AppName;

            NLog.LogManager.Configuration.Variables["logLevels"] = CombinedLogLevels();
            NLog.LogManager.ReconfigExistingLoggers();

            logger.LogDetails(new LogDetails(LogType.Info) { Message = "Application Start" });
            try
            {
                if (!ApplicationsInsightInsKey.IsNullOrEmptyOrWhiteSpace())
                {
                    logger.LogDetails(new LogDetails(LogType.Info) { Message = "Streaming logs to Application Insights" });
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Warn) { Message = "Application Insight key is not provided, log target changed to file" });
                }
                var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

                builder.Host.UseNLog();
                builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
                    jsonOptions.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

                //builder.Services.AddControllers().AddXmlSerializerFormatters();

                builder.Services.AddEndpointsApiExplorer();
                var azConnection = Environment.GetEnvironmentVariable(EnvironmentConstants.ENV_VAR_AZ_CONFIG_CS);
                SecretClient secretClient = null;
                var Configuration = builder.Configuration;
                string KeyVaultUrl = string.Empty;
                string LogLevels = string.Empty;
                ClientSecretCredential keyvaultCredentials = null;
                if (!string.IsNullOrEmpty(azConnection))
                {
                    builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var settings = config.Build();

                        config.AddAzureAppConfiguration(options =>
                            options
                                .Connect(azConnection)
                                .Select(KeyFilter.Any, LabelFilter.Null)
                                .Select(KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName)
                        );
                        KeyVaultUrl = Configuration.GetValue<string>(ConfigurationConstants.KEY_VAULT_URL);
                        LogLevels = Configuration.GetValue<string>(ConfigurationConstants.APP_LOG_LEVELS);

                        //Used only in this scope
                        string tenantId = Configuration.GetValue<string>(ConfigurationConstants.APP_TENANT_ID);
                        string clientId = Configuration.GetValue<string>(ConfigurationConstants.APP_CLIENT_ID);
                        string clientSec = Environment.GetEnvironmentVariable(EnvironmentConstants.ENV_VAR_AZ_KEYVAULT_SEC);

                        if (!KeyVaultUrl.IsNullOrEmptyOrWhiteSpace()
                        && !tenantId.IsNullOrEmptyOrWhiteSpace()
                        && !clientId.IsNullOrEmptyOrWhiteSpace()
                        && !clientSec.IsNullOrEmptyOrWhiteSpace()
                        )
                        {
                            logger.LogDetails(new LogDetails(LogType.Info) { Message = "Key vault enabled, authorizing key vault" });
                            keyvaultCredentials = new ClientSecretCredential(tenantId, clientId, clientSec);
                            secretClient = new SecretClient(new Uri(KeyVaultUrl), keyvaultCredentials);
                            config.AddAzureKeyVault(secretClient, new AzureKeyVaultConfigurationOptions());
                            logger.LogDetails(new LogDetails(LogType.Info) { Message = "Key vault authorization successful" });
                        }

                    });
                }

                string AllowedOrigins = Configuration.GetValue<string>(ConfigurationConstants.APP_ALLOWED_ORIGINS);

                if (!string.IsNullOrEmpty(AllowedOrigins))
                {
                    builder.Services.AddCors(options =>
                    {
                        options.AddPolicy(name: "AllowOrigin",
                            builder =>
                            {
                                builder.WithOrigins(AllowedOrigins.Split(';'))
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod();
                            });
                    });
                }


                builder.Services.Configure<ApiBehaviorOptions>(x =>
                {
                    x.SuppressModelStateInvalidFilter = true;
                });


                if (Configuration.GetSection(ConfigurationConstants.DB_CONFIG).Get<DBConfig>() == null)
                {
                    throw new ArgumentNullException(ConfigurationConstants.DB_CONFIG, "Configuration not found exception");
                }

                if (Configuration.GetSection(ConfigurationConstants.JWT_TOKEN).Get<JwtTokenConfig>() == null)
                {
                    throw new ArgumentNullException(ConfigurationConstants.JWT_TOKEN, "Configuration not found exception");
                }

                if (Configuration.GetSection(ConfigurationConstants.SMTP_CONFIG).Get<SmtpConfig>() == null)
                {
                    throw new ArgumentNullException(ConfigurationConstants.SMTP_CONFIG, "Configuration not found exception");
                }

                if (Configuration.GetSection(ConfigurationConstants.SOCIAL_LOGIN_LINKEDIN_CONFIG).Get<SocialLoginConfig>() == null)
                {
                    throw new ArgumentNullException(ConfigurationConstants.SOCIAL_LOGIN_LINKEDIN_CONFIG, "Configuration not found exception");
                }

                if (Configuration.GetSection(ConfigurationConstants.FORGOT_PASSWORD_CONFIG).Get<ForgotPasswordConfig>() == null)
                {
                    throw new ArgumentNullException(ConfigurationConstants.FORGOT_PASSWORD_CONFIG, "Configuration not found exception");
                }

                if (Configuration.GetSection(ConfigurationConstants.SSO_CONFIG).Get<SSOConfig>() == null)
                {
                    throw new ArgumentNullException(ConfigurationConstants.SSO_CONFIG, "Configuration not found exception");
                }

                if (Configuration.GetSection(ConfigurationConstants.RAZORPAY_CONFIG).Get<PaymentConfig>() == null)
                {
                    throw new ArgumentNullException(ConfigurationConstants.RAZORPAY_CONFIG, "Configuration not found exception");
                }

                if (Configuration.GetSection(ConfigurationConstants.RECURLY_CONFIG).Get<PaymentConfig>() == null)
                {
                    throw new ArgumentNullException(ConfigurationConstants.RECURLY_CONFIG, "Configuration not found exception");
                }

                if (Configuration.GetSection(ConfigurationConstants.AZURE_MAP_CONFIG).Get<GeoLocationConfig>() == null)
                {
                    throw new ArgumentNullException(ConfigurationConstants.AZURE_MAP_CONFIG, "Configuration not found exception");
                }

                if (Configuration.GetSection(ConfigurationConstants.DISCUSSION_FORUM_CONFIG).Get<ForumConfig>() == null)
                {
                    throw new ArgumentNullException(ConfigurationConstants.DISCUSSION_FORUM_CONFIG, "Configuration not found exception");
                }


                builder.Services.Configure<DBConfig>(Configuration.GetSection(ConfigurationConstants.DB_CONFIG));
                builder.Services.Configure<JwtTokenConfig>(Configuration.GetSection(ConfigurationConstants.JWT_TOKEN));

                builder.Services.Configure<SmtpConfig>(Configuration.GetSection(ConfigurationConstants.SMTP_CONFIG));
                builder.Services.Configure<SocialLoginConfig>(Configuration.GetSection(ConfigurationConstants.SOCIAL_LOGIN_LINKEDIN_CONFIG));

                var jwtConfig = Configuration.GetSection(ConfigurationConstants.JWT_TOKEN).Get<JwtTokenConfig>();

                builder.Services.Configure<ForgotPasswordConfig>(Configuration.GetSection(ConfigurationConstants.FORGOT_PASSWORD_CONFIG));
                builder.Services.Configure<SSOConfig>(Configuration.GetSection(ConfigurationConstants.SSO_CONFIG));
                builder.Services.Configure<PaymentConfig>(Configuration.GetSection(ConfigurationConstants.RAZORPAY_CONFIG));

                builder.Services.Configure<PaymentConfig>(Configuration.GetSection(ConfigurationConstants.RECURLY_CONFIG));

                builder.Services.Configure<GeoLocationConfig>(Configuration.GetSection(ConfigurationConstants.AZURE_MAP_CONFIG));

                builder.Services.Configure<ForumConfig>(Configuration.GetSection(ConfigurationConstants.DISCUSSION_FORUM_CONFIG));

                if (!string.IsNullOrEmpty(KeyVaultUrl))
                {
                    //Configure the secrets
                    var vaultResponse = secretClient.GetSecret(KeyVaultConstants.DB_PASSWORD);
                    if (vaultResponse.Value != null)
                    {

                        string Password = vaultResponse.Value.Value;
                        builder.Services.Configure<DBConfig>(x =>
                        {
                            x.Password = Password;
                        });
                        // builder.Services.Configure<DBConfig>(x =>
                        // {
                        //     x.Password = vaultResponse.Value.Value;
                        //     Console.WriteLine(x.ActualConnectionString);
                        // });
                    }

                    // vaultResponse = secretClient.GetSecret(KeyVaultConstants.SMTP_PASSWORD);
                    // if (vaultResponse.Value != null)
                    // {
                    //     builder.Services.Configure<SmtpConfig>(x =>
                    //     {
                    //         x.SmtpPass = vaultResponse.Value.Value;
                    //     });
                    // }

                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.SMTP_PASSWORD);
                    if (vaultResponse.Value != null)
                    {
                        var vaultVaulue = vaultResponse.Value.Value;
                        builder.Services.Configure<SmtpConfig>(x =>
                        {
                            x.SmtpPass = vaultVaulue;
                        });
                    }


                    // vaultResponse = secretClient.GetSecret(KeyVaultConstants.JWT_KEY);
                    // if (vaultResponse.Value != null)
                    // {
                    //     builder.Services.Configure<JwtTokenConfig>(x =>
                    //     {
                    //         x.Key = vaultResponse.Value.Value;
                    //     });
                    // }

                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.JWT_KEY);
                    if (vaultResponse.Value != null)
                    {
                        var vaultValue = vaultResponse.Value.Value;
                        builder.Services.Configure<JwtTokenConfig>(x =>
                        {
                            x.Key = vaultValue;
                        });
                    }


                    // vaultResponse = secretClient.GetSecret(KeyVaultConstants.LINKEDIN_API_KEY);
                    // if (vaultResponse.Value != null)
                    // {
                    //     builder.Services.Configure<SocialLoginConfig>(x =>
                    //     {
                    //         x.ApiKey = vaultResponse.Value.Value;
                    //     });
                    // }

                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.LINKEDIN_API_KEY);
                    if (vaultResponse.Value != null)
                    {
                        var vaultValue = vaultResponse.Value.Value;
                        builder.Services.Configure<SocialLoginConfig>(x =>
                        {
                            x.ApiKey = vaultValue;
                        });
                    }

                    // vaultResponse = secretClient.GetSecret(KeyVaultConstants.LINKEDIN_API_SECRET);
                    // if (vaultResponse.Value != null)
                    // {
                    //     builder.Services.Configure<SocialLoginConfig>(x =>
                    //     {
                    //         x.ApiSecret = vaultResponse.Value.Value;
                    //     });
                    // }

                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.LINKEDIN_API_SECRET);
                    if (vaultResponse.Value != null)
                    {
                        var vaultValue = vaultResponse.Value.Value;
                        builder.Services.Configure<SocialLoginConfig>(x =>
                        {
                            x.ApiSecret = vaultValue;
                        });
                    }



                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.RAZORPAY_API_KEY);
                    if (vaultResponse.Value != null)
                    {
                        var vaultValue = vaultResponse.Value.Value;
                        builder.Services.Configure<PaymentConfig>(x =>
                        {
                            x.rzp_apiKey = vaultValue;
                        });
                    }

                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.RAZORPAY_API_SECRET);
                    if (vaultResponse.Value != null)
                    {
                        var vaultValue = vaultResponse.Value.Value;
                        builder.Services.Configure<PaymentConfig>(x =>
                        {
                            x.rzp_apiSecret = vaultValue;
                        });
                    }

                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.RAZORPAY_WEBHOOK_SECRET);
                    if (vaultResponse.Value != null)
                    {
                        var vaultValue = vaultResponse.Value.Value;
                        builder.Services.Configure<PaymentConfig>(x =>
                        {
                            x.rzp_webHookSecret = vaultValue;
                        });
                    }


                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.RECURLY_API_KEY);
                    if (vaultResponse.Value != null)
                    {
                        var vaultValue = vaultResponse.Value.Value;
                        builder.Services.Configure<RecurlyConfig>(x =>
                        {
                            x.ApiKey = vaultValue;
                        });
                    }

                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.AZURE_MAP_KEY);
                    if (vaultResponse.Value != null)
                    {
                        var vaultValue = vaultResponse.Value.Value;
                        builder.Services.Configure<GeoLocationConfig>(x =>
                        {
                            x.AzureMapKey = vaultValue;
                        });
                    }


                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.Recurly_Webhook_Username);
                    if (vaultResponse.Value != null)
                    {
                        var vaultValue = vaultResponse.Value.Value;
                        builder.Services.Configure<PaymentConfig>(x =>
                        {
                            x.recurly_Webhook_Username = vaultValue;
                        });
                    }

                    vaultResponse = secretClient.GetSecret(KeyVaultConstants.Recurly_Webhook_Password);
                    if (vaultResponse.Value != null)
                    {
                        var vaultValue = vaultResponse.Value.Value;
                        builder.Services.Configure<PaymentConfig>(x =>
                        {
                            x.recurly_Webhook_Password = vaultValue;
                        });
                    }


                    logger.LogDetails(new LogDetails(LogType.Info) { Message = "Getting the secrets from the vault is complete." });
                }

                if (!LogLevels.IsNullOrEmptyOrWhiteSpace())
                {
                    NLog.LogManager.Configuration.Variables["logLevels"] = LogLevels;
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Warn) { Message = "Loglevel not provided, log levels set to default : Trace to Fatal" });
                }

                NLog.LogManager.ReconfigExistingLoggers();


                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = jwtConfig.Audience,
                        ValidIssuer = jwtConfig.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
                    };
                });
                var scopes = new[] { "https://graph.microsoft.com/.default" };

                // TODO: TEMP CREDENDTIAL CONFIG FOR TESTING, SHOULD BE REMOVED.
                var tenantId = "d89a7ac3-a7fb-456b-ad89-7fcb9250801c";
                var clientId = "3dc05c6e-e95c-4845-8895-c66aea79f737";
                var clientSec = "jhN8Q~Ev-MSLdUr4bINfflRK3G5_cIvTEEnnXcin";
                //var adminUserId = "89c802cc-1b42-426c-9a7a-4cb33fd5ab1b";
                var adminUserId = "1ab7d0dd-2cd4-48a3-929b-8380f4c6f310";

                var options = new TokenCredentialOptions
                {
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
                };

                var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSec, options);
                var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

                var authority = $"https://login.microsoftonline.com/{tenantId}/v2.0";
                var cca = ConfidentialClientApplicationBuilder.Create(clientId).WithAuthority(authority).WithClientSecret(clientSec).Build();
                var microsoftAuthService = new MicrosoftAuthService(cca, scopes.ToArray());

                // TODO: Uncomment once tata env setup done.
                //var graphClient = new GraphServiceClient(keyvaultCredentials, scopes);




                var emailConfig = Configuration
                .Get<SmtpConfig>();
                builder.Services.AddSingleton(emailConfig);
                builder.Services.AddScoped<IEmailSender, EmailSender>();

                builder.Services.Configure<FormOptions>(o =>
                {
                    o.ValueLengthLimit = int.MaxValue;
                    o.MultipartBodyLengthLimit = int.MaxValue;
                    o.MemoryBufferThreshold = int.MaxValue;
                });

                builder.Services.AddControllers();
                builder.Services.AddSingleton<IDatabaseManager, DatabaseManager>();
                builder.Services.AddTransient<IAuthService, AuthService>();
                builder.Services.AddTransient<IUserService, UserService>();
                builder.Services.AddTransient<IAuthRepo, AuthRepo>();
                builder.Services.AddTransient<IUserRepo, UserRepo>();
                builder.Services.AddSingleton<ICommunicationService, EmailService>();
                builder.Services.AddTransient<IDashboardService, DashboardService>();
                builder.Services.AddTransient<IDashboardRepo, DashboardRepo>();
                builder.Services.AddTransient<IHeadersAndMenusService, HeadersAndMenusService>();
                builder.Services.AddTransient<IHeadersAndMenusRepo, HeadersAndMenusRepo>();
                builder.Services.AddTransient<IHeroSectionService, HeroSectionService>();
                builder.Services.AddTransient<IHeroSectionRepo, HeroSectionRepo>();
                builder.Services.AddTransient<ILearningPathsService, LearningPathsService>();
                builder.Services.AddTransient<ILearningPathsRepo, LearningPathsRepo>();
                builder.Services.AddTransient<IHeroSectionRepo, HeroSectionRepo>();
                builder.Services.AddTransient<IPaymentService, PaymentService>();
                builder.Services.AddTransient<IPaymentRepo, PaymentRepo>();
                builder.Services.AddTransient<ICartService, CartService>();
                builder.Services.AddTransient<ICartRepo, CartRepo>();
                builder.Services.AddTransient<ISubscriptionsService, SubscriptionsService>();
                builder.Services.AddTransient<ISubscriptionsRepo, SubscriptionsRepo>();
                builder.Services.AddTransient<IAdminService, AdminService>();
                builder.Services.AddTransient<IAdminRepo, AdminRepo>();
                builder.Services.AddTransient<ICourseCatalogService, CourseCatalogService>();
                builder.Services.AddTransient<ICourseCatalogRepo, CourseCatalogRepo>();
                builder.Services.AddTransient<ICouponsService, CouponsService>();
                builder.Services.AddTransient<IQuickStartService, QuickStartService>();
                builder.Services.AddTransient<IQuickStartRepo, QuickStartRepo>();
                builder.Services.AddTransient<ITranscriptService, TranscriptService>();
                builder.Services.AddTransient<ITranscriptRepo, TranscriptRepo>();
                builder.Services.AddTransient<ICouponsService, CouponsService>();
                builder.Services.AddTransient<IWebhookService, WebhookService>();
                builder.Services.AddTransient<IWebhookRepo, WebhookRepo>();
                builder.Services.AddTransient<IUserProfileService, UserProfileService>();
                builder.Services.AddTransient<IUserProfileRepo, UserProfileRepo>();
                builder.Services.AddTransient<ILearningService, LearningService>();
                builder.Services.AddTransient<ILearningRepo, LearningRepo>();
                builder.Services.AddTransient<IRolesService, RolesService>();
                builder.Services.AddTransient<IRolesRepo, RolesRepo>();
                builder.Services.AddTransient<ICompetencyService, CompetencyService>();
                builder.Services.AddTransient<ICompetencyRepo, CompetencyRepo>();
                builder.Services.AddTransient<ITechTipsService, TechTipsService>();
                builder.Services.AddTransient<ITechTipsRepo, TechTipsRepo>();
                builder.Services.AddTransient<ICMSService, CMSService>();
                builder.Services.AddTransient<ICMSRepo, CMSRepo>();
                builder.Services.AddTransient<IRolesStructureService, RolesStructureService>();
                builder.Services.AddTransient<IRolesStructureRepo, RolesStructureRepo>();
                builder.Services.AddTransient<IMicrosoftAuthService>(x => new MicrosoftAuthService(cca, scopes.ToArray()));
                builder.Services.AddTransient<ISkillAdvisorService, SkillAdvisorService>();
                builder.Services.AddTransient<ISkillAdvisorRepo, SkillAdvisorRepo>();
                builder.Services.AddTransient<IMicrosoftAuthService>(x => new MicrosoftAuthService(cca, scopes.ToArray()));
                builder.Services.AddTransient<ITrainingsRepo, TrainingsRepo>();
                builder.Services.AddTransient<ITeamsService>(x => new TeamsService(graphClient, microsoftAuthService, adminUserId));
                builder.Services.AddTransient<IAssignedLearningService, AssignedLearningService>();
                builder.Services.AddTransient<IAssignedLearningRepo, AssignedLearningRepo>();
                builder.Services.AddTransient<IEmailSender, EmailSender>();

                builder.Services.AddTransient<ICommonService, CommonService>();
                builder.Services.AddTransient<ICommonRepo, CommonRepo>();
                builder.Services.AddTransient<ITrainingsService, TrainingsService>();
                builder.Services.AddTransient<ITrainingsService, TrainingsService>();
                builder.Services.AddTransient<INotificationService, NotificationService>();
                builder.Services.AddTransient<INotificationRepo, NotificationRepo>();

                builder.Services.AddTransient<IForumsService, ForumsService>();
                builder.Services.AddTransient<IForumsRepo, ForumsRepo>();

                builder.Services.AddSwaggerGen(swagger =>
                {
                    //This is to generate the Default UI of Swagger Documentation  
                    swagger.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "igetitWebAPI",
                        Description = "igetitWebAPI Release 1"
                    });
                    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    });
                    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                    });

                    System.IO.Directory.EnumerateFiles(AppContext.BaseDirectory, "Tata.IGetIT*.xml").ToList().ForEach(x =>
                    {
                        swagger.IncludeXmlComments(x);
                    });
                });


                var app = builder.Build();

                app.UseSwagger();
                app.UseSwaggerUI();

                // Configure the HTTP request pipeline.
                //if (app.Environment.IsEnvironment(EnvironmentConstants.HOSTING_ENVIRONMENT_DEV)
                //    || app.Environment.IsEnvironment(EnvironmentConstants.HOSTING_ENVIRONMENT_QA))
                //{
                //    app.UseSwagger();
                //    app.UseSwaggerUI();
                //}
                //else
                //{
                //    logger.LogDetails(new LogDetails(LogType.Info) { Message = "Swagger disabled due to environment" });
                //}

                app.UseCors(builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });

                // Global exception handler
                app.UseMiddleware<ErrorHandlerMiddleware>();

                // Authentication middleware
                app.UseMiddleware<AuthenticationMiddleware>();

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();


                logger.LogDetails(new LogDetails(LogType.Info) { Message = "Application started successfully" });

                app.Run();

            }
            catch (Exception exception)
            {
                logger.Fatal(exception, "Application stopped due to exception");
                logger.LogDetails(new LogDetails(LogType.Fatal)
                {
                    Message = "Application stopped due to exception"
                });

                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }



}