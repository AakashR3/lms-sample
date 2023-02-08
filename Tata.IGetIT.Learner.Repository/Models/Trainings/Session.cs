using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class Session
    {
        public int SNo { get; set; }
        public int TrainingSessionID { get; set; }
        public int TrainingID { get; set; }
        public string TrainingName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsActive { get; set; }
        public int Recurrence { get; set; }
        public int PlatformTypeID { get; set; }
        public string InstructorName { get; set; }
        public string Location { get; set; }
        public string MeetingID { get; set; }
        public string EventID { get; set; }
        public string RecordingUrl { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public string Attendee { get; set; }
        public List<string> Attendees { get; set; }
        public string IsPrivileged { get; set; } = string.Empty;


    }

}
