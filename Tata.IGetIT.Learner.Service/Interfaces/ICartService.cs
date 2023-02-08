namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ICartService
    {
        public Task<IEnumerable<Cart>> CartList(int UserID);
        public Task<IEnumerable<PlanInfo>> GetPlanSubscriptionInfo(int SubscriptionID, String CourseType);
        public Task<IEnumerable<Countries>> CountryList();
        public Task<ShippingInfo> ShippingInfo(int UserID, List<string> ErrorsMessages);
        public Task<int> AddToCart(Cart cart, List<string> ErrorsMessages);
        public Task<int> DeleteCartItem(DeleteCart cart, List<string> ErrorsMessages);

        public Task<int> TrialValidation(TrialValidation trial, List<string> ErrorsMessages);
    }
}
