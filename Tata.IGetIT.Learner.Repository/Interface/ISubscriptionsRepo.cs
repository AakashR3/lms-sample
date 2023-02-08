using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ISubscriptionsRepo
    {
        public Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(int UserID);

        public Task<int> UpdateCartPurchase(int CartID);

        public Task<int> UpdateMultipleCartPurchase(string CartIDs,int UserId);
        public Task<IEnumerable<UsersPurchasedHistory>> GetUserPurchasedHistoryAsync(int UserID);
        public Task<ProfessionalBundle> GetProfessionalBundle(string CountryCode);
        public Task<IEnumerable<AvailableSubscription>> GetAvailableSubscription(string CountryCode, int CategoryID);
        public Task<SubscriptionDetail> GetSubscriptionDetail(string SubscriptionID,int UserID);

        public Task<InvoiceDetails> BillingInfo(string SubscriptionID,string Currency);
    }
}
