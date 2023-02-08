using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IWebhookRepo
    {
        public Task<int> UpdatePayment(string accID, string payMethod, int tax, int razorpayFee, string transactionID, string paymentId, string status,
        string orderId, int amount);
        public Task<int> Get_SubscriptionID(string planName);
        public Task<int> UpdateSubscription(string ecomID, int subID, DateTime endDate, int quantity);
        public Task<int> CancelSubscription(string ecomID, string action);
        public Task<bool> RecurlyVerifyAutoRenew(string recurlyID, DateTime expiryDate,int dollerCost,int planCode,int accountId,int quantity);
        public Task<int> ExpireSubscription(string recurlyID);
        public Task<int> InsertSalesFulfillment(int AccountID, int subID, int quantity, string startDate, string endDate, int cost,
        int subType, string ecomID, Nullable<DateTime> TrialStart, Nullable<DateTime> TrialEnd);
        public Task<int> GetNewSubID(int subID,int RecurlID);
        public Task<int> ReactivateSubscription(string ecomID, string action);

    }
}
