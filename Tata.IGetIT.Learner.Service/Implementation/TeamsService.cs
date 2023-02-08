using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class TeamsService : ITeamsService
    {

        private readonly GraphServiceClient _graphServiceClient;
        private readonly MicrosoftAuthService _microsoftAuthService;
        private readonly string _adminUserId;
        ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly ITrainingsRepo _trainingsRepo;
        public TeamsService(GraphServiceClient graphServiceClient, MicrosoftAuthService microsoftAuthService, string adminUserId)
        {
            _graphServiceClient = graphServiceClient;
            _microsoftAuthService = microsoftAuthService;
            _adminUserId = adminUserId;
        }
        public async Task<OnlineMeeting> CreateMeetingAsync(CreateMeetingRequest request)
        {
            var meeting = new OnlineMeeting
            {
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
                Subject = request.Name,
                LobbyBypassSettings = new LobbyBypassSettings
                {
                    Scope = LobbyBypassScope.Everyone
                },
                RecordAutomatically = true
            };

            AddorUpdateParticipants(meeting, request.Attendees);
            var onlineMeeting = await _graphServiceClient.Users[_adminUserId].OnlineMeetings.Request().AddAsync(meeting);
            
            //var eventResult = await AddEventInCalendarAsync(onlineMeeting, request.TimeZone);
            //TODO: Database operation to maintain meetingId and eventId

            return onlineMeeting;
        }
        public async Task<OnlineMeeting> UpdateMeetingAsync(UpdateMeetingRequest request)
        {
            OnlineMeeting onlineMeeting = new OnlineMeeting(); 

            var meeting = new OnlineMeeting
            {
                StartDateTime = request.StartDateTime,
                EndDateTime = request.EndDateTime,
                Subject = request.Name,
                LobbyBypassSettings = new LobbyBypassSettings
                {
                    Scope = LobbyBypassScope.Everyone
                },
                RecordAutomatically = true
            };

            AddorUpdateParticipants(meeting, request.Attendees);
            try
            {
                onlineMeeting = await _graphServiceClient.Users[_adminUserId].OnlineMeetings[request.MeetingID].Request().UpdateAsync(meeting);
                //_ = await UpdateEventInCalendarAsync(request.EventId, onlineMeeting, request.TimeZone);

                //TODO: Database operations for update meeting info

            }
            catch
            {
                throw;
            }
            return onlineMeeting;
        }
        public async Task DeleteMeetingAsync(DeleteMeetingRequest request)
        {
            await _graphServiceClient.Users[_adminUserId].OnlineMeetings[request.MeetingID].Request().DeleteAsync();
            await _graphServiceClient.Users[_adminUserId].Events[request.EventID].Request().DeleteAsync();

            //TODO: Database operations for delete meeting info
        }
        public async Task GetMeetingTranscriptAsync(string meetingId)
        {
         
            var url = $"https://graph.microsoft.com/beta/users/{_adminUserId}/onlineMeetings/{meetingId}/transcripts";
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            await _microsoftAuthService.AuthenticateRequestAsync(request);

            var result = await client.SendAsync(request);

            // TODO: needs to deserialize the data here.

        }
        public async Task GetMeetingRecordingAsync()
        {
            //var stream = await _graphServiceClient.Me.OnlineMeetings["{onlineMeeting-id}"].Recording.Request().GetAsync();

        }
        public async Task <List<AttendanceReport>> GetMeetingAttendanceAsync(string meetingId)
        {
            var meetingAttendanceReports = new List<AttendanceReport>();

            var reports = await _graphServiceClient.Users[_adminUserId].OnlineMeetings[meetingId].AttendanceReports.Request().GetAsync();

            foreach (var report in reports)
            {
                var meetingReport = new AttendanceReport()
                {
                    Id = meetingId,
                    StartTime = report.MeetingStartDateTime,
                    EndTime = report.MeetingEndDateTime,
                    TotalParticipantCount = report.TotalParticipantCount
                };

                var records = await _graphServiceClient.Users[_adminUserId].OnlineMeetings[meetingId].AttendanceReports[report.Id].Request().Expand("attendanceRecords").GetAsync();

                foreach (var attendanceRecord in records.AttendanceRecords)
                {
                    var participant = new MeetingParticipant()
                    {
                        Email = attendanceRecord.EmailAddress,
                        TotalDurationInSeconds = attendanceRecord.TotalAttendanceInSeconds,
                        Role = attendanceRecord.Role
                    };

                    foreach (var attendanceInterval in attendanceRecord.AttendanceIntervals)
                    {
                        var attendance = new MeetingAttendance()
                        {
                            JoinedAt = attendanceInterval.JoinDateTime,
                            LeftAt = attendanceInterval.LeaveDateTime,
                            DurationInSeconds = attendanceInterval.DurationInSeconds
                        };
                        participant.Attendances.Add(attendance);
                    }
                    meetingReport.Participants.Add(participant);
                }
                meetingAttendanceReports.Add(meetingReport);
            }
            return meetingAttendanceReports;
        }
        private void AddorUpdateParticipants(OnlineMeeting onlineMeeting, List<string> attendees)
        {
            var meetingAttendees = new List<MeetingParticipantInfo>();
            foreach (var attendee in attendees)
            {
                if (!string.IsNullOrEmpty(attendee))
                {
                    meetingAttendees.Add(new MeetingParticipantInfo
                    {
                        Upn = attendee.Trim()
                    });
                }
            }

            if (onlineMeeting.Participants == null)
            {
                onlineMeeting.Participants = new MeetingParticipants();
            };

            onlineMeeting.Participants.Attendees = meetingAttendees;
        }
        public async Task<Event> AddEventInCalendarAsync(OnlineMeeting meeting, string timeZone)
        {
            var userEmail = meeting.Participants.Organizer.Upn;

            var subject = meeting.Subject;
            var startDateTime = meeting.StartDateTime.GetValueOrDefault().DateTime;
            var endDateTime = meeting.EndDateTime.GetValueOrDefault().DateTime;
            var attendees = meeting.Participants.Attendees.Select(x => x.Upn).ToList();
            //var body = meeting.JoinWebUrl;

            string TemplatePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), ApplicationConstants.EMAIL_TEMPLATE_PATH_MEETING_INVITE);
            StreamReader str = new StreamReader(TemplatePath);
            var body = str.ReadToEnd();
            body = body.Replace("{CONTENT}", meeting.JoinWebUrl).Replace("{YEAR}", DateTime.Now.ToString("yyyy")).Replace("{PLATFORM}", "Teams");
            str.Close();


            var newEvent = new Event
            {
                Subject = subject,
                Start = new DateTimeTimeZone
                {
                    DateTime = startDateTime.ToString("o"),
                    TimeZone = timeZone
                },
                End = new DateTimeTimeZone
                {
                    DateTime = endDateTime.ToString("o"),
                    TimeZone = timeZone
                },
                AllowNewTimeProposals = false,
                CreatedDateTime = DateTime.Now
            };

            if (attendees.Count > 0)
            {
                var requiredAttendees = new List<Microsoft.Graph.Attendee>();

                foreach (var email in attendees)
                {
                    requiredAttendees.Add(new Microsoft.Graph.Attendee
                    {
                        Type = AttendeeType.Required,
                        EmailAddress = new EmailAddress
                        {
                            Address = email
                        }
                    });
                }

                newEvent.Attendees = requiredAttendees;
            }


            newEvent.Body = new ItemBody
            {
                Content = body,
                ContentType = BodyType.Html
            };

            //newEvent.Body = new ItemBody
            //{
            //    Content = body,
            //    ContentType = BodyType.Text
            //};

            var eventResponse = await _graphServiceClient.Users[userEmail]
                .Calendar
                .Events
                .Request()
                .AddAsync(newEvent);

            //return true;

            return eventResponse;
        }
        public async Task<Event> UpdateEventInCalendarAsync(string eventId, OnlineMeeting meeting, string timeZone)
        {
            var userEmail = meeting.Participants.Organizer.Upn;
            var subject = meeting.Subject;
            var startDateTime = meeting.StartDateTime.GetValueOrDefault().DateTime;
            var endDateTime = meeting.EndDateTime.GetValueOrDefault().DateTime;
            var attendees = meeting.Participants.Attendees.Select(x => x.Upn).ToList();
            //var body = meeting.JoinWebUrl;

            string TemplatePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), ApplicationConstants.EMAIL_TEMPLATE_PATH_MEETING_INVITE);
            StreamReader str = new StreamReader(TemplatePath);
            var body = str.ReadToEnd();
            body = body.Replace("{CONTENT}", meeting.JoinWebUrl).Replace("{YEAR}", DateTime.Now.ToString("yyyy"))
                .Replace("{PLATFORM}", "Teams");
            str.Close();

            var onlineEvent = await _graphServiceClient.Users[_adminUserId].Events[eventId].Request().GetAsync();
            onlineEvent.Body = new ItemBody
            {
                Content = body,
                ContentType = BodyType.Html
            };

            onlineEvent.CreatedDateTime = DateTime.Now;

            onlineEvent.Subject = subject;
            onlineEvent.Start = new DateTimeTimeZone
            {
                DateTime = startDateTime.ToString("o"),
                TimeZone = timeZone
            };
            onlineEvent.End = new DateTimeTimeZone
            {
                DateTime = endDateTime.ToString("o"),
                TimeZone = timeZone
            };
            onlineEvent.AllowNewTimeProposals = false;
            onlineEvent.CreatedDateTime = DateTime.Now;

            var requiredAttendees = new List<Microsoft.Graph.Attendee>();

            foreach (var email in attendees)
            {
                requiredAttendees.Add(new Microsoft.Graph.Attendee
                {
                    Type = AttendeeType.Required,
                    EmailAddress = new EmailAddress
                    {
                        Address = email
                    }
                });
            }
            onlineEvent.Attendees = requiredAttendees;
            return await _graphServiceClient.Users[_adminUserId].Events[eventId].Request().UpdateAsync(onlineEvent);
        }
    }
}
