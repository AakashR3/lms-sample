using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ITranscriptService
    {
        public Task<IEnumerable<TranscriptUserDetails>> GetTranscriptUserDetails(int UserID, List<string> ErrorsMessages);
        public Task<IEnumerable<TranscriptCourseHistoryParent>> GetTranscriptCourseHistory(int UserID, int CategoryID, List<string> ErrorsMessages);
        public Task<IEnumerable<TranscriptAssessmmentHistoryParent>> GetTranscriptAssessmentHistory(int UserID, int CategoryID, List<string> ErrorsMessages);
        public Task<AssessmentProperties> GetAssessmentProperties(int UserID, int AssessmentID, List<string> ErrorsMessages);
        public Task<TranscriptUserPublicURL> GetTranscriptUserPublicURL(int UserID, List<string> ErrorsMessages);
    }
}
