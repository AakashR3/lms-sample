using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{

    /// <summary>
    /// Log Details
    /// </summary>
    public class LogDetails
    {


        public LogDetails(LogType logType,[CallerMemberName] string callerMemberName = "", [CallerFilePath] string FilePath = "", [CallerLineNumber] int LineNo=0)
        {
            this.logType = logType;
            CallerMethod = callerMemberName;
            //CallerFilePath=FilePath;
            //CallerLineNo = LineNo;
        }

        public LogType logType { get; set; }
        public string AppName { get; set; }
        public string AppUrl { get; set; }
        public string AppEnvironment { get; set; }
        public Exception AppException { get; set; }

        public object RequestParam { get; set; }

        /// <summary>
        /// Returns inner exception if there is an exception occured
        /// </summary>
        public Exception InnerException
        {
            get
            {
                if (AppException != null)
                {
                    return AppException.InnerException;
                }
                else
                {
                    return null;
                }

            }
        }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public string Code { get; set; }

        public string CallerMethod { get; set; }

        public string CallerFilePath { get; set; }

        public int CallerLineNo { get; set; }

        public List<KeyValuePair<string, object>> AdditionalInfo { get; set; }
    }

    public enum LogType
    {
        Fatal,
        Error,
        Warn,
        Info,
        Debug,
        Trace
    }
}
