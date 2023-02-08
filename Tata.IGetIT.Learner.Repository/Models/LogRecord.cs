using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Repository.Models
{
    class LogRecord
    {
        string Message { get; set; }

        MessageType messageType { get; set; }

        public LogRecord(string message, MessageType messageType)
        {
            this.Message = message;
            this.messageType = messageType;
        }
    }
    internal enum MessageType
    {
        Error,
        Warn,
        Info
    }
}
