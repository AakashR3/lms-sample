using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteMeetingRequest
    {
        /// <summary>
        /// TrainingSessionID
        /// </summary>
        [Required]
        public int TrainingSessionID { get; set; }
        /// <summary>
        /// Event Id
        /// </summary>
        [Required]
        public string EventID { get; set; }
        /// <summary>
        /// Meeting Id
        /// </summary>
        [Required]
        public string MeetingID { get; set; }
    }
}
