using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;
namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public PaymentRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<IEnumerable<Currency>> CurrencyList()
        {
            try
            {
                var param = new Dictionary<string, object>();
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_CURRENCY,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<Currency>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }



        public Task<int> GetPlanID(string planCode)
        {
            var param = new Dictionary<string, object>
            {
                { "@pname", planCode }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.PLAN_GET_PLANID,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<string> PlanPurchaseResponse(int ID, int UserID, string planname, string plandescription, string planId, double amount, int quantity,
        int noOfUsers, string paymentId, string paymentStatus, string transactionID, string FirstName, string MiddleName, string LastName, string email,
        string address, string contactNumber, string order_id, int cartId)
        {
            var param = new Dictionary<string, object>
            {
                { "@ID", ID },
                { "@UserID", UserID },
                { "@PlanName", planname },
                { "@PlanDescription", plandescription },
                { "@PlanID", planId },
                { "@PlanAmount", amount },
                { "@Quantity", quantity },
                { "@NoOfUsers", noOfUsers },
                { "@PaymentID", paymentId },
                { "@PaymentStatus", paymentStatus },
                { "@TransactionID", transactionID },
                { "@FirstName", FirstName },
                { "@MiddleName", MiddleName },
                { "@LastName", LastName },
                { "@Email", email },
                { "@Address", address },
                { "@ContactNo", contactNumber },
                { "@orderID", order_id },
                { "@cartId", cartId }

            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.PLAN_SAVEPURCHASED_PLANINFO,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncString(queryInfo);
        }

        public Task<int> SubscriptionResponse(int AccountID, int subID, int quantity, string startDate, string endDate, int cost, int subName, string ecomID,
     string TrialStart, string TrialEnd, int cartId, string PaymentID)
        {
            var param = new Dictionary<string, object>
            {
                { "@AccountID", AccountID },
                { "@SubID", subID },
                { "@Quantity", quantity },
                { "@StartDate", startDate },
                { "@ExpireDate", endDate },
                { "@Cost", cost },
                { "@SubType", subName },
                { "@SalesDetailID", ecomID },
                { "@TrialStartAt", TrialStart },
                { "@TrialEndAt", TrialEnd},
                { "@cartId", cartId},
                { "@PaymentID", PaymentID}                
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.SUB_INSERT_SALESFULFILLMENT_V2,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<bool> CancelSubscription(string subscriptionId, string action)
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

            return _databaseOperations.ExecuteScalarAsyncBool(queryInfo);
        }

        public Task<PlanObject> FetchRecurlyPlans(int PlanCode)
        {
            try
            {
                var param = new Dictionary<string, object>
            {

                { "@PlanCode", PlanCode }
            };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.PLAN_GETACTIVE_PLANS,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<PlanObject>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
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

        public Task<bool> ExpireSubscription(string recurlyID)
        {
            var param = new Dictionary<string, object>
            {
                { "@RecurlyID", recurlyID }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.SUB_EXPIREFULFILLMENT,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncBool(queryInfo);
        }

       

    }
}
