using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ITrainingsService
    {
        //Task<PagedResult<IEnumerable<Training>>> GetTrainings(int userId, int pageNumber, int pageSize);

        Task<IEnumerable<Training>> GetTrainings(int userId, int pageNumber, int pageSize,string filter);

        Task<int> CreateTrainings(CreateTrainingRequest request, int userId, int accoutId);
        Task<bool> UpdateTrainings(UpdateTrainingRequest request, int userId, int accoutId);
        Task<bool> DeleteTrainings(int trainingId, int userId);

        Task GetSessions(int trainingId);
        Task CreateSessionAsync(CreateMeetingRequest request,int userId, List<string> ErrorsMessages);
        Task UpdateSessionAsync(UpdateMeetingRequest request, int userId, List<string> ErrorsMessages);
        Task DeleteSessionAsync(DeleteMeetingRequest request, int userId, List<string> ErrorsMessages);
        Task GetSessionTranscriptAsync(string meetingId);
        Task GetSessionRecordingAsync(string meetingId);
        Task<Session> GetSession(int id,int UserID);

        Task<Training> GetTrainings(int id,int UserID);
        Task<IEnumerable<Platform>> GetPlatforms();

        /// <summary>
        /// Get meeting attendance report
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        Task<List<AttendanceReport>> GetSessionAteendanceReportsAsync(string meetingId);


        public Task<IEnumerable<Session>> TrainingSession(int TrainingID,int UserID);


        public Task<IEnumerable<Session>> CalendarSessions(string Date,int UserId);

    }
}
