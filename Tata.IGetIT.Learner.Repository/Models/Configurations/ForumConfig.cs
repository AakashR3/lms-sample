using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models.Configurations
{ 
    
   /// <summary>
   /// Configuration model for Discussion Forum
   /// </summary>
    public class ForumConfig
    {
        /// <summary>
        /// ForumConfig
        /// </summary>
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
    }
}
