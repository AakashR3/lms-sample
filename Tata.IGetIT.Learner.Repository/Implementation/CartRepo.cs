using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class CartRepo : ICartRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public CartRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<int> AddToCart(Cart cart)
        {
            var param = new Dictionary<string, object>
            {
                  { "@UserID", cart.UserID },
                  { "@AccountID", cart.AccountID },
                  { "@PlanCode", cart.PlanCode },
                  { "@SubscriptionID", cart.SubscriptionID },
                  { "@IsTrial", cart.IsTrial },
                  { "@PurchaseType", cart.PurchaseType },
                  { "@BasePlan", cart.BasePlan },
                  { "@PPlan", cart.PPlan }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.ADD_CART_ITEM,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }
        public Task<int> DeleteCartItem(DeleteCart cart)
        {
            var param = new Dictionary<string, object>
            {
                  { "@CartID", cart.CartID },
                  { "@UserID", cart.UserID }
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.DELETE_CART_ITEM,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<IEnumerable<Cart>> CartList(int UserID)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", UserID);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_CART_ITEMS,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<Cart>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<Countries>> CountryList()
        {
            try
            {
                var param = new Dictionary<string, object>();
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_COUNTRY_LIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<Countries>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }
        public Task<ShippingInfo> ShippingInfo(int UserID)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", UserID);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_USER_SHIPPING_INFORMATION,
                    Parameters = param
                };

                return _databaseOperations.GetFirstRecordAsync<ShippingInfo>(queryInfo);

            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<PlanInfo>> GetPlanSubscriptionInfo(int SubscriptionID, string CourseType)
        {

            try
            {
                var param = new Dictionary<string, object>();
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_PLAN_SUBSCRIPTION_DETAIL,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<PlanInfo>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<int> TrialValidation(TrialValidation trial)
        {
            var param = new Dictionary<string, object>
            {
                { "@UserID", trial.UserID},
                { "@CartID", trial.CartID },
                { "@IsTrial", trial.IsTrial },
                { "@PurchaseType", trial.PurchaseType}
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.VALIDATE_TRIAL_ACCOUNT,
                Parameters = param
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }
    }
}
