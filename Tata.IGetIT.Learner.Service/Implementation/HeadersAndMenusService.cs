using Dapper;
using Recurly.Resources;
using System;
using static Dapper.SqlMapper;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class HeadersAndMenusService : IHeadersAndMenusService
    {
        private readonly IHeadersAndMenusRepo _headersAndMenusRepo;
        public HeadersAndMenusService(IHeadersAndMenusRepo headersAndMenusRepo)
        {
            if (headersAndMenusRepo == null)
            {
                new ArgumentNullException(LearnerAppConstants.USEREPO_NULL);
            }
            _headersAndMenusRepo = headersAndMenusRepo;

        }

        public async Task<NotificationsCourseReleases> AllUserNotification(int UserID)
        {

            return await _headersAndMenusRepo.AllUserNotification(UserID);

            //var releaseDetails = result.Read<ReleaseDetails>().ToList();
            //var headersAndMenu_UserNotifications = result.Read<HeadersAndMenu_UserNotifications>();

            //if (result != null)
            //{
            //    var NotificationCount = (from a in headersAndMenu_UserNotifications
            //                             select a.NotificationCount).FirstOrDefault();
            //    var allNotifications = (from item in headersAndMenu_UserNotifications
            //                            select new HeadersAndMenu_UserNotifications { NotificationID = item.NotificationID, 
            //                                Message = item.Message, Link = item.Link }).ToList();
            //    HeadersAndMenu_UserNotificationDetails userNotificationDetails = new()
            //    {
            //        NotificationsCount = NotificationCount,
            //        Notifications = allNotifications
            //    };
            //    NotificationsCourseReleases notificationsCourseReleases = new()
            //    {
            //        headersAndMenu_UserNotificationDetails = userNotificationDetails,
            //        ReleaseDetails = releaseDetails
            //    };

            //    return notificationsCourseReleases;
            //}
            //else
            //{
            //    return new NotificationsCourseReleases();
            //}

        }

        public async Task<TrialUserDetails> CheckTrialUser(int UserID, List<string> ErrorsMessages)
        {

            try
            {
                var result = await _headersAndMenusRepo.CheckTrialUser(UserID);

                result.Message = result.Result == 1 ? String.Format(LearnerAppConstants.TRIAL_USER, result.RemainingDays) : result.Status;
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<HeadersAndMenu_MoreActions_Favorites>> GetFavorites(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _headersAndMenusRepo.GetFavorites(UserID);
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<HeadersAndMenu_MoreActions_Subscription_Parent> GetSubscriptions(int UserID, string Currency, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _headersAndMenusRepo.GetSubscriptions(UserID, Currency);
                HeadersAndMenu_MoreActions_Subscription_Parent headersAndMenu_MoreActions_Subscription_Parent = new();

                if (result.Any())
                {
                    var resultParent = new HeadersAndMenu_MoreActions_Subscription_Parent()
                    {
                        UserID = (int)(from num in result
                                       select num.UserID).FirstOrDefault(),
                        AccountID = (int)(from num in result
                                          select num.AccountID).FirstOrDefault(),
                        ExpiredCount = result.Count(e => (e.Status.Equals("Expired", StringComparison.OrdinalIgnoreCase) || e.Status.Equals("Churn", StringComparison.OrdinalIgnoreCase))),
                        ActiveCount = result.Count(e => (e.Status.Equals("Trial", StringComparison.OrdinalIgnoreCase) || e.Status.Equals("Active", StringComparison.OrdinalIgnoreCase))),
                        ExpiredSubscriptions = (from gc in result.Where(e => (e.Status.Equals("Expired", StringComparison.OrdinalIgnoreCase) || e.Status.Equals("Churn", StringComparison.OrdinalIgnoreCase)))
                                                select new HeadersAndMenu_MoreActions_Subscription_Child()
                                                {
                                                    UserID = null,
                                                    AccountID = null,
                                                    SubscriptionID = gc.SubscriptionID,
                                                    FulfillmentID = gc.FulfillmentID,
                                                    SalesOrderDetailID = gc.SalesOrderDetailID,
                                                    ExpireDate = gc.ExpireDate,
                                                    SubscriptionName = gc.SubscriptionName,
                                                    CourseCount = gc.CourseCount > 0 ? gc.CourseCount : null,
                                                    AssessmentCount = gc.AssessmentCount > 0 ? gc.AssessmentCount : null,
                                                    Price = gc.Price,
                                                    Currency = gc.Currency,
                                                    Image=gc.Image
                                                }),
                        ActiveSubscriptions = (from gc in result.Where(e => (e.Status.Equals("Trial", StringComparison.OrdinalIgnoreCase) || e.Status.Equals("Active", StringComparison.OrdinalIgnoreCase)))
                                               select new HeadersAndMenu_MoreActions_Subscription_Child()
                                               {
                                                   UserID = null,
                                                   AccountID = null,
                                                   SubscriptionID = gc.SubscriptionID,
                                                   FulfillmentID = gc.FulfillmentID,
                                                   SalesOrderDetailID = gc.SalesOrderDetailID,
                                                   ExpireDate = gc.ExpireDate,
                                                   SubscriptionName = gc.SubscriptionName,
                                                   CourseCount = gc.CourseCount > 0 ? gc.CourseCount : null,
                                                   AssessmentCount = gc.AssessmentCount > 0 ? gc.AssessmentCount : null,
                                                   Price = gc.Price,
                                                   Currency = gc.Currency,
                                                   Image = gc.Image
                                               })
                    };

                    return resultParent;
                }
                else
                {
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                    return headersAndMenu_MoreActions_Subscription_Parent;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<HeadersAndMenu_UserMenu>> MenuItems(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _headersAndMenusRepo.MenuItems(UserID);
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<HeadersAndMenu_UserPoints> Points(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _headersAndMenusRepo.Points(UserID);
                if (result == null)
                {
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> UserCartCount(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _headersAndMenusRepo.UserCartCount(UserID);
                //if (result == 0)
                //    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<HeadersAndMenu_UserNotificationDetails> UserNotification(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _headersAndMenusRepo.UserNotification(UserID);

                if (result != null)
                {
                    var NotificationCount = (from a in result
                                             select a.NotificationCount).FirstOrDefault();
                    var allNotifications = (from item in result
                                            select new HeadersAndMenu_UserNotifications { NotificationID = item.NotificationID, Message = item.Message, Link = item.Link }).ToList();
                    HeadersAndMenu_UserNotificationDetails userNotificationDetails = new()
                    {
                        NotificationsCount = NotificationCount,
                        Notifications = allNotifications
                    };
                    return userNotificationDetails;
                }
                else
                {
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                    return new HeadersAndMenu_UserNotificationDetails();
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
