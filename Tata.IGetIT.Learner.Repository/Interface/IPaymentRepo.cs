using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IPaymentRepo
    {
        public Task<IEnumerable<Currency>> CurrencyList();
        
        public Task<int> GetPlanID(string planCode);
        public Task<string> PlanPurchaseResponse(int ID, int UserID, string planname, string plandescription, string planId, double amount, int quantity,
        int noOfUsers, string paymentId, string paymentStatus, string transactionID, string FirstName, string MiddleName, string LastName, string email,
        string address, string contactNumber, string order_id,int cartId);
        public Task<int> SubscriptionResponse(int AccountID, int subID, int quantity, string startDate, string endDate, int cost, int subName, string ecomID,
        string TrialStart, string TrialEnd, int cartId,string PaymentID);

        public Task<bool> CancelSubscription(string subscriptionId, string action);

        public Task<PlanObject> FetchRecurlyPlans(int PlanCode);

       
    }
}


