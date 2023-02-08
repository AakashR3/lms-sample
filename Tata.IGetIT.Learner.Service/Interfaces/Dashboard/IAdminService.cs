﻿using Tata.IGetIT.Learner.Repository.Models.AccountManagement;

namespace Tata.IGetIT.Learner.Service.Interfaces.AccountManagement
{
    public interface IAdminService
    {
        public Task<IEnumerable<TopAssessments>> GetTopAssessments(int AccountID);
        public Task<IEnumerable<TopCourses>> GetTopCourses(int AccountID);
        public Task<IEnumerable<Topusers>> GetTopUsers(int AccountID, int Num, int GroupID);
        public Task<IEnumerable<LoginMonths>> GetLoginMonths(int AccountID, int GroupID);
        public Task<UserScoreCards> GetUserScoreCards(int AccountID);
        public Task<CurrentSubscriptionParent> GetCurrentSubscription(int AccountID,int PageNumber, int PageSize);
        public Task<IEnumerable<DownloadUserReport>> DownloadUserReport(int AccountID);

    }
}
