using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class TrainingsService : ITrainingsService
    {
        private readonly ITeamsService _teamsService;
        private readonly ITrainingsRepo _trainingsRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public TrainingsService(ITeamsService teamsService, ITrainingsRepo trainingsRepo)
        {
            _teamsService = teamsService ?? throw new ArgumentNullException(nameof(ITeamsService));
            _trainingsRepo = trainingsRepo ?? throw new ArgumentNullException(nameof(ITrainingsRepo));
        }
        #region "Trainings"
        //public async Task<PagedResult<IEnumerable<Training>>> GetTrainings(int userId, int pageNumber, int pageSize)
        //{
        //    return  await _trainingsRepo.GetTrainings(userId, pageNumber, pageSize);
        //}

        public async Task<IEnumerable<Training>> GetTrainings(int userId, int pageNumber, int pageSize, string filter)
        {
             return await _trainingsRepo.GetTrainings(userId, pageNumber, pageSize, filter);
        }

        public async Task<int> CreateTrainings(CreateTrainingRequest request, int userId, int accoutId)
        {
            return await _trainingsRepo.CreateTrainings(request , userId, accoutId);
        }
        public async Task<bool> UpdateTrainings(UpdateTrainingRequest request, int userId, int accoutId)
        {
            return await _trainingsRepo.UpdateTrainings(request, userId, accoutId);
        }
        public async Task<bool> DeleteTrainings(int trainingId, int userId)
        {
            return await _trainingsRepo.DeleteTrainings(trainingId, userId);
        }

        #endregion

        #region "Sessions"
        public async Task GetSessions(int trainingId)
        {

        }

        public async Task CreateSessionAsync(CreateMeetingRequest request,int userId, List<string> ErrorsMessages)
        {  
            try
            {

                if (request.PlatformTypeID == 1)
                {

                    var onlineMeeting = await _teamsService.CreateMeetingAsync(request);
                    var eventResult = await _teamsService.AddEventInCalendarAsync(onlineMeeting, request.TimeZone);

                    if (onlineMeeting != null)
                    {
                        string Attendees = string.Join(";", request.Attendees);
                        //var meetingCode = onlineMeeting.AdditionalData.FirstOrDefault(x => x.Key == "meetingCode").Value.ToString();
                        int response = await _trainingsRepo.CreateSession(request, Attendees, onlineMeeting.Id, eventResult.Id, userId);

                        if (response != 1)
                        {
                            ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_CREATE_MEETING);
                        }
                    }
                }
                else
                {
                    //Need to Handle Other Meeting Platform

                    ErrorsMessages.Add(LearnerAppConstants.MEETING_FEATURE_UNAVAILABLE);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateSessionAsync(UpdateMeetingRequest request, int userId, List<string> ErrorsMessages)
        {
            try
            {
                if (request.PlatformTypeID == 1)
                {

                    var onlineMeeting = await _teamsService.UpdateMeetingAsync(request);
                    //_ = await UpdateEventInCalendarAsync(request.EventId, onlineMeeting, request.TimeZone);
                    var eventResult = await _teamsService.UpdateEventInCalendarAsync(request.EventID, onlineMeeting, request.TimeZone);

                    if (onlineMeeting != null)
                    {
                        string Attendees = string.Join(";", request.Attendees);
                        int response = await _trainingsRepo.UpdateSession(request, Attendees, onlineMeeting.Id, eventResult.Id, userId);

                        if (response != 1)
                        {
                            ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_UPDATE_MEETING);
                        }
                    }
                }
                else
                {
                    //Need to Handle Other Meeting Platform

                    ErrorsMessages.Add(LearnerAppConstants.MEETING_FEATURE_UNAVAILABLE);
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteSessionAsync(DeleteMeetingRequest request, int userId,List<string> ErrorsMessages)
        {
            try
            {
                
                bool response = await _trainingsRepo.DeleteSession(request.TrainingSessionID,userId);
                if(response==true)
                {
                    await _teamsService.DeleteMeetingAsync(request);
                }
                else
                {
                    ErrorsMessages.Add(LearnerAppConstants.UNABLE_TO_DELETE_MEETING);
                }

            }
            catch
            {
                throw;
            }
        }

        public async Task GetSessionTranscriptAsync(string meetingId)
        {
            throw new NotImplementedException();

        }
        public async Task GetSessionRecordingAsync(string meetingId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<AttendanceReport>> GetSessionAteendanceReportsAsync(string meetingId)
        {
            return await _teamsService.GetMeetingAttendanceAsync(meetingId);
        }

        public async Task<Session> GetSession(int id,int UserID)
        {
            return await _trainingsRepo.GetSession(id,UserID);
        }

        public async Task<Training> GetTrainings(int id,int UserID)
        {
            return await _trainingsRepo.GetTrainings(id, UserID);
        }

        public async Task<IEnumerable<Platform>> GetPlatforms()
        {
            return await _trainingsRepo.GetPlatforms();
        }

        public async Task<IEnumerable<Session>> TrainingSession(int TrainingID,int UserID)
        {
            try
            {
                return await _trainingsRepo.TrainingSession(TrainingID, UserID);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Session>> CalendarSessions(string Date,int UserId)
        {
            try
            {
                return await _trainingsRepo.CalendarSessions(Date, UserId);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}