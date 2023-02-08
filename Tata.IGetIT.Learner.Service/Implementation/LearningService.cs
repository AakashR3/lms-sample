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
    public class LearningService : ILearningService
    {
        private readonly ILearningRepo _learningRepo;
        //Inject logger,config, db and other required services
        public LearningService(ILearningRepo learningRepo)
        {
            if (learningRepo == null)
            {
                new ArgumentNullException("LearningRepo cannot be null");
            }
            _learningRepo = learningRepo;
        }

        public async Task<IEnumerable<UserHistoryGridData>> GetUserHistoryGridData(UserHistory UserHistory)
        {
            try
            {
                return await _learningRepo.GetUserHistoryGridData(UserHistory);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<MyLearningGridData>> GetMyLearningGridData(MyLearning MyLearning)
        {
            try
            {
                return await _learningRepo.GetMyLearningGridData(MyLearning);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> AddFavoriteItem(AddRemoveFavorite AddRemoveFavorite)
        {
            try
            {
                return await _learningRepo.AddFavoriteItem(AddRemoveFavorite);
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> RemoveFavoriteItem(AddRemoveFavorite AddRemoveFavorite)
        {
            try
            {
                return await _learningRepo.RemoveFavoriteItem(AddRemoveFavorite);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<LearningPathDetailedList>> GetMyLearningPath(MyLearning MyLearning)
        {
            try
            {

                var result = await _learningRepo.GetMyLearningPath(MyLearning);

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
                                                         Type = child.Type,
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
                                                         Favorite = child.Favorite
                                                     })
                    };

                return nestedResult;
            }
            catch
            {
                throw;
            }
        }

         public async Task<DownloadCertificateInfo> GetDownloadCertificate(DownloadCertificate downloadCertificate)
        {
            try
            {
                return await _learningRepo.GetDownloadCertificate(downloadCertificate);
            }
            catch
            {
                throw;
            }
        }
    }
}