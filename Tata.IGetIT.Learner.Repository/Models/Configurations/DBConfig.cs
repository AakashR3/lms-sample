using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models.Configurations
{

    /// <summary>
    /// Database configuration
    /// </summary>
    public class DBConfig
    {
        /// <summary>
        /// User ActualConnectionString property instead.
        /// Connection string without credentials
        /// </summary>
        public string Connectionstring { get; set; }
        public string Password { get; set; }

        private string _actualConnectionString;

        /// <summary>
        /// Full connection string that appends password from the password vault
        /// </summary>
        public string ActualConnectionString
        {
            get
            {
                return _actualConnectionString = Connectionstring + (Connectionstring.EndsWith(";") ? "" : ";") + $"Password={Password}";
            }
        }
    }
}
