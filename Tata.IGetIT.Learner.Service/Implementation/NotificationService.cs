using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web;
using Tata.IGetIT.Learner.Repository.Helpers;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepo _NotificationRepo;
        //Inject logger,config, db and other required services
        public NotificationService(INotificationRepo NotificationRepo)
        {
            if (NotificationRepo == null)
            {
                new ArgumentNullException("NotificationRepo cannot be null");
            }
            _NotificationRepo = NotificationRepo;
        }       

        public async Task<NotificationData> GetNotification(Notification notification)
        {
            try
            {
                return await _NotificationRepo.GetNotification(notification);
            }
            catch
            {
                throw;
            }
        }
    }
}