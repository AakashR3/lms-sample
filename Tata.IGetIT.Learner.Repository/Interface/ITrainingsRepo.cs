using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ITrainingsRepo
    {
        //Task<PagedResult<IEnumerable<Training>>> GetTrainings(int userId, int pageNumber, int pageSize);
        Task<IEnumerable<Training>> GetTrainings(int userId, int pageNumber, int pageSize, string filter);
        Task<int> CreateTrainings(CreateTrainingRequest request, int userId, int accoutId);
        Task<bool> UpdateTrainings(UpdateTrainingRequest request, int userId, int accoutId);
        Task<bool> DeleteTrainings(int trainingId, int userId);
        Task<int> CreateSession(CreateMeetingRequest createMeetingRequest, string Attendees,string meetingId,string eventId,int userId);
        Task<int> UpdateSession(UpdateMeetingRequest createMeetingRequest, string Attendees, string meetingId,string eventId, int userId);
        Task<bool> DeleteSession(int TrainingSessionID, int userId);
        Task<Session> GetSession(int id,int UserID);

        Task<Training> GetTrainings(int id,int UserID);

        Task<IEnumerable<Platform>> GetPlatforms();

        public Task<IEnumerable<Session>> TrainingSession(int TrainingID,int UserID);

        public Task<IEnumerable<Session>> CalendarSessions(string Date,int UserId);
    }
}
