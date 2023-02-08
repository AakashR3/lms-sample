using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    /// <summary>
    /// Error response to be populated for any Model errors
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Errors to be populated 
        /// </summary>
        public List<string> Errors { get; set; }
        public ErrorResponse(List<string> Errors)
        {
            this.Errors = Errors;
        }
    }
}
