using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using Razorpay.Api.Errors;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using Tata.IGetIT.Learner.Repository.Models.Configurations;
using Microsoft.Extensions.Options;
using Tata.IGetIT.Learner.Repository.Helpers;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    /// <summary>
    /// Webhooks let you build or set up integrations that subscribe to certain events on RazorpayX API or Recurly.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : BaseController
    {
        private readonly IWebhookService _webhookService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public WebhookController(IWebhookService webhookService)
        {
            _webhookService = webhookService ?? throw new ArgumentNullException(nameof(IWebhookService));
        }

        #region Razorpay Webhook
        /// <summary>
        /// Razorpay Webhook
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost("RazorPayWebHook")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> RazorPayWebHook([FromBody] object data)
        {
            try
            {
                Task.Delay(10000).Wait();

                string signature = Request.Headers["x-razorpay-signature"].ToString();
                logger.Debug("RazorPay Actual Signature: " + signature);

                string webhookData = data.ToString();
                //logger.Debug("RazorPay Webhook: "+ webhookData);

                if (signature.IsNullOrEmptyOrWhiteSpace() || webhookData.IsNullOrEmptyOrWhiteSpace())
                {
                    Response.Clear();
                    logger.Debug("Empty Signature or WebhookData");

                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<Response>()
                    {
                        Message = LearnerAppConstants.WEBHOOK_INVALID_DATA
                    });
                }
                else
                {
                    var result = await _webhookService.RazorpayWebhookData(webhookData, signature);

                    Response.Clear();
                    logger.LogDebug("Success Response after Data processed: "+ result);                    
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Response>()
                    {
                        Message = LearnerAppConstants.WEBHOOK_PROCESSED
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.EXCEPTION_MESSAGE,
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                Response.Clear();
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });

            }
        }
        #endregion


        #region Recurly Webhook
        /// <summary>
        /// Recurly WebHook
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost("RecurlyWebHook")]
        [Consumes("application/xml")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]

        public async Task<IActionResult> RecurlyWebHook()
        {
            try
            {
                string authHeader = Request.Headers["Authorization"].ToString();
                logger.Info("Auth Header: " + authHeader);

                var reader = new StreamReader(Request.Body, Encoding.UTF8);
                var body = await reader.ReadToEndAsync().ConfigureAwait(false);

                logger.Info("Webhook Data: " + body);

                if (authHeader != null)
                {
                    if (authHeader.StartsWith("basic ", StringComparison.InvariantCultureIgnoreCase))
                    {
                        logger.Info("Auth Header Success");

                        string userNameAndPassword = Encoding.Default.GetString(Convert.FromBase64String(authHeader.Substring(6)));

                        bool result = await _webhookService.RecurlyWebhookData(body, userNameAndPassword);

                        if (result == true)
                        {
                            HttpContext.Response.Clear();
                            HttpContext.Response.StatusCode = 200;

                            logger.Info("Response Success");

                            return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Response>()
                            {
                                Message = LearnerAppConstants.WEBHOOK_PROCESSED
                            });
                        }
                        else
                        {
                            HttpContext.Response.Clear();
                            return StatusCode((int)HttpStatusCode.Unauthorized, new ReturnResponse<Response>()
                            {
                                Message = LearnerAppConstants.UNABLE_PROCESSED_WEBHOOK
                            });
                        }
                    }
                    else
                    {
                        HttpContext.Response.Clear();
                        return StatusCode((int)HttpStatusCode.Unauthorized, new ReturnResponse<Response>()
                        {
                            Message = LearnerAppConstants.UNABLE_PROCESSED_WEBHOOK
                        });
                    }
                }
                else
                {
                    HttpContext.Response.Clear();
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<Response>()
                    {
                        Message = LearnerAppConstants.UNABLE_PROCESSED_WEBHOOK
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.EXCEPTION_MESSAGE,
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                HttpContext.Response.Clear();
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>()
                {
                    Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE
                });
            }
        }

        #endregion


    }
}
