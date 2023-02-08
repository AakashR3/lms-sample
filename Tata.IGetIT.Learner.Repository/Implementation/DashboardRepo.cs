using System.Data;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class DashboardRepo : IDashboardRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public DashboardRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<Dashboard> GetScoreCard(ScoreCard scoreCard)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", scoreCard.UserID);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETSCORECARD,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<Dashboard>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<CourseList>> GetNewCoursesList()
        {
            try
            {
                var param = new Dictionary<string, object>();
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETNEWCOURSELIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CourseList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<LearningPathList>> GetLearningPath(LearningPath learningPath)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", learningPath.UserID);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETLEARNINGPATH,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<LearningPathList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<CourseListInProgress>> GetInProgressCourseList(ScoreCard scoreCard)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", scoreCard.UserID);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETINPROGRESSCOURSELIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CourseListInProgress>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<SubscriptionList>> GetTrendingSubscription(Subscription subscription)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@CurrencyCode", subscription.CurrencyCode);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETTRENDINGSUBSCRIPTION,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<SubscriptionList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<CatalogList>> GetCatalog()
        {
            try
            {
                var param = new Dictionary<string, object>();
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETCATALOG,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CatalogList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<CourseList>> GetRecommendedCourseList(LearningPath learningPath)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", learningPath.UserID);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETRECOMMENDEDCOURSELIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CourseList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<CourseList>> GetPeersCourseList(LearningPath learningPath)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", learningPath.UserID);
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETPEERSCOURSELIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CourseList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<UpcomingEventsList>> GetUpcomingEventsList(UpcomingEvents upcomingEvents)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", upcomingEvents.UserID);
                param.Add("@EventPeriod", upcomingEvents.EventPeriod);

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETUPCOMINGEVENTLIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<UpcomingEventsList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<ProfileData>> GetProfile(ScoreCard scoreCard)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", scoreCard.UserID);

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETPROFILEDATA,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<ProfileData>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<TranscriptList>> GetTranscriptList(ScoreCard scoreCard)
        {
            try
            {
                var param = new Dictionary<string, object>();
                param.Add("@UserID", scoreCard.UserID);
                

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETTRANSCRIPTLIST,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<TranscriptList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<LeadingUserDetail>> GetLeadingUsersAsync(int userId, int numberOfRecords, int numberOfDays)
        {
            var param = new Dictionary<string, object>
            {
                 { "@UserId", userId },
                 { "@NumberOfRecords" , numberOfRecords},
                 { "@NumberOfDays" , numberOfDays},
            };

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GET_LEADING_USERS,
                Parameters = param
            };

            return await _databaseOperations.GetMultipleRecords<LeadingUserDetail>(queryInfo);

        }
        
        public Task<IEnumerable<PopularRolesList>> GetPopularRoles(ScoreCard scoreCard)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", scoreCard.UserID }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETPOPULARROLES,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<PopularRolesList>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<TimeSpentMetrics> GetTimeSpent(ScoreCard scoreCard)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", scoreCard.UserID }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETTIMESPENTMETRICS,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<TimeSpentMetrics>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<TimeSpentGraph>> GetTimeSpentGraph(ScoreCard scoreCard)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", scoreCard.UserID }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETTIMESPENTGRAPH,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<TimeSpentGraph>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<PopularRoleGraph>> GetPopularRolesGraph(ScoreCard scoreCard)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", scoreCard.UserID }
                };

                QueryInfo queryInfo = new()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GET_POPULAR_ROLE_GRAPH,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<PopularRoleGraph>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<HeroSectionDetails> GetDashboardHeroSectionDetails(ScoreCard scoreCard)
        {
            var conn = _databaseOperations.CreateConnection();
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", scoreCard.UserID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DASHBOARD_GETHEROSECTIONDETAILS,
                    Parameters = param
                };
                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);


                if (results != null)
                {
                    try
                    {
                        var heroSectionNavigation_result = results.Read<HeroSectionNavigation>().FirstOrDefault();
                        var heroSectionLearningPathList_result = results.Read<HeroSectionLearningPathList>().ToList();
                        HeroSectionDetails heroSectionDetails = new()
                        {
                            heroSectionNavigation = heroSectionNavigation_result,
                            heroSectionLearningPathList = heroSectionLearningPathList_result
                        };
                        return heroSectionDetails;
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
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }
    }
}