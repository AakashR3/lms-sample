using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models.Configurations;

namespace Tata.IGetIT.Learner.Service.Helpers
{
    public static class Common
    {
        public static string ClientIPAddress(HttpContext context)
        {
            try
            {
                string PublidIPAddress = string.Empty;
                if (!string.IsNullOrEmpty(context.Request.Headers["X-Forwarded-For"]))
                {
                    PublidIPAddress = context.Request.Headers["X-Forwarded-For"];
                }
                else
                {
                    PublidIPAddress = context.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
                }

                if (PublidIPAddress.Contains(':') == true)
                {
                    PublidIPAddress = PublidIPAddress.Split(':')[0];
                }

                return PublidIPAddress;
            }
            catch
            {
                return "";
            }
        }
    }
}
