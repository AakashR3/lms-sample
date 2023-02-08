using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NLog.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tata.IGetIT.Learner.Repository.Implementation;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using ILogger = NLog.ILogger;

namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class CMSService : ICMSService
    {
        private readonly ICMSRepo _cmsRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public CMSService(ICMSRepo cmsRepo)
        {
            if (cmsRepo == null)
            {
                new ArgumentNullException("cmsRepo cannot be null");
            }
            _cmsRepo = cmsRepo;
        }


        public async Task<IEnumerable<V2_CMSFormType>> GetCMSFormTypes()
        {
            return await _cmsRepo.GetCMSFormTypes();
        }

        public async Task<bool> InsertCMSFormData(V2_CMSFormDataInsert v2_CMSFormDataInsert)
        {
            try
            {
                return await _cmsRepo.InsertCMSFormData(v2_CMSFormDataInsert);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
