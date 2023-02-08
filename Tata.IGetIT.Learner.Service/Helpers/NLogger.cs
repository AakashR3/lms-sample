using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using NLog.Web;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NLog.Fluent;
using System.Reflection;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Helpers;
using System.Runtime.CompilerServices;

namespace Tata.IGetIT.Learner.Service.Helpers
{

    public static class NLogger
    {

        public static void LogDetails(this NLog.ILogger logger, LogDetails ld)
        {
            var logEvent = ld.logType switch
            {
                LogType.Fatal => logger.ForFatalEvent(),
                LogType.Error => logger.ForErrorEvent(),
                LogType.Warn => logger.ForWarnEvent(),
                LogType.Info => logger.ForInfoEvent(),
                LogType.Debug => logger.ForDebugEvent(),
                _ => logger.ForTraceEvent(),
            };
            if (ld.AppException != null)
            {
                logEvent = logger.ForErrorEvent();
                logEvent = logEvent.Exception(ld.AppException);
            }


            if (!ld.Message.IsNullOrEmptyOrWhiteSpace())
            {
                logEvent = logEvent.Message(ld.Message);
            }


            if (!ld.MessageCode.IsNullOrEmptyOrWhiteSpace())
            {
                logEvent.Property("MessageCode", ld.AppUrl);
            }


            if (!ld.CallerMethod.IsNullOrEmptyOrWhiteSpace())
            {
                logEvent.Property("Method", ld.CallerMethod);
            }


            if (!ld.CallerFilePath.IsNullOrEmptyOrWhiteSpace())
            {
                logEvent.Property("File", ld.CallerFilePath);
            }


            if (ld.CallerLineNo != 0)
            {
                logEvent.Property("LineNo", ld.CallerLineNo);
            }

            if (ld.InnerException != null)
            {
                logEvent = logEvent.Property<Exception>("Inner Exception", ld.InnerException);
            }


            if ((ld.AdditionalInfo != null) && ld.AdditionalInfo.Any())
            {
                foreach (var kv in ld.AdditionalInfo)
                {
                    logEvent.Property(kv.Key, kv.Value);
                }
            }

            if (!ld.AppUrl.IsNullOrEmptyOrWhiteSpace())
            {
                logEvent.Property("AppUrl", ld.AppUrl);
            }



            logEvent.Log();

        }

        public static void LogTrace(this NLog.ILogger logger, string message)
        {
            logger
             .ForTraceEvent()
             .Message(message)
             .Log();
        }

        public static void LogDebug(this NLog.ILogger logger, string message, string MethodName = null, string url = null)
        {
            logger.ForDebugEvent()
                  .Message(message)
                  .Property("Url", url)
                  .Property("Class", logger.Name)
                  .Property("Method", MethodName)
                  .Log();
        }

        public static void LogInfo(this NLog.ILogger logger, string message)
        {
            logger
                .ForInfoEvent()
                .Message(message)
                .Log();
        }


        public static void LogFatal(this NLog.ILogger logger, string message, Exception exception)
        {
            logger.ForFatalEvent()
                .Exception(exception)
                .Message(message)
                .Log();
        }

        public static void LogError(this NLog.ILogger logger, string message, Exception exception, string url = null)
        {
            logger.ForErrorEvent()
                .Message(message)
                .Property("ExceptionMessage", exception.Message)
                .Property("url", url)
                .Exception(exception)
                .Log();
        }






    }
}
