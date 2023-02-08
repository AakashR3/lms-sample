using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ITechTipsRepo
    {
        public Task<IEnumerable<TechTips>> GetTechTips(int UserID, int CategoryID, int SubCategoryID, int TopicID,
            int Filter, string SearchTag, int SearchInID, int SearchInTag, int SearchInTitle, int SearchInContent);

        public Task<IEnumerable<Topics>> GetTopicsBySubCategoryID(int SubCategoryID);

        public Task<IEnumerable<TopicsByCategory>> GetTopicsByCategoryID(int CategoryID);
    }
}
