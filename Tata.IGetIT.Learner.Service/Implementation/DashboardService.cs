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

namespace Tata.IGetIT.Learner.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepo _dashboardRepo;
        //Inject logger,config, db and other required services
        public DashboardService(IDashboardRepo dashboardRepo)
        {
            if (dashboardRepo == null)
            {
                new ArgumentNullException("DashboardRepo cannot be null");
            }
            _dashboardRepo = dashboardRepo;
        }

        public async Task<Dashboard> GetScoreCard(ScoreCard scoreCard)
        {
            try
            {
                return await _dashboardRepo.GetScoreCard(scoreCard);
            }
            catch
            {
                throw;
            }

        }

        public async Task<IEnumerable<SubscriptionList>> GetTrendingSubscription(Subscription subscription)
        {
            try
            {
                return await _dashboardRepo.GetTrendingSubscription(subscription);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CatalogList>> GetCatalog()
        {
            try
            {
                return await _dashboardRepo.GetCatalog();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CourseList>> GetNewCoursesList()
        {
            try
            {
                return await _dashboardRepo.GetNewCoursesList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CourseListInProgress>> GetInProgressCourseList(ScoreCard scoreCard)
        {
            try
            {
                return await _dashboardRepo.GetInProgressCourseList(scoreCard);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<LearningPathDetailedList>> GetLearningPath(LearningPath learningPath)
        {
            try
            {

                var result = await _dashboardRepo.GetLearningPath(learningPath);

                var nestedResult =
                    from grpByParent in result
                    group grpByParent by new
                    {
                        grpByParent.SNo,
                        grpByParent.PathID,
                        grpByParent.LearningPathName,
                        grpByParent.LPDuration,
                        grpByParent.CourseCount
                    } into glp
                    select new LearningPathDetailedList()
                    {
                        SNo = glp.Key.SNo,
                        PathID = glp.Key.PathID,
                        LearningPathName = glp.Key.LearningPathName,
                        LPDuration = glp.Key.LPDuration,
                        CourseCount = glp.Key.CourseCount,
                        LearningPathCourseDetails = (from child in glp
                                                     select new LearningPathCourseDetails()
                                                     {
                                                         CourseSNo = child.CourseSNo,
                                                         CourseID = child.CourseID,
                                                         CourseName = child.CourseName,
                                                         LessonsCompleted = child.LessonsCompleted,
                                                         LessonsTotal = child.LessonsTotal,
                                                         Duration = child.Duration,
                                                         Progress = child.Progress,
                                                         EventID = child.EventID,
                                                         LastAccess = child.LastAccess,
                                                         StartDate = child.StartDate,
                                                         DueDate = child.DueDate,
                                                         Type = child.Type
                                                     })
                    };

                return nestedResult;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CourseList>> GetRecommendedCourseList(LearningPath learningPath)
        {
            try
            {
                return await _dashboardRepo.GetRecommendedCourseList(learningPath);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CourseList>> GetPeersCourseList(LearningPath learningPath)
        {
            try
            {
                return await _dashboardRepo.GetPeersCourseList(learningPath);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProfileData>> GetProfile(ScoreCard scoreCard)
        {
            try
            {
                return await _dashboardRepo.GetProfile(scoreCard);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TranscriptList>> GetTranscriptList(ScoreCard scoreCard)
        {
            try
            {
                return await _dashboardRepo.GetTranscriptList(scoreCard);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<UpcomingEventsList>> GetUpcomingEventsList(UpcomingEvents upcomingEvents)
        {
            try
            {
                return await _dashboardRepo.GetUpcomingEventsList(upcomingEvents);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<LeadingUserDetail>> GetLeadingUsersAsync(int userId, int numberOfRecords, int numberOfDays)
        {
            return await _dashboardRepo.GetLeadingUsersAsync(userId, numberOfRecords, numberOfDays);
        }

        public async Task<IEnumerable<PopularRolesList>> GetPopularRoles(ScoreCard scoreCard)
        {
            try
            {
                return await _dashboardRepo.GetPopularRoles(scoreCard);
            }
            catch
            {
                throw;
            }
        }

        public async Task<TimeSpentMetrics> GetTimeSpent(ScoreCard scoreCard)
        {
            try
            {
                return await _dashboardRepo.GetTimeSpent(scoreCard);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TimeSpentGraph>> GetTimeSpentGraph(ScoreCard scoreCard)
        {
            try
            {
                return await _dashboardRepo.GetTimeSpentGraph(scoreCard);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PopularRoleGraph>> GetPopularRolesGraph(ScoreCard scoreCard)
        {
            try
            {
                return await _dashboardRepo.GetPopularRolesGraph(scoreCard);
            }
            catch
            {
                throw;
            }
        }

        public async Task<HeroSectionDetails> GetDashboardHeroSectionDetails(ScoreCard scoreCard)
        {
            try
            {
                return await _dashboardRepo.GetDashboardHeroSectionDetails(scoreCard);
            }
            catch
            {
                throw;
            }
        }
    }
}