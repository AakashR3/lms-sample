using Newtonsoft.Json.Linq;
using Recurly;
using Recurly.Resources;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using Tata.IGetIT.Learner.Repository.Helpers;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly Client _reculyClient;
        private readonly IPaymentRepo _paymentRepo;
        private readonly PaymentConfig _paymentConfig;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public PaymentService(IPaymentRepo paymentRepo, IOptions<PaymentConfig> paymentConfig)
        {
            if (paymentRepo == null)
            {
                new ArgumentNullException("PaymentService cannot be null");
            }
            _paymentRepo = paymentRepo;
            _paymentConfig = paymentConfig.Value;
            _reculyClient = new Recurly.Client(_paymentConfig.recurly_apiKey);
        }

        public async Task<IEnumerable<Currency>> CurrencyList()
        {
            try
            {
                return await _paymentRepo.CurrencyList();
            }
            catch
            {
                throw;
            }
        }




        public async Task<bool> CancelSubscription(CancelSubscription cancelSubscription, List<string> ErrorsMessages)
        {
            try
            {
                bool response = false;

                if (cancelSubscription.SubscriptionID.IsNullOrEmptyOrWhiteSpace())
                {
                    ErrorsMessages.Add(LearnerAppConstants.RAZORPAY_SUBSCRIPTION_ID_REQUIRED);
                }
                else
                {
                    bool blnFlag = false;
                    if (cancelSubscription.SubscriptionID.ToLower().StartsWith("sub_") == true)
                    {
                        blnFlag = await PaymentHelper.RazorpayCancelSubscription(cancelSubscription.SubscriptionID, _paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret,
                        _paymentConfig.rzp_apiCancelSubscription);

                        if (blnFlag == true)
                        {
                            bool result = await CancelSubscriptionDB(cancelSubscription, ErrorsMessages);

                            if (result == true)
                            {
                                response = true;
                            }
                            else
                            {
                                ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_CANCEL_SUBSCRIPTION);
                            }
                        }
                        else
                        {
                            ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_CANCEL_SUBSCRIPTION);
                        }
                    }
                    else
                    {
                        /////Recurly Subscription here/////

                        var cancel = await _reculyClient.CancelSubscriptionAsync("uuid-" + cancelSubscription.SubscriptionID);

                        if (cancel != null)
                        {
                            if (cancel.ExpirationReason == "canceled")
                            {
                                response = true;
                            }
                            else
                            {
                                ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_CANCEL_SUBSCRIPTION);
                            }
                        }
                        else
                        {
                            ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_CANCEL_SUBSCRIPTION);
                        }

                    }
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> CancelSubscriptionDB(CancelSubscription cancelSubscription, List<string> ErrorsMessages)
        {
            try
            {
                bool result = false;
                result = await _paymentRepo.CancelSubscription(cancelSubscription.SubscriptionID, "Churn");

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RazorpayResponse(RazorpayResponse _requestData, List<string> ErrorsMessages)
        {
            try
            {
                bool result = false;
                if (_requestData.PurchaseType.ToUpper() == "PLAN")
                {
                    result = await PlanPurchaseResponse(_requestData, ErrorsMessages);
                }
                else
                {
                    result = await SubscriptionPurchaseResponse(_requestData, ErrorsMessages);
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
               



    public async Task<bool> SubscriptionPurchaseResponse(RazorpayResponse _requestData, List<string> ErrorsMessages)
    {
            try
            {
                bool response = false;
                if (_requestData.CartID == 0)
                {
                    ErrorsMessages.Add(LearnerAppConstants.RAZORPAY_CART_ID_REQUIRED);
                    response = false;
                }
                else
                {
                    string sub_id = _requestData.RZP_SubscriptionID;
                    int accountID = _requestData.AccountID;
                    string paymentId = _requestData.PaymentID;
                    string razorpay_signature = _requestData.Signature;
                    int subID = _requestData.SubscriptionID;
                    int quantity = 1;
                    int cost = Convert.ToInt32(_requestData.Amount);
                    cost = cost / 100;
                    string ecomID = _requestData.RZP_SubscriptionID;
                    int cartId = _requestData.CartID;

                    string strStart = PaymentHelper.UnixTimeStampToDateTime(Convert.ToDouble(_requestData.StartAt)).ToString("yyyy-MM-dd");
                    string strEnd = PaymentHelper.UnixTimeStampToDateTime(Convert.ToDouble(_requestData.EndAt)).ToString("yyyy-MM-dd");
                    string strTrialStart = PaymentHelper.UnixTimeStampToDateTime(Convert.ToDouble(_requestData.TrialstartAt)).ToString("yyyy-MM-dd");
                    string strTrialEnd = PaymentHelper.UnixTimeStampToDateTime(Convert.ToDouble(_requestData.TrialEndAt)).ToString("yyyy-MM-dd");

                    DateTime startDate = DateTime.ParseExact(strStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(strEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    DateTime? TrialStart = null;
                    DateTime? TrialEnd = null;
                    if (Convert.ToDouble(_requestData.TrialstartAt) != 0) // This is for Trail subscription
                    {
                        TrialStart = DateTime.ParseExact(strTrialStart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        TrialEnd = DateTime.ParseExact(strTrialEnd, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    }
                    string subName = _requestData.PlanName;
                    string generated_signature = "";
                    var enc = Encoding.Default;
                    byte[]
                    baText2BeHashed = enc.GetBytes(paymentId + "|" + sub_id),
                    baSalt = enc.GetBytes(_paymentConfig.rzp_apiSecret);
                    HMACSHA256 hasher = new HMACSHA256(baSalt);
                    byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
                    generated_signature = string.Join("", baHashedText.ToList().Select(b => b.ToString("x2")).ToArray());
                    if (generated_signature == razorpay_signature)
                    {
                        int subType = 1;
                        if (TrialStart.HasValue)
                        {
                            subType = 4;
                        }
                        string trialStartDate, trialEndDate;
                        if (TrialStart.HasValue)
                        {
                            trialStartDate = Convert.ToDateTime(TrialStart).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            trialStartDate = "";
                        }

                        if (TrialEnd.HasValue)
                        {
                            trialEndDate = Convert.ToDateTime(TrialEnd).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            trialEndDate = "";
                        }

                        //string InvoiceID = await PaymentHelper.GetRazorPayInvoiceID(_requestData.PaymentID, _paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret, "https://api.razorpay.com/v1/payments/");

                        int FulfillmentID = await _paymentRepo.SubscriptionResponse(accountID, subID, quantity, startDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        endDate.ToString("yyyy-MM-dd HH:mm:ss"), cost, subType, ecomID, trialStartDate, trialEndDate, cartId, _requestData.PaymentID);
                        if (FulfillmentID > 0)
                        {
                            response = true;
                        }
                        else
                        {
                            response = false;
                            ErrorsMessages.Add(LearnerAppConstants.RAZORPAY_UNABLE_TO_COMPLETE_PAYMENT);
                        }
                    }
                    else
                    {
                        ErrorsMessages.Add(LearnerAppConstants.RAZORPAY_PAYMENT_FAILURE);
                        response = false;
                    }
                }

                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> PlanPurchaseResponse(RazorpayResponse _requestData, List<string> ErrorsMessages)
        {
            try
            {
                bool response = false;

                if (_requestData.CartID == 0)
                {
                    ErrorsMessages.Add(LearnerAppConstants.RAZORPAY_CART_ID_REQUIRED);
                    response = false;
                }
                else
                {
                    var RPlanList = await PaymentHelper.FetchAllPlans(_paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret, _paymentConfig.rzp_apiPlanList);
                    int ID = _requestData.ID;
                    int UserID = _requestData.UserID;
                    string FirstName = _requestData.FirstName;
                    string MiddleName = _requestData.MiddleName;
                    string LastName = _requestData.LastName;
                    string address = _requestData.Address;
                    string paymentId = _requestData.PaymentID;
                    double amount = _requestData.Amount;
                    amount = amount / 100;
                    string email = _requestData.Email;
                    string contactNumber = _requestData.ContactNumber;
                    string planname = _requestData.PlanName;
                    string planId = _requestData.RZP_PlanID;

                    var Plan = PaymentHelper.GetPlanInfo(planId, RPlanList);
                    var PlanPrice = Plan.Amount;
                    var RPlanCode = Plan.PlanCode;
                    var RPlanDesc = Plan.Description;
                    string plandescription = RPlanDesc;
                    int quantity = _requestData.Quantity;
                    int noOfUsers = _requestData.NoOfUsers;
                    string transactionID = _requestData.TransactionID;
                    string order_id = _requestData.OrderID;
                    string razorpay_signature = _requestData.Signature;
                    int cartId = _requestData.CartID;

                    Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(_paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret);
                    Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);
                    string paymentStatus = payment.Attributes["status"];

                    string generated_signature = "";

                    var enc = Encoding.Default;
                    byte[]
                    baText2BeHashed = enc.GetBytes(order_id + "|" + paymentId),
                    baSalt = enc.GetBytes(_paymentConfig.rzp_apiSecret);
                    HMACSHA256 hasher = new HMACSHA256(baSalt);
                    byte[] baHashedText = hasher.ComputeHash(baText2BeHashed);
                    generated_signature = string.Join("", baHashedText.ToList().Select(b => b.ToString("x2")).ToArray());
                    if (generated_signature == razorpay_signature)
                    {  
                        //payment is successful
                        string result = await _paymentRepo.PlanPurchaseResponse(ID, UserID, planname, plandescription, planId, amount, quantity, noOfUsers, paymentId, paymentStatus, transactionID, FirstName, MiddleName, LastName, email, address, contactNumber, order_id, cartId);

                        if (result != null)
                        {
                            if (result.ToUpper() == "SUCCESFULLY INSERTED")
                            {
                                response = true;
                            }
                            else
                            {
                                response = false;
                                ErrorsMessages.Add(LearnerAppConstants.RAZORPAY_UNABLE_TO_COMPLETE_PAYMENT);
                            }
                        }
                        else
                        {
                            response = false;
                            ErrorsMessages.Add(LearnerAppConstants.RAZORPAY_UNABLE_TO_COMPLETE_PAYMENT);
                        }
                    }
                    else
                    {
                        response = false;
                        ErrorsMessages.Add(LearnerAppConstants.RAZORPAY_PAYMENT_FAILURE);
                    }
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<RazorpayModel> RazorpayCheckout(PaymentInitiate payment, List<string> errorsMessages)
        {
            try
            {
                RazorpayModel orderModel = new();
                if (payment.PurchaseType.ToUpper() == "PLAN")
                {
                    orderModel = await CreateOrder(payment, errorsMessages);
                }
                else
                {
                    orderModel = await CreateSusbscription(payment, errorsMessages);
                }
                return orderModel;
            }
            catch
            {
                throw;
            }
        }
        public async Task<RazorpayModel> CreateOrder(PaymentInitiate _requestData, List<string> errorsMessages)
        {
            try
            {
                int ID = 0;
                RazorpayModel orderModel = new();
                ID = await _paymentRepo.GetPlanID(_requestData.PlanCode.ToString());
                if (ID > 0)
                {
                    var RPlanList = await PaymentHelper.FetchAllPlans(_paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret, _paymentConfig.rzp_apiPlanList);
                    var PlanIDResult = PaymentHelper.GetPlanID(_requestData.PlanCode.ToString(), RPlanList);
                    var PlanID = PlanIDResult.ID;
                    var Plan = PaymentHelper.GetPlanInfo(PlanID, RPlanList);
                    var PlanPrice = Plan.Amount;
                    var RPlanCode = Plan.PlanCode;
                    var RPlanDesc = Plan.Description;

                    Random randomObj = new Random();
                    string transactionId = randomObj.Next(10000000, 100000000).ToString(); //this transaction id is for genrating receipt
                    Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(_paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret);//("APIKeyID, APIKeySecret") Test API Credentials
                    Dictionary<string, object> options = new Dictionary<string, object>();
                    options.Add("amount", PlanPrice);  // Amount will in paise
                    options.Add("receipt", transactionId);
                    options.Add("currency", "INR");
                    options.Add("payment_capture", "1"); // 1 - automatic  , 0 - manual

                    Razorpay.Api.Order orderResponse = client.Order.Create(options);

                    if (orderResponse == null)
                    {
                        errorsMessages.Add(LearnerAppConstants.RAZORPAY_UNABLE_TO_CREATE_ORDER);
                    }
                    else
                    {
                        string orderId = orderResponse["id"].ToString();

                        if (orderId.IsNullOrEmptyOrWhiteSpace())
                        {
                            errorsMessages.Add(LearnerAppConstants.RAZORPAY_UNABLE_TO_CREATE_ORDER);
                        }
                        else
                        {
                            orderModel.RazorpayKey = _paymentConfig.rzp_apiKey;
                            orderModel.ID = ID;
                            orderModel.UserID = _requestData.UserID;
                            orderModel.AccountID = _requestData.AccountID;
                            orderModel.SubscriptionID = _requestData.SubscriptionID;
                            orderModel.OrderID = orderResponse.Attributes["id"];
                            orderModel.Amount = PlanPrice;
                            orderModel.Currency = "INR";
                            orderModel.FirstName = _requestData.FirstName;
                            orderModel.MiddleName = _requestData.MiddleName;
                            orderModel.LastName = _requestData.LastName;
                            orderModel.Email = _requestData.Email;
                            orderModel.ContactNumber = _requestData.ContactNumber;
                            orderModel.Address = _requestData.Address;
                            orderModel.Description = "";
                            orderModel.PlanName = _requestData.PlanCode;
                            orderModel.PlanDescription = RPlanDesc;
                            orderModel.PlanID = PlanID;
                            orderModel.Quantity = _requestData.Quantity;
                            orderModel.NoOfUsers = _requestData.NoOfUsers;
                            orderModel.TransactionID = transactionId;
                            orderModel.PurchaseType = _requestData.PurchaseType;
                            orderModel.CartID = _requestData.CartID;
                        }
                    }
                }
                else
                {
                    errorsMessages.Add(LearnerAppConstants.RAZORPAY_INVALID_PLAN);
                }

                return orderModel;
            }
            catch
            {
                throw;
            }
        }

        public async Task<RazorpayModel> CreateSusbscription(PaymentInitiate _requestData, List<string> errorsMessages)
        {
            RazorpayModel objSubModel = new();
            PlanDetailsModel objPlanDetails = new PlanDetailsModel();
            try
            {
                int ID = 0;
                ID = await _paymentRepo.GetPlanID(_requestData.PlanCode.ToString());
                if (ID > 0)
                {
                    var RPlanList = await PaymentHelper.FetchAllPlans(_paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret, _paymentConfig.rzp_apiPlanList);
                    var PlanIDResult = PaymentHelper.GetPlanID(_requestData.PlanCode.ToString(), RPlanList);
                    var PlanID = PlanIDResult.ID;
                    var Plan = PaymentHelper.GetPlanInfo(PlanID, RPlanList);
                    var PlanPrice = Plan.Amount;
                    var RPlanCode = Plan.PlanCode;
                    var RPlanDesc = Plan.Description;


                    objSubModel.RazorpayKey = _paymentConfig.rzp_apiKey;
                    objSubModel.ID = ID;
                    objSubModel.UserID = _requestData.UserID;
                    objSubModel.AccountID = _requestData.AccountID;
                    objSubModel.SubscriptionID = _requestData.SubscriptionID;
                    objSubModel.FirstName = _requestData.FirstName;
                    objSubModel.MiddleName = _requestData.MiddleName;
                    objSubModel.Description = "Customer Name";
                    objSubModel.LastName = _requestData.LastName;
                    objSubModel.Email = _requestData.Email;
                    objSubModel.ContactNumber = _requestData.ContactNumber;
                    objSubModel.Address = _requestData.Address;
                    objSubModel.PlanName = _requestData.PlanCode;
                    objSubModel.PlanID = PlanID;
                    objSubModel.Amount = Plan.Amount;
                    objSubModel.PlanDescription = RPlanDesc;
                    objSubModel.Currency = "INR";
                    objSubModel.Quantity = _requestData.Quantity;
                    objSubModel.NoOfUsers = _requestData.NoOfUsers;
                    objSubModel.IsTrial = _requestData.IsTrial;
                    objSubModel.PurchaseType = _requestData.PurchaseType;
                    objSubModel.CartID = _requestData.CartID;


                    Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(_paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret);
                    Dictionary<string, object> options = new Dictionary<string, object>();
                    options.Add("plan_id", PlanID);  // Amount will in paise
                    options.Add("total_count", 1);
                    options.Add("quantity", 1);
                    options.Add("customer_notify", true);
                    if (objSubModel.IsTrial == 1)
                    {
                        DateTime StartDate = DateTime.Today.AddDays(7);
                        double dblStartDate = PaymentHelper.DateTimeToUnixTimeStampUTC(StartDate);
                        options.Add("start_at", dblStartDate);
                    }
                    Razorpay.Api.Subscription createSub = client.Subscription.Create(options);

                    if (createSub == null)
                    {
                        errorsMessages.Add(LearnerAppConstants.RAZORPAY_UNABLE_TO_CREATE_SUBSCRIPTION);
                    }
                    else
                    {
                        string subscriptionID = createSub.Attributes.id;

                        if (subscriptionID.IsNullOrEmptyOrWhiteSpace())
                        {
                            errorsMessages.Add(LearnerAppConstants.RAZORPAY_UNABLE_TO_CREATE_SUBSCRIPTION);
                        }
                        else
                        {
                            if (createSub.Attributes.id != null && createSub.Attributes.status == "created")
                            {
                                objSubModel.RZP_SubscriptionID = createSub.Attributes.id;
                                objSubModel.IntQuantity = createSub.Attributes.quantity;
                                if (objSubModel.IsTrial == 1) // For Trial
                                {
                                    objSubModel.DtChargeAt = createSub.Attributes.charge_at;
                                    objSubModel.DtStartAt = createSub.Attributes.created_at; ;
                                    objSubModel.DtEndAt = createSub.Attributes.start_at; ;
                                    objSubModel.DtTrialStartAt = createSub.Attributes.created_at;
                                    objSubModel.DtTrialEndAt = createSub.Attributes.start_at;
                                }
                                else
                                {
                                    objSubModel.DtStartAt = PaymentHelper.DateTimeToUnixTimeStampUTC(DateTime.Today);
                                    objPlanDetails = await PaymentHelper.GetPlanDetails(PlanID, _paymentConfig.rzp_apiKey, _paymentConfig.rzp_apiSecret, _paymentConfig.rzp_apiPlanDetails);
                                    if (objPlanDetails.PlanDuration.ToLower() == "yearly")
                                    {
                                        objSubModel.DtEndAt = PaymentHelper.DateTimeToUnixTimeStampUTC(DateTime.Today.AddYears(objPlanDetails.PlanInterval));
                                    }
                                    else if (objPlanDetails.PlanDuration.ToLower() == "monthly")
                                    {
                                        objSubModel.DtEndAt = PaymentHelper.DateTimeToUnixTimeStampUTC(DateTime.Today.AddMonths(objPlanDetails.PlanInterval));
                                    }
                                    else if (objPlanDetails.PlanDuration.ToLower() == "weekly")
                                    {
                                        int intDays = objPlanDetails.PlanInterval * 7;
                                        objSubModel.DtEndAt = PaymentHelper.DateTimeToUnixTimeStampUTC(DateTime.Today.AddDays(intDays));
                                    }
                                    else if (objPlanDetails.PlanDuration.ToLower() == "daily")
                                    {
                                        objSubModel.DtEndAt = PaymentHelper.DateTimeToUnixTimeStampUTC(DateTime.Today.AddDays(objPlanDetails.PlanInterval));
                                    }
                                    objSubModel.DtTrialStartAt = 0;
                                    objSubModel.DtTrialEndAt = 0;
                                }
                            }
                            else
                            {
                                errorsMessages.Add(LearnerAppConstants.RAZORPAY_UNABLE_TO_CREATE_SUBSCRIPTION);
                            }
                        }
                    }
                }
                else
                {
                    errorsMessages.Add(LearnerAppConstants.RAZORPAY_INVALID_PLAN);
                }
            }
            catch
            {
                throw;
            }
            return objSubModel;
        }



        #region Recurly Payment

        public async Task<RecurlyCardCheckoutResponse> RecurlyCardCheckout(RecurlyCardCheckout recurlyCardCheckout, List<string> errorsMessages)
        {
            try
            {
                bool IsError = true;
                RecurlyCardCheckoutResponse response = new();
                BillingObject billingObject = new();

                if (recurlyCardCheckout.PlanCode <= 0)
                {
                    errorsMessages.Add(LearnerAppConstants.INVALID_PLANCODE);
                }
                else
                {
                    try
                    {
                        Account accountExists = await _reculyClient.GetAccountAsync("code-" + recurlyCardCheckout.AccountID.ToString());
                        response.RecurlyID = accountExists.Id;
                        IsError = false;
                    }
                    catch
                    {
                        try
                        {
                            var accountReq = new AccountCreate()
                            {
                                Code = recurlyCardCheckout.AccountID.ToString(),
                                FirstName = recurlyCardCheckout.FirstName,
                                LastName = recurlyCardCheckout.LastName,
                                Email = recurlyCardCheckout.Email,
                                Address = new Address()
                                {
                                    Street1 = recurlyCardCheckout.Address1,
                                    Street2 = recurlyCardCheckout.Address2,
                                    City = recurlyCardCheckout.City,
                                    Region = recurlyCardCheckout.State,
                                    PostalCode = recurlyCardCheckout.PostalCode,
                                    Country = recurlyCardCheckout.Country,
                                    Phone = recurlyCardCheckout.Phone
                                }
                            };
                            Account account = await _reculyClient.CreateAccountAsync(accountReq);
                            response.RecurlyID = account.Id;
                            IsError = false;
                        }
                        catch (Recurly.Errors.Validation ex)
                        {
                            errorsMessages.Add($"Failed validation: {ex.Error.Message}");
                            IsError = true;
                        }
                        catch (Recurly.Errors.ApiError ex)
                        {
                            // Use ApiError to catch a generic error from the API
                            logger.Debug(ex.Message.ToString());
                            errorsMessages.Add(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE);
                            IsError = true;
                        }
                    }

                    if (IsError == false)
                    {

                        var Token = await PaymentHelper.GetRecurlyLoginToken(recurlyCardCheckout.AccountID, _paymentConfig.recurly_apiKey, _paymentConfig.recurly_domainName, _paymentConfig.recurly_loginTokenApi);

                        if (Token.ToString().IsNullOrEmptyOrWhiteSpace())
                        {
                            errorsMessages.Add(LearnerAppConstants.UNABLE_TO_GENERATE_TOKEN);
                        }
                        else
                        {
                            var Coupon = await PaymentHelper.GetActiveCoupon(recurlyCardCheckout.AccountID, _paymentConfig.recurly_apiKey, _paymentConfig.recurly_domainName, _paymentConfig.recurly_getActiveCoupon);
                            var Signature = PaymentHelper.GenerateRecurlyJSSignature(recurlyCardCheckout.PlanCode.ToString(), recurlyCardCheckout.AccountID.ToString(), recurlyCardCheckout.CurrencyCode, _paymentConfig.recurly_apiKey);

                            if (Signature.Length <= 0)
                            {
                                errorsMessages.Add(LearnerAppConstants.UNABLE_TO_GENERATE_SIGNATURE);
                            }
                            else
                            {
                                var PlanDetails = await _paymentRepo.FetchRecurlyPlans(recurlyCardCheckout.PlanCode);

                                if (PlanDetails == null || PlanDetails.Name.IsNullOrEmptyOrWhiteSpace())
                                {
                                    errorsMessages.Add(LearnerAppConstants.INVALID_PLANNAME);
                                }
                                else
                                {
                                    response.PlanName = PlanDetails.Name;
                                    var billingInfo = await PaymentHelper.GetBillingInformation(recurlyCardCheckout.AccountID, _paymentConfig.recurly_apiKey, _paymentConfig.recurly_domainName, _paymentConfig.recurly_getBillingInfo);

                                    if (billingInfo.FirstName != null && billingInfo.Address1 != null && billingInfo.City != null && billingInfo.Country != null)
                                    {
                                        response.BillingInfo = billingInfo;
                                    }
                                    else
                                    {
                                        billingObject.FirstName = recurlyCardCheckout.FirstName;
                                        billingObject.LastName = recurlyCardCheckout.LastName;
                                        billingObject.Address1 = recurlyCardCheckout.Address1;
                                        billingObject.Address2 = recurlyCardCheckout.Address2;
                                        billingObject.City = recurlyCardCheckout.City;
                                        billingObject.State = recurlyCardCheckout.State;
                                        billingObject.Country = recurlyCardCheckout.Country;
                                        billingObject.PostalCode = recurlyCardCheckout.PostalCode;
                                        response.BillingInfo = billingObject;
                                    }
                                }
                            }
                            response.TokenID = Token;
                            response.PlanCode = recurlyCardCheckout.PlanCode;
                            response.CurrencyCode = recurlyCardCheckout.CurrencyCode;
                            response.Coupon = Coupon;
                            response.AccountID = recurlyCardCheckout.AccountID;
                            response.Signature = Signature;
                        }
                    }
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        #endregion

    }
}
