using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ICommonService
    {
        public Task<CategoryDetails> GetCategories(int CategoryID);
        public Task<IEnumerable<GetSubCategoriesBasedOnCategoryID>> GetSubCategoriesBasedOnCategoryID(int CategoryID);
        public Task<IEnumerable<GetTopicsBasedOnCategoryID>> GetTopicsBasedOnCategoryID(int CategoryID);
        public Task<IEnumerable<Plans>> GetAllPlans(int PlanCode);
        public Task<IEnumerable<IndividualPlans>> GetIndividualPlans(string ClientIP);
        public Task<IEnumerable<IndividualPlans>> GetwwwIndividualPlans(string CountryCode);
        public Task<IEnumerable<CourseTitles>> GetCourseTitles(int CategoryID, int SubCategoryID);
        public Task<bool> SendEmailAsync(Common_EmailInfo common_EmailInfo);

    }
}
