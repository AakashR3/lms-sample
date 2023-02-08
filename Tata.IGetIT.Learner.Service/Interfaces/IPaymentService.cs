using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface IPaymentService
    {
        public Task<IEnumerable<Currency>> CurrencyList();
        public Task<RazorpayModel> RazorpayCheckout(PaymentInitiate payment, List<string> ErrorsMessages);
        public Task<bool> RazorpayResponse(RazorpayResponse payment, List<string> ErrorsMessages);

        public Task<bool> CancelSubscription(CancelSubscription cancelSubscription, List<string> ErrorsMessages);

        public Task<RecurlyCardCheckoutResponse> RecurlyCardCheckout(RecurlyCardCheckout recurlyCardCheckout, List<string> ErrorsMessages);


        



    }
}
