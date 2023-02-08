
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface IDashboardService
    {
        public Task<Dashboard> GetScoreCard(ScoreCard scoreCard);
        public Task<IEnumerable<SubscriptionList>> GetTrendingSubscription(Subscription subscription);
        public Task<IEnumerable<CatalogList>> GetCatalog();
        public Task<IEnumerable<CourseList>> GetNewCoursesList();
        public Task<IEnumerable<CourseListInProgress>> GetInProgressCourseList(ScoreCard scoreCard);
        public Task<IEnumerable<LearningPathDetailedList>> GetLearningPath(LearningPath learningPath);
        public Task<IEnumerable<CourseList>> GetRecommendedCourseList(LearningPath learningPath);
        public Task<IEnumerable<CourseList>> GetPeersCourseList(LearningPath learningPath);
        public Task<IEnumerable<ProfileData>> GetProfile(ScoreCard scoreCard);
        public Task<IEnumerable<TranscriptList>> GetTranscriptList(ScoreCard scoreCard);
        public Task<IEnumerable<UpcomingEventsList>> GetUpcomingEventsList(UpcomingEvents upcomingEvents);
        public Task<IEnumerable<LeadingUserDetail>> GetLeadingUsersAsync(int userId, int numberOfRecords, int numberOfDays);
        public Task<IEnumerable<PopularRolesList>> GetPopularRoles(ScoreCard scoreCard);
        public Task<IEnumerable<PopularRoleGraph>> GetPopularRolesGraph(ScoreCard scoreCard);
        public Task<TimeSpentMetrics> GetTimeSpent(ScoreCard scoreCard);
        public Task<IEnumerable<TimeSpentGraph>> GetTimeSpentGraph(ScoreCard scoreCard);
        public Task<HeroSectionDetails> GetDashboardHeroSectionDetails(ScoreCard scoreCard);
    }
}