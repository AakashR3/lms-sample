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
    public class QuickStartService : IQuickStartService
    {
        private readonly IQuickStartRepo _quickstartRepo;
        //Inject logger,config, db and other required services
        public QuickStartService(IQuickStartRepo quickstartRepo)
        {
            if (quickstartRepo == null)
            {
                new ArgumentNullException("QuickstartRepo cannot be null");
            }
            _quickstartRepo = quickstartRepo;
        }       

        public async Task<IEnumerable<QuickStartGridData>> GetQuickStartGridData(QuickStartGrid quickStartGrid)
        {
            try
            {
                return await _quickstartRepo.GetQuickStartGridData(quickStartGrid);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Categories>> GetCategories(QuickStartGrid quickStartGrid)
        {
            try
            {
                return await _quickstartRepo.GetCategories(quickStartGrid);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SubCategories>> GetSubCategories(QuickStartGrid quickStartGrid)
        {
            try
            {
                return await _quickstartRepo.GetSubCategories(quickStartGrid);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> GetNotificationFlag(QuickStartNotification quickStartNotification)
        {
            try
            {
                return await _quickstartRepo.GetNotificationFlag(quickStartNotification);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateNotificationFlag(QuickStartNotification quickStartNotification)
        {
            try
            {
                return await _quickstartRepo.UpdateNotificationFlag(quickStartNotification);
            }
            catch
            {
                throw;
            }
        }
        
    }
}