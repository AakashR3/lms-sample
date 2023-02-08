using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface INotificationRepo
    {        
        public Task<NotificationData> GetNotification(Notification notification);
    }
}