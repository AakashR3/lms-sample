using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ITeamsService
    {

        /// <summary>
        /// Create microsoft teams meeting
        /// </summary>
        /// <param name="request">CreateMeetingRequest</param>
        /// <returns></returns>
        Task<Event> AddEventInCalendarAsync(OnlineMeeting request,string TimeZone);

        /// <summary>
        /// Create microsoft teams meeting
        /// </summary>
        /// <param name="request">CreateMeetingRequest</param>
        /// <returns></returns>
        Task<OnlineMeeting> CreateMeetingAsync(CreateMeetingRequest request);
        /// <summary>
        /// Update microsoft teams meeting
        /// </summary>
        /// <param name="request">UpdateMeetingRequest</param>
        /// <returns></returns>
        Task<OnlineMeeting> UpdateMeetingAsync(UpdateMeetingRequest request);


        /// <summary>
        /// Update microsoft teams Event
        /// </summary>
        /// <param name="onlineMeeting">UpdateEventInCalendarAsync</param>
        /// <returns></returns>
        Task<Event> UpdateEventInCalendarAsync(string EventId,OnlineMeeting onlineMeeting,string TimeZone);



        /// <summary>
        /// Delete microsoft teams meeting
        /// </summary>
        /// <param name="request">DeleteMeetingRequest</param>
        /// <returns></returns>
        Task DeleteMeetingAsync(DeleteMeetingRequest request);
        /// <summary>
        /// Get microsoft teams meeting Transcript
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        Task GetMeetingTranscriptAsync(string meetingId);
        /// <summary>
        /// Get microsoft teams meeting recording
        /// </summary>
        /// <returns></returns>
        Task GetMeetingRecordingAsync();
        /// <summary>
        /// Get meeting attendance report
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        Task<List<AttendanceReport>> GetMeetingAttendanceAsync(string meetingId);

    }
}
