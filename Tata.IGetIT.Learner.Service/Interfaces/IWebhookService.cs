using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface IWebhookService
    {  
        public Task<string> RazorpayWebhookData(string webhookData,string Signature);
        public Task<bool> RecurlyWebhookData(string responseString, string userNameAndPassword);
    }
}
