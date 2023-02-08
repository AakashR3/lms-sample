using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class WebhookRepo : IWebhookRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public WebhookRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<int> UpdatePayment(string accID, string payMethod, int tax, int razorpayFee, string transactionID, string paymentId, string status,
        string orderId, int amount)
        {
            var param = new Dictionary<string, object>
            {
                { "@accID", accID },
                { "@payMethod", payMethod },
                { "@tax", tax },
                { "@razorpayFee", razorpayFee },
                { "@transactionID", transactionID },
                { "@paymentID", paymentId },
                { "@status", status },
                { "@amount", amount }
        };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.PLAN_UPDATE_PAYMENT,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }


        public Task<int> Get_SubscriptionID(string planName)
        {
            var param = new Dictionary<string, object>
            {
                { "@planName", planName }

             };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.PLAN_GET_SUBSCRIPTIONID,
                Parameters = param
            };
            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> UpdateSubscription(string ecomID, int subID, DateTime endDate, int quantity)
        {
            var param = new Dictionary<string, object>
            {
                { "@RecurlyID", ecomID },
                { "@NewPlanID", subID },
                { "@NewExpDate", endDate },
                { "@NewQty", quantity }                

             };
            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.SUB_UPDATE_FULFILLMENT,
                Parameters = param
            };
            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> CancelSubscription(string subscriptionId, string action)
        {
            var param = new Dictionary<string, object>
            {
                { "@RecurlyID", subscriptionId },
                { "@Action", action },
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.SUB_CANCEL_SUBSCRIPTION,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }



        public Task<int> InsertSalesFulfillment(int AccountID, int subID, int quantity, string startDate, string endDate, int cost,
        int subType, string ecomID, Nullable<DateTime> TrialStart, Nullable<DateTime> TrialEnd)
        {
            var param = new Dictionary<string, object>
            {
                { "@AccountID", AccountID },
                { "@SubID", subID },
                { "@Quantity", quantity },
                { "@StartDate", startDate },
                { "@ExpireDate", endDate },
                { "@Cost", cost },
                { "@SubType", subType },
                { "@SalesDetailID", ecomID },
                { "@TrialStartAt", TrialStart },
                { "@TrialEndAt", TrialEnd }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.SUB_INSERT_SALESFULFILLMENT,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> GetNewSubID(int subID,int RecurlID)
        {
            var param = new Dictionary<string, object>
            {
                { "@subID", subID },
                { "@RecurlID", RecurlID }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GET_PAID_TRIAL_SUBID,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }


        public Task<int> ReactivateSubscription(string ecomID, string action)
        {
            var param = new Dictionary<string, object>
            {
                { "@RecurlyID", ecomID },
                { "@Action", action },
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.SUB_CANCEL_SUBSCRIPTION,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<int> ExpireSubscription(string recurlyID)
        {
            var param = new Dictionary<string, object>
            {
                { "@recurlyID", recurlyID }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.SUB_EXPIREFULFILLMENT,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<bool> RecurlyVerifyAutoRenew(string recurlyID, DateTime expiryDate, int dollerCost, int planCode, int accountId, int quantity)
        {
            var param = new Dictionary<string, object>
            {
                { "@RecurlyID", recurlyID },
                { "@ExpDate", expiryDate },
                { "@Cost", dollerCost },
                { "@PlanCode", planCode },
                { "@AccountID", accountId },
                { "@Quantity", quantity }

        };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.SUB_VERIFYAUTORENEW,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncBool(queryInfo);
        }
    }
}
