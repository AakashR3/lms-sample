using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ISubscriptionsService
    {
        public Task<IEnumerable<UserSubscription>> GetUserSubscriptionsAsync(int UserID, List<string> ErrorsMessages);
        public Task<RecurlySubscriptionResponse> SubscribeWithRecurlyAsync(RecurlyPurchaseRequest request, List<string> ErrorsMessages);

        public Task<IEnumerable<AvailableSubscription>> GetAvailableSubscription(string CountryCode, int CategoryID, List<string> ErrorsMessages);
        public Task<SubscriptionDetail> GetSubscriptionDetail(string SubscriptionID, int UserID, List<string> ErrorsMessages);
        public Task<IEnumerable<UsersPurchasedHistory>> GetUserPurchasedHistoryAsync(int UserID, List<string> ErrorsMessages);

        public Task<RecurlySubscriptionResponse> MultiSubscriptionWithRecurlyAsync(RecurlyPurchaseRequest request, List<string> ErrorsMessages);
        public Task<ProfessionalBundle> GetProfessionalBundle(string ClientIP, List<string> ErrorsMessages);
        public Task<ProfessionalBundle> GetwwwProfessionalBundle(string CountryCode, List<string> ErrorsMessages);

        public Task<InvoiceResponse> DownloadInvoice(string SubscriptionID, List<string> ErrorsMessages);
    }
}
