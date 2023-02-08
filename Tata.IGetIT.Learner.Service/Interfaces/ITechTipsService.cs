using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ITechTipsService
    {
        public Task<IEnumerable<TechTips>> GetTechTips(int UserID, int CategoryID, int SubCategoryID, int TopicID, 
            int Filter, string SearchTag,int SearchInID,int SearchInTag,int SearchInTitle, int SearchInContent);
        public Task<IEnumerable<Topics>> GetTopicsSubCategoryID(int SubCategoryID);
        public Task<IEnumerable<TopicsByCategory>> GetTopicsCategoryID(int SubCategoryID);
    }
}
