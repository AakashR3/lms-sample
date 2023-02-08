using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Recurly;
using Recurly.Constants;
using Recurly.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Helpers;
using Tata.IGetIT.Learner.Service.Helpers;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly RecurlyConfig _recurlyConfig;
        private readonly Client _reculyClient;
        ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly ISubscriptionsRepo _subscriptionRepo;
        private readonly ILearningPathsService _learningPathsService;
        private readonly PaymentConfig _paymentConfig;
        private readonly GeoLocationConfig _geoLocationConfig;

        /// <summary>
        ///  Subscriptions service constructor initialization.
        /// </summary>
        /// <param name="recurlyConfig">Recurly payment config injected from Program.cs</param>
        public SubscriptionsService(IOptions<RecurlyConfig> recurlyConfig, ISubscriptionsRepo subscriptionsRepo,
            ILearningPathsService learningPathsService, IOptions<PaymentConfig> paymentConfig, IOptions<GeoLocationConfig> geoLocationConfig)
        {
            _recurlyConfig = recurlyConfig?.Value ?? throw new ArgumentNullException(LearnerAppConstants.RECURLY_CONFIG_NOT_FOUND);
            _subscriptionRepo = subscriptionsRepo ?? throw new ArgumentNullException(nameof(ISubscriptionsRepo));
            _learningPathsService = learningPathsService ?? throw new ArgumentNullException(nameof(ILearningPathsService));

            if (_recurlyConfig.ApiKey == null)
            {
                new KeyNotFoundException(LearnerAppConstants.RECURLY_KEY_IS_NULL_OR_EMPTY);
            }
            _reculyClient = new Client(_recurlyConfig.ApiKey);
            _paymentConfig = paymentConfig.Value;
            _geoLocationConfig = geoLocationConfig.Value;
        }

        public async Task<RecurlySubscriptionResponse> SubscribeWithRecurlyAsync(RecurlyPurchaseRequest request, List<string> ErrorsMessages)
        {

            try
            {
                await ConfigureAccountAsync(request);
                var account = await _reculyClient.GetAccountAsync("code-" + request.AccountId);
                if ((bool)account.HasActiveSubscription)
                {
                    var subscriptions = _reculyClient.ListAccountSubscriptions(account.Id);
                    if (subscriptions.Any())
                    {
                        var subscription = await _reculyClient.GetSubscriptionAsync(subscriptions.Data.First().Id);
                        if (subscription != null)
                        {
                            var changeReq = new SubscriptionChangeCreate()
                            {
                                PlanCode = request.Purchase.PlanCode,
                                Quantity = 1,
                                CouponCodes = string.IsNullOrEmpty(request.Purchase.Coupon) ? null : new List<string> { request.Purchase.Coupon }
                            };

                            var subscriptionChange = await _reculyClient.CreateSubscriptionChangeAsync(subscriptions.Data.First().Id, changeReq);

                            _ = await _subscriptionRepo.UpdateCartPurchase(request.CartID);

                            var response = new RecurlySubscriptionResponse()
                            {
                                PlanId = subscriptionChange.Plan.Id,
                                UUID = subscriptionChange.Id,
                                CurrencyCode = request.Purchase.CurrencyCode,
                                Cost = subscriptionChange.UnitAmount,
                                Email = request.Billing.Email,
                                Name = subscriptionChange.Plan.Name,
                                //Paths = (await _learningPathsService.GetLearningPathsByManagerAsync(request.UserId)).ToList()
                            };

                            logger.Info($"Subscription updated for account {account.Id}, plan {subscriptionChange.Plan.Id}, subscription id {subscriptions.First().Uuid}");
                            return response;
                        }
                    }
                }
                else
                {

                    Account account1 = await _reculyClient.GetAccountAsync(account.Id);
                    var subscription = new SubscriptionCreate()
                    {
                        CouponCodes = string.IsNullOrEmpty(request.Purchase.Coupon) ? null : new List<string> { request.Purchase.Coupon },
                        PlanCode = request.Purchase.PlanCode,
                        Quantity = 1,
                        Currency = request.Purchase.CurrencyCode,
                        Account = new AccountCreate()
                        {
                            Code = request.AccountId.Replace("code-", "")
                        },
                        BillingInfoId = account.BillingInfo.Id
                    };

                    var subscriptionCreate = await _reculyClient.CreateSubscriptionAsync(subscription);
                    _ = await _subscriptionRepo.UpdateCartPurchase(request.CartID);
                    var response = new RecurlySubscriptionResponse()
                    {
                        PlanId = subscriptionCreate.Plan.Id,
                        UUID = subscriptionCreate.Id,
                        CurrencyCode = request.Purchase.CurrencyCode,
                        Cost = subscriptionCreate.UnitAmount,
                        Email = subscriptionCreate.Account.Email,
                        Name = subscriptionCreate.Plan.Name,
                        ExpireDate = subscriptionCreate.ExpiresAt,
                        TrialDate = subscriptionCreate.TrialEndsAt

                        // Paths = (await _learningPathsService.GetLearningPathsByManagerAsync(request.UserId)).ToList()
                    };

                    logger.Info($"Subscription created for account {account.Id}, plan {subscriptionCreate.Plan.Id}, subscription id {subscriptionCreate.Id}");
                    return response;
                }
            }
            catch
            {
                throw;
            }
            throw new Exception($"Subscription event failure");
        }

        private async Task ConfigureAccountAsync(RecurlyPurchaseRequest request)
        {
            try
            {
                Account account = new();
                try
                {
                    account = await _reculyClient.GetAccountAsync("code-" + request.AccountId.ToString());
                }
                catch
                {
                    if (account.Id == null)
                    {
                        var accountReq = new AccountCreate()
                        {
                            Code = request.AccountId,
                            Email = request.Billing.Email,
                            FirstName = request.Billing.FirstName,
                            LastName = request.Billing.LastName,
                            Address = new Address()
                            {
                                Street1 = request.Billing.Address1,
                                Street2 = request.Billing.Address2,
                                City = request.Billing.City,
                                Region = request.Billing.State,
                                Country = request.Billing.Country,
                                PostalCode = request.Billing.Zip
                            }
                        };

                        account = await _reculyClient.CreateAccountAsync(accountReq);

                        logger.Info($"Account created for email {request.Billing.Email}");
                    }
                }

                if (account != null && account.BillingInfo == null)
                {
                    var billingInfo = new BillingInfoCreate()
                    {
                        TokenId = request.RecurlyToken
                    };
                    var reslt_temp = await _reculyClient.CreateBillingInfoAsync(account.Id, billingInfo);
                    logger.Info($"Billing info created for account id {request.AccountId} email {request.Billing.Email}");
                }
                else if (account != null && account.BillingInfo != null)
                {
                    var billingInfo = new BillingInfoCreate()
                    {
                        TokenId = request.RecurlyToken,

                    };
                    var reslt_temp = await _reculyClient.UpdateABillingInfoAsync(account.Id, account.BillingInfo.Id, billingInfo);
                    logger.Info($"Billing info created for account id {request.AccountId} email {request.Billing.Email}");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(int UserID, List<string> ErrorsMessages)
        {
            var result = await _subscriptionRepo.GetUserSubscriptionsAsync(UserID);
            if (!result.Any())
                ErrorsMessages.Add(LearnerAppConstants.NO_SUBSCRIPTION);
            return result;
        }

        public async Task<IEnumerable<UsersPurchasedHistory>> GetUserPurchasedHistoryAsync(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _subscriptionRepo.GetUserPurchasedHistoryAsync(UserID);
                if (!result.Any())
                    ErrorsMessages.Add(LearnerAppConstants.NO_SUBSCRIPTION);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<AvailableSubscription>> GetAvailableSubscription(string CountryCode, int CategoryID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _subscriptionRepo.GetAvailableSubscription(CountryCode, CategoryID);
                if (!result.Any())
                    ErrorsMessages.Add(LearnerAppConstants.NO_Available_SUBSCRIPTION);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<RecurlySubscriptionResponse> MultiSubscriptionWithRecurlyAsync(RecurlyPurchaseRequest request, List<string> ErrorsMessages)
        {
            try
            {
                RecurlySubscriptionResponse recurlySubscriptionResponse = new();
                List<SubscriptionPurchase> subscriptionPurchase = new List<SubscriptionPurchase>();
                string CardIDs = "0";
                if (request.Subscriptions == null)
                {
                    ErrorsMessages.Add(LearnerAppConstants.NO_CART_ITEM_SELECTED);
                }
                else
                {
                    if (request.Subscriptions == null)
                    {
                        ErrorsMessages.Add(LearnerAppConstants.NO_CART_ITEM_SELECTED);
                    }
                    else
                    {

                        if (request.Subscriptions.Count > 0)
                        {
                            CardIDs = String.Join(",", request.Subscriptions.Select(p => p.CartID));

                            foreach (var subscription in request.Subscriptions)
                            {
                                var SubscriptionPurchase = new SubscriptionPurchase();
                                if (subscription.IsTrial.ToString() == "1")
                                {
                                    SubscriptionPurchase.PlanCode = subscription.SubscriptionID.ToString() + "99";
                                }
                                else
                                {
                                    SubscriptionPurchase.PlanCode = subscription.SubscriptionID.ToString();
                                }
                                SubscriptionPurchase.Quantity = 1;
                                subscriptionPurchase.Add(SubscriptionPurchase);
                            }
                        }
                        var purchaseReq = new PurchaseCreate()
                        {
                            Currency = request.Purchase.CurrencyCode,
                            Account = new AccountPurchase()
                            {
                                Code = request.AccountId.ToString(),
                                FirstName = request.Billing.FirstName,
                                LastName = request.Billing.LastName,
                                Email = request.Billing.Email,
                                Address = new Address()
                                {
                                    Street1 = request.Billing.Address1,
                                    Street2 = request.Billing.Address2,
                                    City = request.Billing.City,
                                    Region = request.Billing.State,
                                    Country = request.Billing.Country,
                                    PostalCode = request.Billing.Zip,
                                },
                                BillingInfo = new BillingInfoCreate()
                                {
                                    TokenId = request.RecurlyToken
                                }
                            },
                            Subscriptions = subscriptionPurchase,
                            CouponCodes = string.IsNullOrEmpty(request.Purchase.Coupon) ? null : new List<string> { request.Purchase.Coupon },
                        };
                        InvoiceCollection collection = await _reculyClient.CreatePurchaseAsync(purchaseReq);
                        Console.WriteLine($"Created ChargeInvoice with Number: {collection.ChargeInvoice.Number}");

                        _ = await _subscriptionRepo.UpdateMultipleCartPurchase(CardIDs, request.UserId);
                    }
                }
                return recurlySubscriptionResponse;
            }
            catch (Recurly.Errors.Validation ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RecurlyCardCheckout)), ex);
                // If the request was not valid, you may want to tell your user
                // why. You can find the invalid params and reasons in ex.Error.Params
                //Console.WriteLine($"Failed validation: {ex.Error.Message}");
                throw;
            }
            catch (Recurly.Errors.ApiError ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RecurlyCardCheckout)), ex);
                // Use ApiError to catch a generic error from the API
                //Console.WriteLine($"Unexpected Recurly Error: {ex.Error.Message}");
                throw;
            }

        }

        public async Task<ProfessionalBundle> GetProfessionalBundle(string ClientIP, List<string> ErrorsMessages)
        {
            try
            {
                var locationResult = await GetCountryCode(ClientIP);
                var result = await _subscriptionRepo.GetProfessionalBundle(locationResult.CountryCode);

                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<ProfessionalBundle> GetwwwProfessionalBundle(string CountryCode, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _subscriptionRepo.GetProfessionalBundle(CountryCode);

                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Location> GetCountryCode(string ClientIP)
        {
            try
            {
                Location location = new();

                if (ClientIP != "::1")
                {
                    location.CountryCode = await UtilityHelper.GetGeoLocation(ClientIP, _geoLocationConfig.GeoLocationApi, _geoLocationConfig.AzureMapKey, _geoLocationConfig.PublicIPApi);
                    location.IPAddress = await UtilityHelper.GetGeoIPAddress(ClientIP, _geoLocationConfig.GeoLocationApi, _geoLocationConfig.AzureMapKey, _geoLocationConfig.PublicIPApi);
                    logger.LogDebug("ProfessionalBundle GetCountryCode API: " + location.IPAddress);
                }
                else
                {
                    location.CountryCode = "IN";
                }
                return location;
            }
            catch (Exception ex)
            {
                logger.LogDebug("ProfessionalBundle GetCountryCode Exception: " + ex.ToString());
                throw;
            }
        }

        public async Task<InvoiceResponse> DownloadInvoice(string SubscriptionID, List<string> ErrorsMessages)
        {
            string result = string.Empty;
            string invoiceId = string.Empty;
            InvoiceResponse invoiceResponse = new InvoiceResponse();
            try
            {
                if (SubscriptionID.ToLower().StartsWith("sub_") == true || SubscriptionID.ToLower().StartsWith("pay_") == true)
                {
                    var res = await _subscriptionRepo.BillingInfo(SubscriptionID, "INR");

                    Invoices model = new Invoices();
                    model.type = "invoice";
                    model.description = res.description;
                    model.partial_payment = false;

                    BillingAddress billingAddress = new BillingAddress
                    {
                        line1 = res.billing_address_line1,
                        line2 = res.billing_address_line2,
                        zipcode = res.billing_address_zipcode,
                        city = res.billing_address_city,
                        state = res.billing_address_state,
                        country = res.billing_address_country,
                    };

                    Repository.Models.ShippingAddress shippingAddress = new Repository.Models.ShippingAddress
                    {
                        line1 = res.shipping_address_line1,
                        line2 = res.shipping_address_line2,
                        zipcode = res.shipping_address_zipcode,
                        city = res.shipping_address_city,
                        state = res.shipping_address_state,
                        country = res.shipping_address_country,
                    };

                    Customer customer = new Customer
                    {
                        name = res.customer_name,
                        contact = res.phone,
                        email = res.email,
                        billing_address = billingAddress,
                        shipping_address = shippingAddress
                    };
                    model.customer = customer;
                   

                    float PurchasedAmount = await PaymentHelper.GetRazorPayPurchasedAmount(SubscriptionID, _paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret, "https://api.razorpay.com/v1/payments/");
                    
                    List<Repository.Models.LineItem> lineItem = new List<Repository.Models.LineItem>
                    {
                        new Repository.Models.LineItem
                        {
                            name=res.subscription_name,
                            description="",
                            amount=PurchasedAmount,
                            currency="INR",
                            quantity=res.Quantity
                        }
                    };

                    model.line_items = lineItem;
                    model.sms_notify = 1;
                    model.email_notify = 1;
                    model.draft = "1";
                    model.date = res.date;
                    model.expire_by = res.expire_by;
                    Random randomObj = new Random();
                    string transactionId = randomObj.Next(10000000, 100000000).ToString();
                    model.receipt = transactionId;
                    model.comment = res.comment;
                    model.terms = res.terms;

                    Notes notes = new Notes
                    {
                        notes_key_1 = "",
                        notes_key_2 = ""
                    };
                    model.notes = notes;

                    using (HttpClient client = new HttpClient())
                    {
                        var jsonString = JsonConvert.SerializeObject(model);

                        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                        var authenticationBytes = Encoding.ASCII.GetBytes($"{_paymentConfig.rzp_apiKey}:{_paymentConfig.rzp_apiSecret}");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes));
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var CreateResponse = await client.PostAsync("https://api.razorpay.com/v1/invoices", content);
                        var Invoice_Create_Response = await CreateResponse.Content.ReadAsStringAsync();
                        dynamic data = JObject.Parse(Invoice_Create_Response);
                        invoiceId = data.id;



                        if (!CreateResponse.IsSuccessStatusCode)
                        {
                            result = data.error.description;
                            ErrorsMessages.Add(result);
                        }

                        if (CreateResponse.IsSuccessStatusCode)
                        {
                            using (HttpClient client1 = new HttpClient())
                            {
                                var request_json = jsonString;
                                var content1 = new StringContent("", Encoding.UTF8, "application/json");
                                client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes));
                                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                string url = $"https://api.razorpay.com/v1/invoices/{invoiceId}/issue";
                                var IssueInvoiceResponse = await client1.PostAsync(url, content1);
                                var response_string = await IssueInvoiceResponse.Content.ReadAsStringAsync();
                                dynamic data1 = JObject.Parse(response_string);

                                if (!IssueInvoiceResponse.IsSuccessStatusCode)
                                {
                                    result = data1.error.description;
                                    ErrorsMessages.Add(result);
                                }

                                invoiceResponse.ShortURL = data1.short_url;
                                invoiceResponse.InvoiceType = "URL";
                                
                            }
                        }                    }
                }
                else if (SubscriptionID.ToLower().StartsWith("inv_") == true)
                {
                    using (HttpClient client1 = new HttpClient())
                    {
                        //string invoiceId = SubscriptionID;
                        var authenticationBytes = Encoding.ASCII.GetBytes($"{_paymentConfig.rzp_apiKey}:{_paymentConfig.rzp_apiSecret}");
                        //var content1 = new StringContent("", Encoding.UTF8, "application/json");
                        client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes));
                        client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        string url = $"https://api.razorpay.com/v1/invoices/{SubscriptionID}";
                        var IssueInvoiceResponse = await client1.GetAsync(url);
                        var response_string = await IssueInvoiceResponse.Content.ReadAsStringAsync();
                        dynamic data1 = JObject.Parse(response_string);

                        if (!IssueInvoiceResponse.IsSuccessStatusCode)
                        {
                            result = data1.error.description;
                            ErrorsMessages.Add(result);
                        }
                        invoiceResponse.ShortURL = data1.short_url;
                        invoiceResponse.InvoiceType = "URL";
                    }
                }
                else
                {
                    string InvoiceNo = string.Empty;
                    var subscriptionInvoices = _reculyClient.ListSubscriptionInvoices("uuid-" + SubscriptionID);
                    foreach (Invoice inv in subscriptionInvoices)
                    {
                        Console.WriteLine(inv.Number);
                        InvoiceNo = inv.Number;
                    }
                    if (InvoiceNo.IsNullOrEmptyOrWhiteSpace())
                    {
                        ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_FOUND_INVOICE);
                    }
                    else
                    {
                        //string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                        //string pathDownload = Path.Combine(pathUser, "Downloads/Invoice");
                        string pathDownload = "Downloads/Invoice";

                        BinaryFile invoice = _reculyClient.GetInvoicePdf("number-" + InvoiceNo);
                        string filename = $"{pathDownload}/payment-invoice-{InvoiceNo}-{DateTime.Now.ToString("ss")}.pdf";
                        System.IO.File.WriteAllBytes(filename, invoice.Data);
                        Console.WriteLine($"Saved Invoice PDF to {filename}");

                        invoiceResponse.FilePath = filename;
                        invoiceResponse.InvoiceType = "FILE";


                        Byte[] bytes = File.ReadAllBytes(filename);
                        string file = Convert.ToBase64String(bytes);

                        invoiceResponse.FileContent = file;
                        invoiceResponse.FileName = $"payment-invoice-{InvoiceNo}-{DateTime.Now.ToString("ss")}.pdf";
                    }
                }
            }
            catch
            {
                throw;
            }
            return invoiceResponse;
        }

        public async Task<SubscriptionDetail> GetSubscriptionDetail(string SubscriptionID, int UserID, List<string> ErrorsMessages)
        {
            var result = await _subscriptionRepo.GetSubscriptionDetail(SubscriptionID,UserID);
            if (result == null)
                ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
            return result;
        }
    }
}
