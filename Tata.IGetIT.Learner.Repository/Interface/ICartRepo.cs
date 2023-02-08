using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ICartRepo
    {
        public Task<IEnumerable<Cart>> CartList(int UserID);
        public Task<IEnumerable<PlanInfo>> GetPlanSubscriptionInfo(int SubscriptionID, String CourseType);
        public Task<IEnumerable<Countries>> CountryList();
        public Task<ShippingInfo> ShippingInfo(int UserID);
        public Task<int> AddToCart(Cart cart);
        public Task<int> DeleteCartItem(DeleteCart cart);

        public Task<int> TrialValidation(TrialValidation trial);
    }
}
