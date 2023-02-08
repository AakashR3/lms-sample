using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class AttendanceReport
    {
        public AttendanceReport()
        {
            Participants = new List<MeetingParticipant>();
        }
        public string? Id { get; set; }
        public int? TotalParticipantCount { get; set; }
        public DateTimeOffset? StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public List<MeetingParticipant> Participants { get; set; }

    }

    public class MeetingParticipant
    {
        public MeetingParticipant()
        {
            Attendances = new List<MeetingAttendance>();
        }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public List<MeetingAttendance> Attendances { get; set; }
        public int? TotalDurationInSeconds { get; set; }
    }

    public class MeetingAttendance
    {
        public DateTimeOffset? JoinedAt { get; set; }
        public DateTimeOffset? LeftAt { get; set; }
        public int? DurationInSeconds { get; set; }
    }
}
