using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ICommonRepo
    {
        public Task<CategoryDetails> GetCategories(int CategoryID);
        public Task<IEnumerable<GetSubCategoriesBasedOnCategoryID>> GetSubCategoriesBasedOnCategoryID(int CategoryID);
        public Task<IEnumerable<GetTopicsBasedOnCategoryID>> GetTopicsBasedOnCategoryID(int CategoryID);
        public Task<IEnumerable<Plans>> GetAllPlans(int PlanCode);
        public Task<IEnumerable<IndividualPlans>> GetIndividualPlans(string CountryCode);
        public Task<IEnumerable<CourseTitles>> GetCourseTitles(int CategoryID, int SubCategoryID);

        //public Task SendEmailAsync(Common_EmailInfo common_EmailInfo);
    }
}
