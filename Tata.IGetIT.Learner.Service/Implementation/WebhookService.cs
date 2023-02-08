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
using Tata.IGetIT.Learner.Repository.Helpers;
using Newtonsoft.Json;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    /// <summary>
    /// Service for user controller
    /// </summary>
    public class WebhookService : IWebhookService
    {
        #region Declaration
        private readonly PaymentConfig _paymentConfig;
        private readonly IWebhookRepo _webhookRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();


        public WebhookService(IWebhookRepo webhookRepo, IOptions<PaymentConfig> paymentConfig)
        {
            if (webhookRepo == null)
            {
                new ArgumentNullException("Webhook cannot be null");
            }
            _webhookRepo = webhookRepo;
            _paymentConfig = paymentConfig.Value;
        }

        #endregion


        public async Task<string> RazorpayWebhookData(string webhookData, string signature)
        {
            string strRespMessage = "Error";
            string RAZOR_PAY_SECREAT = _paymentConfig.rzp_webHookSecret;
            try
            {
                string json = webhookData;
                try
                {
                    ////Validate Sign
                    string RazorpaySignature = signature;
                    var payload = webhookData;
                    Utils.verifyWebhookSignature(payload, RazorpaySignature, RAZOR_PAY_SECREAT);
                    //logger.Debug("Signature Verified Successfully");
                    XDocument xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(json), new XmlDictionaryReaderQuotas()));
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(xml.ToString());
                    string notificationName = xmlDocument.DocumentElement.Name;
                    string eventid = xmlDocument.SelectSingleNode(notificationName + "/event").InnerText;
                    
                    logger.Debug("Razorpay Events: " + eventid.ToLower().Trim());
                    logger.Debug("Razorpay Raw Data: " + json);

                    if (eventid.ToLower().Trim() == "payment.captured")
                    {
                        //Retrieve Information from payload
                        string orderId = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/order_id").InnerText;
                        string paymentId = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/id").InnerText;
                        int amount = int.Parse(xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/amount").InnerText);
                        amount = amount / 100;

                        string email = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/email").InnerText;
                        string contactNumber = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/contact").InnerText;
                        string accID = xmlDocument.SelectSingleNode(notificationName + "/account_id").InnerText;
                        string payMethod = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/method").InnerText;
                        int tax = int.Parse(xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/tax").InnerText);
                        tax = tax / 100;

                        int razorpayFee = int.Parse(xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/fee").InnerText);
                        razorpayFee = razorpayFee / 100;

                        string status = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/status").InnerText;

                        string transactionID = "";
                        string invoiceID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/invoice_id").InnerText;

                        //logger.Debug(eventid + ": Payment InvoiceID: " + invoiceID);

                        if (payMethod == "netbanking")
                        {
                            transactionID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/acquirer_data" + "/bank_transaction_id").InnerText;
                        }
                        else if (payMethod == "card")
                        {
                            transactionID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/acquirer_data" + "/auth_code").InnerText;
                        }
                        else if (payMethod == "wallet")
                        {
                            transactionID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/acquirer_data" + "/transaction_id").InnerText;
                        }
                        else if (payMethod == "upi")
                        {
                            transactionID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/acquirer_data" + "/rrn").InnerText;
                        }
                        if (transactionID == null || transactionID == "" || transactionID == "null")
                        {
                            transactionID = "0";
                        }

                        //db update
                        _ = await _webhookRepo.UpdatePayment(accID, payMethod, tax, razorpayFee, transactionID, paymentId, status, orderId, amount);

                        strRespMessage = "Successfull";
                        logger.LogDebug(strRespMessage);
                    }
                    else if (eventid.ToLower().Trim() == "payment.failed")
                    {
                        strRespMessage = "Successfull";
                        logger.LogDebug("payment.failed");
                    }
                    else if (eventid.ToLower().Trim() == "subscription.authenticated")
                    {
                        strRespMessage = "Successfull";
                        logger.LogDebug("subscription.authenticated");
                    }
                    else if (eventid.ToLower().Trim() == "subscription.activated")
                    {
                        //Retrieve Information from payload
                        int subID = 0;
                        string ecomID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/id").InnerText; //Subscription Unique ID
                        string strUniqPlanID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/plan_id").InnerText;
                        string planName = await PaymentHelper.GetRazorpayPlanDetails(strUniqPlanID, _paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret, _paymentConfig.rzp_apiPlanDetails); //Plan unique Id need to convert to Razopay and then map it with orginal id
                        int quantity = Convert.ToInt32(xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/quantity").InnerText);
                        string endDate = PaymentHelper.UnixTimeStampToDateTime(Convert.ToDouble(xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/current_end").InnerText)).ToString("yyyy-MM-dd");

                        if (!planName.IsNullOrEmptyOrWhiteSpace())
                        {
                            subID = await _webhookRepo.Get_SubscriptionID(planName);
                        }

                        //string invoiceID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/invoice_id").InnerText;

                        //logger.Debug(eventid + ": Payment InvoiceID: " + invoiceID);

                        //db update
                        _ = await _webhookRepo.UpdateSubscription(ecomID, subID, Convert.ToDateTime(endDate), quantity);

                        strRespMessage = "Successfull";
                        logger.LogDebug("subscription.activated successfull");
                    }
                    else if (eventid.ToLower().Trim() == "subscription.charged")
                    {
                        //Retrieve Information from payload
                        int subID = 0;
                        string ecomID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/id").InnerText; //Subscription Unique ID
                        string strUniqPlanID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/plan_id").InnerText;
                        string planName = await PaymentHelper.GetRazorpayPlanDetails(strUniqPlanID, _paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret, _paymentConfig.rzp_apiPlanDetails); //Plan unique Id need to convert to Razopay and then map it with orginal id
                        int quantity = Convert.ToInt32(xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/quantity").InnerText);
                        string endDate = PaymentHelper.UnixTimeStampToDateTime(Convert.ToDouble(xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/current_end").InnerText)).ToString("yyyy-MM-dd");

                        if (!planName.IsNullOrEmptyOrWhiteSpace())
                        {
                            subID = await _webhookRepo.Get_SubscriptionID(planName);
                        }
                        //string invoiceID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/invoice_id").InnerText;

                        //logger.Debug(eventid + ": Payment InvoiceID: " + invoiceID);

                        //db update
                        _ = await _webhookRepo.UpdateSubscription(ecomID, subID, Convert.ToDateTime(endDate), quantity);

                        strRespMessage = "Successfull";
                        logger.LogDebug("subscription.charged successfull");
                    }
                    else if (eventid.ToLower().Trim() == "subscription.completed")
                    {
                        //Retrieve Information from payload
                        string ecomID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/id").InnerText; //Subscription Unique ID
                        string strUniqPlanID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/plan_id").InnerText;
                        int subID = 0;
                        string planName = await PaymentHelper.GetRazorpayPlanDetails(strUniqPlanID, _paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret, _paymentConfig.rzp_apiPlanDetails); //Plan unique Id need to convert to Razopay and then map it with orginal id
                        int quantity = Convert.ToInt32(xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/quantity").InnerText);
                        string endDate = PaymentHelper.UnixTimeStampToDateTime(Convert.ToDouble(xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/current_end").InnerText)).ToString("yyyy-MM-dd");

                        if (!planName.IsNullOrEmptyOrWhiteSpace())
                        {
                            subID = await _webhookRepo.Get_SubscriptionID(planName);
                        }

                        //string invoiceID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/payment" + "/entity" + "/invoice_id").InnerText;

                        //logger.Debug(eventid + ": Payment InvoiceID: " + invoiceID);

                        //db update
                        _ = await _webhookRepo.UpdateSubscription(ecomID, subID, Convert.ToDateTime(endDate), quantity);

                        strRespMessage = "Successfull";
                        logger.LogDebug("subscription.completed successfull");
                    }
                    else if (eventid.ToLower().Trim() == "subscription.updated")
                    {
                    }
                    else if (eventid.ToLower().Trim() == "subscription.cancelled")
                    {
                        //Retrieve Information from payload
                        string ecomID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/id").InnerText; //Subscription Unique ID

                        logger.Debug("subscription.cancelled: " + ecomID ?? "");

                        //db update
                        _ = await _webhookRepo.CancelSubscription(ecomID ?? "", "churn");
                        logger.LogDebug("subscription.cancelled successfull");
                    }
                    else if (eventid.ToLower().Trim() == "subscription.pending")
                    {
                        logger.LogDebug("subscription.pending successfull");
                    }
                    else if (eventid.ToLower().Trim() == "subscription.halted")
                    {
                        string ecomID = xmlDocument.SelectSingleNode(notificationName + "/payload" + "/subscription" + "/entity" + "/id").InnerText; //Subscription Unique ID
                        _ = await _webhookRepo.CancelSubscription(ecomID, "churn");

                        logger.LogDebug("subscription.halted successfull");
                    }
                }
                catch
                {
                    strRespMessage = "Error";
                    throw;
                }
            }
            catch
            {
                strRespMessage = "Error";
                throw;
            }

            return strRespMessage;

        }


        public async Task<bool> RecurlyWebhookData(string responseString, string userNameAndPassword)
        {
            try
            {
                string RecurlyWebhookUserName = _paymentConfig.recurly_Webhook_Username;
                string RecurlyWebhookPassword = _paymentConfig.recurly_Webhook_Password;


                if (AuthenticateUser(userNameAndPassword, RecurlyWebhookUserName, RecurlyWebhookPassword))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(responseString);
                    string notificationName = xmlDocument.DocumentElement.Name;
                    logger.LogDebug(notificationName);
                    if (notificationName == "renewed_subscription_notification")
                    {
                        int AccountID = Convert.ToInt32(xmlDocument.SelectSingleNode(notificationName + "/account/account_code").InnerText);
                        string recurlyID = xmlDocument.SelectSingleNode(notificationName + "/subscription/uuid").InnerText;
                        var result = await PaymentHelper.VerifyRenewal(AccountID, recurlyID, _paymentConfig.recurly_apiKey, _paymentConfig.recurly_domainName, _paymentConfig.recurly_loginTokenApi);

                        if (result.ExpiryDate != null)
                        {
                            //db Part
                            _ = await _webhookRepo.RecurlyVerifyAutoRenew(recurlyID, Convert.ToDateTime(result.ExpiryDate), result.DollarCost,
                            result.PlanCode, AccountID, result.Quantity);
                        }

                        logger.LogDebug(notificationName + " - Success");

                    }
                    else if (notificationName == "expired_subscription_notification")
                    {
                        string recurlyID = xmlDocument.SelectSingleNode(notificationName + "/subscription/uuid").InnerText;

                        //db Part
                        _ = await _webhookRepo.ExpireSubscription(recurlyID);
                        logger.LogDebug(notificationName + " - Success");
                    }
                    else if (notificationName == "new_subscription_notification")
                    {
                        int AccountID = Convert.ToInt32(xmlDocument.SelectSingleNode(notificationName + "/account/account_code").InnerText);
                        string ecomID = xmlDocument.SelectSingleNode(notificationName + "/subscription/uuid").InnerText;
                        int subID = Convert.ToInt32(xmlDocument.SelectSingleNode(notificationName + "/subscription/plan/plan_code").InnerText);
                        int quantity = Convert.ToInt32(xmlDocument.SelectSingleNode(notificationName + "/subscription/quantity").InnerText);
                        string startDate = xmlDocument.SelectSingleNode(notificationName + "/subscription/current_period_started_at").InnerText;
                        string endDate = xmlDocument.SelectSingleNode(notificationName + "/subscription/current_period_ends_at").InnerText;
                        int cost = Convert.ToInt32(xmlDocument.SelectSingleNode(notificationName + "/subscription/total_amount_in_cents").InnerText) / 100;
                        string subName = xmlDocument.SelectSingleNode(notificationName + "/subscription/plan/name").InnerText;
                        string trialStartDate = xmlDocument.SelectSingleNode(notificationName + "/subscription/trial_started_at").InnerText;
                        string trialEndDate = xmlDocument.SelectSingleNode(notificationName + "/subscription/trial_ends_at").InnerText;
                        Nullable<DateTime> TrialStart = null;
                        Nullable<DateTime> TrialEnd = null;
                        if (!string.IsNullOrWhiteSpace(trialStartDate))
                        {
                            try
                            {
                                TrialStart = Convert.ToDateTime(trialStartDate);
                            }
                            catch
                            {
                                TrialStart = null;
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(trialEndDate))
                        {
                            try
                            {
                                TrialEnd = Convert.ToDateTime(trialEndDate);
                            }
                            catch
                            {
                                TrialEnd = null;
                            }
                        }
                        //Utilities.Subscription.InsertPurchase(AccountID, subID, quantity, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate), cost, ecomID, subName, TrialStart, TrialEnd);


                        int subType = 1;
                        if (TrialStart.HasValue)
                        {
                            subType = 4;
                        }
                        int FulfillmentID = await _webhookRepo.InsertSalesFulfillment(AccountID, subID, quantity, startDate, endDate, cost, subType, ecomID, TrialStart, TrialEnd);

                        logger.LogDebug(notificationName + " - Success");
                    }
                    else if (notificationName == "updated_subscription_notification")
                    {
                        string ecomID = xmlDocument.SelectSingleNode(notificationName + "/subscription/uuid").InnerText;
                        int subID = Convert.ToInt32(xmlDocument.SelectSingleNode(notificationName + "/subscription/plan/plan_code").InnerText);
                        int quantity = Convert.ToInt32(xmlDocument.SelectSingleNode(notificationName + "/subscription/quantity").InnerText);
                        string endDate = xmlDocument.SelectSingleNode(notificationName + "/subscription/current_period_ends_at").InnerText;

                        //To Chnage Trial ID to Orginal ID
                        bool blnFlag = xmlDocument.SelectSingleNode(notificationName + "/subscription/plan/plan_code").InnerText.ToString().EndsWith("99");
                        if (blnFlag)
                        {
                            int newSubID = await _webhookRepo.GetNewSubID(subID, 0);
                            subID = newSubID;
                        }
                        //To Chnage Trial ID to Orginal ID

                        _ = await _webhookRepo.UpdateSubscription(ecomID, subID, Convert.ToDateTime(endDate), quantity);

                        logger.LogDebug(notificationName + " - Success");
                    }
                    else if (notificationName == "canceled_subscription_notification")
                    {
                        string recurlyID = xmlDocument.SelectSingleNode(notificationName + "/subscription/uuid").InnerText;
                        _ = await _webhookRepo.CancelSubscription(recurlyID, "churn");

                        logger.LogDebug(notificationName + " - Success");
                    }
                    else if (notificationName == "reactivated_account_notification")
                    {
                        string recurlyID = xmlDocument.SelectSingleNode(notificationName + "/subscription/uuid").InnerText;
                        _ = await _webhookRepo.ReactivateSubscription(recurlyID, "Reactivate");

                        logger.LogDebug(notificationName + " - Success");
                    }
                    else if (notificationName == "failed_payment_notification")
                    {
                        string recurlyID = xmlDocument.SelectSingleNode(notificationName + "/transaction/subscription_id").InnerText;
                        //if bypasstrial payment failed
                        logger.LogDebug(notificationName + " - Success");
                    }

                    return true;

                }
                return false;
            }
            catch
            {

                throw;
            }
        }

        private static bool AuthenticateUser(string credentials, string RecurlyWebhookUserName, string RecurlyWebhookPassword)
        {
            bool validated = false;
            try
            {
                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                validated = CheckPassword(name, password, RecurlyWebhookUserName, RecurlyWebhookPassword);
            }
            catch (FormatException)
            {
                validated = false;
            }
            return validated;
        }
        private static bool CheckPassword(string username, string password, string RecurlyWebhookUserName, string RecurlyWebhookPassword)
        {
            //return username == "recurlyUser" && password == "recurlyPassword";
            return username == RecurlyWebhookUserName && password == RecurlyWebhookPassword;
        }

    }
}
