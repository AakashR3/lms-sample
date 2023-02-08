using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class TranscriptRepo : ITranscriptRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public TranscriptRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<AssessmentProperties> GetAssessmentProperties(int UserID, int AssessmentID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@AssessmentID", AssessmentID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetAssessmentProperties,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<AssessmentProperties>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<TranscriptAssessmmentHistory>> GetTranscriptAssessmentHistory(int UserID, int CategoryID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@CategoryID", CategoryID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_TRANSCRIPT_ASSESSMENT_HISTORY,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<TranscriptAssessmmentHistory>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<TranscriptCourseHistory>> GetTranscriptCourseHistory(int UserID, int CategoryID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@CategoryID", CategoryID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_TRANSCRIPT_COURSE_HISTORY,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<TranscriptCourseHistory>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<TranscriptUserDetails>> GetTranscriptUserDetails(int UserID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_TRANSCRIPT_USER_DETAILS,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<TranscriptUserDetails>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TranscriptUserPublicURL> GetTranscriptUserPublicURL(int UserID)
        {
            var conn = _databaseOperations.CreateConnection();
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_TRANSCRIPT_Public_URL,
                    Parameters = param
                };

                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);

                if (results != null)
                {

                    try
                    {
                        var transcriptUserDetails = results.Read<TranscriptUserDetails>().FirstOrDefault();
                        var transcriptUserProfile = results.Read<TranscriptUserProfile>().FirstOrDefault();
                        var transcriptUserCertificates = results.Read<TranscriptUserCertificates>().ToList();
                        TranscriptUserPublicURL transcriptUserPublicURL = new()
                        {
                            transcriptUserDetails = transcriptUserDetails,
                            transcriptUserProfile = transcriptUserProfile,
                            transcriptUserCertificates = transcriptUserCertificates
                        };

                        return transcriptUserPublicURL;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
