using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models.Configurations
{
    /// <summary>
    /// Configuration model for Outgoing mail server(SMTP)
    /// </summary>
    public class SmtpConfig
    {
        /// <summary>
        /// User name for SMTP
        /// </summary>
        public string SmtpUser { get; set; }

        /// <summary>
        /// Display name for outgoing email address
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// User credentials for outgoing Email address
        /// </summary>
        public string SmtpPass { get; set; }

        /// <summary>
        /// Email host
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// Email port
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// Outgoing email address
        /// </summary>
        public string FromEmailId { get; set; }
    }


}
