using Microsoft.Graph;
using Microsoft.Identity.Client;
using Razorpay.Api;
using Recurly.Resources;
using System;
using System.IO;
using Tata.IGetIT.Learner.Repository.Interface.AccountManagement;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.AccountManagement;
using Tata.IGetIT.Learner.Service.Interfaces.AccountManagement;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Tata.IGetIT.Learner.Service.Implementation.AccountManagement
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public AdminService(IAdminRepo adminRepo)
        {
            if (adminRepo == null)
            {
                new ArgumentNullException("assignedLearningRepo cannot be null");
            }
            _adminRepo = adminRepo;
        }
/*
        public async Task<AdminDashboardDetails> GetAdminDashboardDetails(int AccountID)
        {
            var result= await _adminRepo.GetAdminDashboardDetails(AccountID);
        
*//*
            var currentsub = from c in result.currentSubscriptions
                                 select c;
            foreach (var cur in currentsub)
            {
                cur.ExpireDateString = cur.ExpireDate.ToString().Length>0 ? cur.ExpireDate.ToString("dd-MM-yyyy"): String.Empty; 
            }
            result.currentSubscriptions = currentsub;*//*
            return result;

        }*/

        public async Task<IEnumerable<DownloadUserReport>> DownloadUserReport(int AccountID)
        {
            return await _adminRepo.DownloadUserReport(AccountID);

        }

        public async Task<IEnumerable<LoginMonths>> GetLoginMonths(int AccountID, int GroupID)
        {
            return await _adminRepo.GetLoginMonths(AccountID, GroupID);
        }

        public async Task<IEnumerable<TopAssessments>> GetTopAssessments(int AccountID)
        {
            return await _adminRepo.GetTopAssessments(AccountID);
        }

        public async Task<IEnumerable<TopCourses>> GetTopCourses(int AccountID)
        {
            return await _adminRepo.GetTopCourses(AccountID);
        }

        public async Task<IEnumerable<Topusers>> GetTopUsers(int AccountID, int Num, int GroupID)
        {
            return await _adminRepo.GetTopusers(AccountID, Num, GroupID);   
        }

        public async Task<UserScoreCards> GetUserScoreCards(int AccountID)
        {
            return await _adminRepo.GetUserScoreCards(AccountID);
        }

        public async Task<CurrentSubscriptionParent> GetCurrentSubscription(int AccountID, int PageNumber, int PageSize)
        {
            return await _adminRepo.GetCurrentSubscription(AccountID, PageNumber, PageSize);
        }
    }
}
