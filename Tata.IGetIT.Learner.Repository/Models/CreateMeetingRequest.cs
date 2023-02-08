using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    /// <summary>
    /// Create microsoft team meeting
    /// </summary>
    public class CreateMeetingRequest
    {
        /// <summary>
        /// TrainingID
        /// </summary>
        [Required]
        public int TrainingID { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Start DateTime
        /// </summary>
        [Required]
        public DateTimeOffset StartDateTime { get; set; }
        /// <summary>
        /// End DateTime
        /// </summary>
        [Required]
        public DateTimeOffset EndDateTime { get; set; }

        /// <summary>
        /// Recurrence
        /// </summary>
        [Required]
        public int Recurrence { get; set; }

        /// <summary>
        /// Location
        /// </summary>
        [Required]
        public string Location { get; set; }

        /// <summary>
        /// PlatformType
        /// </summary>
        [Required]
        public int PlatformTypeID { get; set; }

        /// <summary>
        /// InstructorName
        /// </summary>
        [Required]
        public string InstructorName { get; set; }

        /// <summary>
        /// List of Attendees
        /// </summary>
        [Required]
        public List<string> Attendees { get; set; }
        /// <summary>
        /// Timezone
        /// </summary>
        [Required]
        public string TimeZone { get; set; }


       

       


    }
}
