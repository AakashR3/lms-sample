using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _cartRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public CartService(ICartRepo cartRepo)
        {
            if (cartRepo == null)
            {
                new ArgumentNullException("CartRepo cannot be null");
            }
            _cartRepo = cartRepo;
        }

        public async Task<int> AddToCart(Cart cart, List<string> errorsMessages)
        {
            try
            {
                var result = await _cartRepo.AddToCart(cart);

                switch (result)
                {
                    case 1:
                        break;

                    case -1:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_CART_INSERT);
                        break;

                    case -2:
                        errorsMessages.Add(LearnerAppConstants.USER_ALREADY_SUBSCRIBED);
                        break;

                    case -3:
                        errorsMessages.Add(LearnerAppConstants.USER_ALREADY_SUBSCRIBED);
                        break;
                    case -4:
                        errorsMessages.Add(LearnerAppConstants.INVALID_PRURCHASE_TYPE);
                        break;
                    case -5:
                        errorsMessages.Add(LearnerAppConstants.INVALID_ARGUMENTS);
                        break;
                    case 0:
                        errorsMessages.Add(LearnerAppConstants.UNABLE_INSERT_CART_ITEM);
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_CART_INSERT);
                        break;
                }

                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> DeleteCartItem(DeleteCart cart, List<string> errorsMessages)
        {
            try
            {
                var result = await _cartRepo.DeleteCartItem(cart);

                switch (result)
                {
                    case 1:
                        break;

                    case -1:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_CART_DELETE);
                        break;

                    case -2:
                        errorsMessages.Add(LearnerAppConstants.CART_ITEM_DOESNOT_EXIST);
                        break;

                    default:
                        errorsMessages.Add(LearnerAppConstants.ISSUE_CART_DELETE);
                        break;
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Cart>> CartList(int UserID)
        {
            try
            {
                return await _cartRepo.CartList(UserID);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Countries>> CountryList()
        {
            try
            {
                return await _cartRepo.CountryList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ShippingInfo> ShippingInfo(int UserID, List<string> ErrorsMessages)
        {
            ShippingInfo shippingInfo = new ShippingInfo();
            var response = await _cartRepo.ShippingInfo(UserID);
            if (response != null)
            {
                shippingInfo = response;
            }
            else
            {
                ErrorsMessages.Add(LearnerAppConstants.SHIPPING_INFO_NOT_FOUND);
            }
            return shippingInfo;
        }

        public async Task<IEnumerable<PlanInfo>> GetPlanSubscriptionInfo(int SubscriptionID, string CourseType)
        {
            try
            {
                return await _cartRepo.GetPlanSubscriptionInfo(SubscriptionID, CourseType);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TrialValidation(TrialValidation trial, List<string> ErrorsMessages)
        {
            try
            {

                int result = 0;
                if (trial.CartID == 0)
                {
                    ErrorsMessages.Add(LearnerAppConstants.RAZORPAY_CART_ID_REQUIRED);
                }
                else
                {
                    result = await _cartRepo.TrialValidation(trial);
                    if (result == 0)
                    {

                    }
                    else if (result == 1)
                    {
                        ErrorsMessages.Add(LearnerAppConstants.USER_IN_TRIAL_ACCOUNT);
                    }
                    else if (result == 2)
                    {
                        ErrorsMessages.Add(LearnerAppConstants.INVALID_CARTID);
                    }
                    else if (result == 3)
                    {
                        ErrorsMessages.Add(LearnerAppConstants.INVALID_USERID);
                    }
                    else if (result == 4)
                    {
                        ErrorsMessages.Add(LearnerAppConstants.PLAN_PURCHASE_NOT_TRIAL);
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
