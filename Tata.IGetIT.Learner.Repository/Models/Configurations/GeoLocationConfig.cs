using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models.Configurations
{
    public class GeoLocationConfig
    {
        /// <summary>
        /// Azure Map Configuration and Get Public IP Address API 
        /// </summary>
        /// 

        public string AzureMapKey { get; set; }
        public string GeoLocationApi { get; set; }
        public string PublicIPApi { get; set; }
    }
}
