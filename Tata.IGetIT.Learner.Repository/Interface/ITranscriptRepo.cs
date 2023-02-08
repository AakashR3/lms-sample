using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ITranscriptRepo
    {
        public Task<IEnumerable<TranscriptUserDetails>> GetTranscriptUserDetails(int UserID);
        public Task<IEnumerable<TranscriptCourseHistory>> GetTranscriptCourseHistory(int UserID, int CategoryID);
        public Task<IEnumerable<TranscriptAssessmmentHistory>> GetTranscriptAssessmentHistory(int UserID, int CategoryID);
        public Task<AssessmentProperties> GetAssessmentProperties(int UserID, int AssessmentID);
        public Task<TranscriptUserPublicURL> GetTranscriptUserPublicURL(int UserID);


    }
}
