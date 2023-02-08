using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    /// <summary>
    /// Update meeting request
    /// </summary>
    public class UpdateMeetingRequest
    {
        /// <summary>
        /// TrainingID
        /// </summary>
        [Required]
        public int TrainingID { get; set; }

        /// <summary>
        /// Training SessionID
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
        /// Begin time
        /// </summary>
        [Required]
        public DateTimeOffset StartDateTime { get; set; }
        /// <summary>
        /// End time
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
        /// Attendees
        /// </summary>
        [Required]
        public List<string> Attendees { get; set; }
        /// <summary>
        /// Time zone
        /// </summary>
        [Required]
        public string TimeZone { get; set; }
    }
}
