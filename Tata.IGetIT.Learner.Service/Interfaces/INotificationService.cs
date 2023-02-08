using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface INotificationService
    {
         public Task<NotificationData> GetNotification(Notification notification);
    }
}