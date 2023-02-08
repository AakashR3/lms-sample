using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Service.Helpers
{
    public static class ValidationHelper
    {

    }

    public class ValidationMessage
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public MessageType MessageType { get; set; }

        public ValidationMessage(string Message,int code,MessageType messageType)
        {
            this.Message = Message;
            this.Code = Code;
            this.MessageType = MessageType;
        }

    }

    public enum MessageType
    {
        Error,
        Warning,
        Information
    }
}


