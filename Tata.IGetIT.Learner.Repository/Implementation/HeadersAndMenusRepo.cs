using Dapper;
using System.Data;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;
using static Dapper.SqlMapper;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class HeadersAndMenusRepo : IHeadersAndMenusRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public HeadersAndMenusRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public async Task<NotificationsCourseReleases> AllUserNotification(int UserID)
        {

            var conn = _databaseOperations.CreateConnection();
            try
            {
                var queryParameters = new Dictionary<string, object>
                {
                    { "@UserID", UserID }
                };

                QueryInfo queryInfo = new()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetUserNotifications,
                    Parameters = queryParameters
                };
                var results = await _databaseOperations.GetMultipleResultAsync(conn, queryInfo);                

                if (results != null)
                {
                    try
                    {
                        var releaseDetails = results.Read<ReleaseDetails>().ToList();
                        var headersAndMenu_UserNotifications = results.Read<HeadersAndMenu_UserNotifications>();
                        var NotificationCount = (from a in headersAndMenu_UserNotifications
                                                 select a.NotificationCount).FirstOrDefault();
                        var allNotifications = (from item in headersAndMenu_UserNotifications
                                                select new HeadersAndMenu_UserNotifications
                                                {
                                                    NotificationID = item.NotificationID,
                                                    Message = item.Message,
                                                    Link = item.Link
                                                }).ToList();

                        HeadersAndMenu_UserNotificationDetails userNotificationDetails = new()
                        {
                            NotificationsCount = NotificationCount,
                            Notifications = allNotifications
                        };
                        NotificationsCourseReleases notificationsCourseReleases = new()
                        {
                            headersAndMenu_UserNotificationDetails = userNotificationDetails,
                            ReleaseDetails = releaseDetails
                        };

                        return notificationsCourseReleases;
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

        public Task<TrialUserDetails> CheckTrialUser(int UserID)
        {

            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.CheckTrialUser,
                Parameters = queryParameters
            };

            return _databaseOperations.GetFirstRecordAsync<TrialUserDetails>(queryInfo);
        }

        public Task<IEnumerable<HeadersAndMenu_MoreActions_Favorites>> GetFavorites(int UserID)
        {

            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetFavorites,
                Parameters = queryParameters
            };

            return _databaseOperations.GetMultipleRecords<HeadersAndMenu_MoreActions_Favorites>(queryInfo);
        }

        public Task<IEnumerable<HeadersAndMenu_MoreActions_Subscription_Child>> GetSubscriptions(int UserID, string Currency)
        {

            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID },
                { "@Currency", Currency }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetSubscriptions,
                Parameters = queryParameters
            };

            return _databaseOperations.GetMultipleRecords<HeadersAndMenu_MoreActions_Subscription_Child>(queryInfo);
        }

        public Task<IEnumerable<HeadersAndMenu_UserMenu>> MenuItems(int UserID)
        {
            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetMenuItems,
                Parameters = queryParameters
            };

            return _databaseOperations.GetMultipleRecords<HeadersAndMenu_UserMenu>(queryInfo);
        }

        public Task<HeadersAndMenu_UserPoints> Points(int UserID)
        {
            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetPoints,
                Parameters = queryParameters
            };

            return _databaseOperations.GetFirstRecordAsync<HeadersAndMenu_UserPoints>(queryInfo);
        }

        public Task<int> UserCartCount(int UserID)
        {
            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetUserCartCount,
                Parameters = queryParameters
            };

            return _databaseOperations.ExecuteScalarAsyncInteger(queryInfo);
        }

        public Task<IEnumerable<HeadersAndMenu_UserNotifications>> UserNotification(int UserID)
        {
            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetUserNotifications,
                Parameters = queryParameters
            };

            return _databaseOperations.GetMultipleRecords<HeadersAndMenu_UserNotifications>(queryInfo);
        }

    }
}
