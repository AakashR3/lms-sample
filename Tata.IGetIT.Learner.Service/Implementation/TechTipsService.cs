using NLog.Filters;
using System;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class TechTipsService : ITechTipsService
    {
        private readonly ITechTipsRepo _techTipsRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public TechTipsService(ITechTipsRepo techTipsRepo)
        {
            if (techTipsRepo == null)
            {
                new ArgumentNullException("techTipsRepo cannot be null");
            }
            _techTipsRepo = techTipsRepo;
        }

        public async Task<IEnumerable<TechTips>> GetTechTips(int UserID, int CategoryID, int SubCategoryID, int TopicID,
            int Filter, string SearchTag, int SearchInID, int SearchInTag, int SearchInTitle, int SearchInContent)
        {
            try
            {
                return await _techTipsRepo.GetTechTips(UserID, CategoryID, SubCategoryID, TopicID,
                    Filter, SearchTag, SearchInID, SearchInTag, SearchInTitle, SearchInContent);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TopicsByCategory>> GetTopicsCategoryID(int SubCategoryID)
        {

            try
            {
                return await _techTipsRepo.GetTopicsByCategoryID(SubCategoryID);
                //IEnumerable<TopicsByCategory> topics = new List<TopicsByCategory>();
                //topics = (from gc in result
                //          select new TopicsByCategory()
                //          {
                //              ID = gc.ID,
                //              Name = gc.Name,
                //          }).Distinct().ToList();
                //return topics;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Topics>> GetTopicsSubCategoryID(int SubCategoryID)
        {

            try
            {
                var result = await _techTipsRepo.GetTopicsBySubCategoryID(SubCategoryID);
                IEnumerable<Topics> topics = new List<Topics>();
                topics = (from gc in result
                          select new Topics()
                          {
                              TopicID = gc.TopicID,
                              TopicName = gc.TopicName,
                          }).Distinct().ToList();
                return topics;
            }
            catch
            {
                throw;
            }
        }
    }
}
