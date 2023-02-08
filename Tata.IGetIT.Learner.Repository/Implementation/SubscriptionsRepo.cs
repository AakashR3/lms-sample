using System.Data;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class SubscriptionsRepo : ISubscriptionsRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public SubscriptionsRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public async Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(int UserID)
        {

            var param = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };
            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GET_SUBSCRIPTION_DETAILS,
                Parameters = param
            };

            return await _databaseOperations.GetMultipleRecords<UserSubscription>(queryInfo);

        }


        public async Task<IEnumerable<UsersPurchasedHistory>> GetUserPurchasedHistoryAsync(int UserID)
        {

            var param = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };
            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GET_USERS_PURCHASED_HISTORY,
                Parameters = param
            };

            return await _databaseOperations.GetMultipleRecords<UsersPurchasedHistory>(queryInfo);

        }

        /// <summary>
        /// Insert OTP details
        /// </summary>
        /// <param name="CartID"></param>
        /// <returns></returns>
        public async Task<int> UpdateCartPurchase(int CartID)
        {
            var param = new Dictionary<string, object>
            {
                { "@CartID", CartID }
            };

            QueryInfo queryInfo = new QueryInfo()
            {

                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.UPDATE_CART_PURCHASE,
                Parameters = param
            };

            return await _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public async Task<IEnumerable<AvailableSubscription>> GetAvailableSubscription(string CountryCode, int CategoryID)
        {

            var param = new Dictionary<string, object>
            {
                { "@CountryCode", CountryCode },
                { "@CategoryID", CategoryID }
            };
            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetAvailableSubscription,
                Parameters = param
            };

            return await _databaseOperations.GetMultipleRecords<AvailableSubscription>(queryInfo);
        }
        /// <summary>
        ///Recurly Update Multiple Cart Purchase
        /// </summary>
        /// <param name="CartIDs"></param>
        /// <returns></returns>
        public async Task<int> UpdateMultipleCartPurchase(string CartIDs,int UserId)
        {
            var param = new Dictionary<string, object>
            {
                { "@CartID", CartIDs },
                { "@UserId", UserId }
            };

            QueryInfo queryInfo = new QueryInfo()
            {

                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.UPDATE_CART_PURCHASE,
                Parameters = param
            };

            return await _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public async Task<ProfessionalBundle> GetProfessionalBundle(string CountryCode)
        {
            var param = new Dictionary<string, object>
            {
                { "@CountryCode", CountryCode }
            };

            QueryInfo queryInfo = new QueryInfo()
            {

                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetProfessionalBundle,
                Parameters = param
            };

            return await _databaseOperations.GetFirstRecordAsync<ProfessionalBundle>(queryInfo);
        }


        /// <summary>
        /// Get Billing Information
        /// </summary>
        /// <param name="SubscriptionID"></param>
        /// <returns></returns>
        public Task<InvoiceDetails> BillingInfo(string SubscriptionID, string Currency)
        {
            try
            {
                var param = new Dictionary<string, object>
            {
                { "@SubscriptionID", SubscriptionID },
                { "@Currency", Currency },   
            };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.BILLING_INFO_FOR_INVOICE,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<InvoiceDetails>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<SubscriptionDetail> GetSubscriptionDetail(string SubscriptionID,int UserID)
        {
            var conn = _databaseOperations.CreateConnection();
            try
            {
                var queryParameters = new Dictionary<string, object>
                {
                    { "@SubscriptionID", SubscriptionID },
                    { "@UserID", UserID }
                };

                QueryInfo queryInfo = new()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.SubscriptionDetail,
                    Parameters = queryParameters
                };
                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);

                if (results != null)
                {
                    try
                    {
                        var subscription = results.Read<SkillAdvisor_Subscription>().FirstOrDefault();
                        var courses = results.Read<SkillAdvisor_Courses>().ToList();
                        var assessments = results.Read<SkillAdvisor_Assessments>().ToList();

                        SubscriptionDetail subscriptionDetail = new()
                        {
                            skillAdvisor_Subscription = subscription,
                            skillAdvisor_Courses = courses,
                            SkillAdvisor_Assessments = assessments,
                        };

                        return subscriptionDetail;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
    }
}
