using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models.Configurations
{
    /// <summary>
    /// Configuration model for Social login (linkedin,google, etc...)
    /// </summary>
    public class SocialLoginConfig
    {
        /// <summary>
        /// Authorization Url
        /// </summary>
        public string AuthUrl { get; set; }
        public string ProfileAPIurl { get; set; }
        public string EmailAPIurl { get; set; }
        public string RedirectUrl { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string Scope { get; set; }
        public string State { get; set; }
        public string Granttype { get; set; }

    }
}
