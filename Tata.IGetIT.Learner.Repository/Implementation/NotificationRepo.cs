using System.Data;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class NotificationRepo : INotificationRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public NotificationRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public async Task<NotificationData> GetNotification(Notification notification)
        {
            var conn = _databaseOperations.CreateConnection();
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", notification.UserID },
                    { "@AccountID", notification.AccountID },
                    { "@NotificationTypeId", notification.NotificationTypeId }
                };

                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GET_USERNOTIFICATION_EMAILCONTENT,
                    Parameters = param
                };

                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);

                if (results != null)
                {
                    try
                    {
                        var userNotificationDetails_result = results.Read<UserNotificationDetails>().ToList();
                        var usersPendingProfileCompletion_result = results.Read<UsersPendingProfileCompletion>().ToList();
                        var usersDashboardMetrics_result = results.Read<UsersDashboardMetrics>().ToList();

                        NotificationData notificationData = new()
                        {
                            UserNotificationDetails = userNotificationDetails_result,
                            UsersPendingProfileCompletion = usersPendingProfileCompletion_result,
                            UsersDashboardMetrics = usersDashboardMetrics_result
                        };

                        return notificationData;
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