using System;
using System.Data;
using System.Text.RegularExpressions;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface.AccountManagement;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.AccountManagement;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation.AccountManagement
{
    public class AdminRepo : IAdminRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public AdminRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        /*public async Task<AdminDashboardDetails> GetAdminDashboardDetails(int AccountID)
        {
            var conn = _databaseOperations.CreateConnection();
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetAdminDashboardDetails,
                    Parameters = param
                };
                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);

                if (results != null)
                {
                    try
                    {

                        var userScoreCards_result = results.Read<UserScoreCards>().FirstOrDefault();
                        var currentSubscription_result = results.Read<CurrentSubscription>().ToList();
                        AdminDashboardDetails adminDashboardDetails = new()
                        {
                            userScoreCards = userScoreCards_result,
                            currentSubscriptions = currentSubscription_result
                        };
                        return adminDashboardDetails;
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
*//*public async Task<AdminDashboardDetails> GetAdminDashboardDetails(int AccountID)
        {
            var conn = _databaseOperations.CreateConnection();
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetAdminDashboardDetails,
                    Parameters = param
                };
                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);

                if (results != null)
                {
                    try
                    {

                        var userScoreCards_result = results.Read<UserScoreCards>().FirstOrDefault();
                        var currentSubscription_result = results.Read<CurrentSubscription>().ToList();
                        AdminDashboardDetails adminDashboardDetails = new()
                        {
                            userScoreCards = userScoreCards_result,
                            currentSubscriptions = currentSubscription_result
                        };
                        return adminDashboardDetails;
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
*/
        public Task<IEnumerable<DownloadUserReport>> DownloadUserReport(int AccountID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DownloadUserReport,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<DownloadUserReport>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<LoginMonths>> GetLoginMonths(int AccountID, int GroupID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                    { "@GroupID", GroupID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetLoginMonths,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<LoginMonths>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<TopAssessments>> GetTopAssessments(int AccountID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetTopAssessments,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<TopAssessments>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<TopCourses>> GetTopCourses(int AccountID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetTopCourses,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<TopCourses>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<Topusers>> GetTopusers(int AccountID, int Num, int GroupID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                    { "@Num", Num },
                    { "@GroupID", GroupID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetTopUsers,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<Topusers>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<UserScoreCards> GetUserScoreCards(int AccountID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetScoreCard,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<UserScoreCards>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CurrentSubscriptionParent> GetCurrentSubscription(int AccountID, int PageNumber, int PageSize)
        {
            var conn = _databaseOperations.CreateConnection();
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                    { "@PageNumber", PageNumber },
                    { "@PageSize", PageSize },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetCurrentSubscription,
                    Parameters = param
                };

                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);

                if (results != null)
                {
                    try
                    {
                        var totalRecords = results.Read<int>().FirstOrDefault();
                        var currentSub = results.Read<CurrentSubscription>().ToList();

                        CurrentSubscriptionParent currentSubscriptionParent = new()
                        {
                            TotalItems = currentSub.Count,
                            currentSubscriptions= currentSub
                        };

                        return currentSubscriptionParent;
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
