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
    public class TranscriptService : ITranscriptService
    {
        private readonly ITranscriptRepo _transcriptRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public TranscriptService(ITranscriptRepo transcriptRepo)
        {
            if (transcriptRepo == null)
            {
                new ArgumentNullException("transcriptRepo cannot be null");
            }
            _transcriptRepo = transcriptRepo;
        }

        public async Task<AssessmentProperties> GetAssessmentProperties(int UserID, int AssessmentID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _transcriptRepo.GetAssessmentProperties(UserID, AssessmentID);
                if (result != null)
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TranscriptAssessmmentHistoryParent>> GetTranscriptAssessmentHistory(int UserID, int CategoryID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _transcriptRepo.GetTranscriptAssessmentHistory(UserID, CategoryID);
                IEnumerable<TranscriptAssessmmentHistoryParent> transcriptAssessments = new List<TranscriptAssessmmentHistoryParent>();

                if (result.Any())
                {
                    var transcriptAssessmmentHistoryParent = from c in result
                                                             group c by new
                                                             {
                                                                 c.CategoryID,
                                                                 c.CategoryName,
                                                                 c.CategoryImageFileName,
                                                                 c.SubCategoryID,
                                                                 c.SubCategoryName,
                                                                 c.SubCategoryImageFileName,
                                                             } into gcs
                                                             select new TranscriptAssessmmentHistoryParent()
                                                             {
                                                                 CategoryID = gcs.Key.CategoryID,
                                                                 CategoryName = gcs.Key.CategoryName,
                                                                 CategoryImageFileName = gcs.Key.CategoryImageFileName,
                                                                 SubCategoryID = gcs.Key.SubCategoryID,
                                                                 SubCategoryName = gcs.Key.SubCategoryName,
                                                                 SubCategoryImageFileName = gcs.Key.SubCategoryImageFileName,
                                                                 SubTotalEndPoints = gcs.Sum(e => e.EndPoint),
                                                                 SubTotal_TotalPoints = gcs.Sum(e => e.TotalPoint),
                                                                 TranscriptAssessmmentHistoryChildren = (from gc in gcs.ToList()
                                                                                                         select new TranscriptAssessmmentHistoryChild()
                                                                                                         {
                                                                                                             AssessmentID = gc.AssessmentID,
                                                                                                             Title = gc.Title,
                                                                                                             LastLaunchDate = gc.LastLaunchDate,
                                                                                                             Progress = gc.Progress,
                                                                                                             tTime = gc.tTime,
                                                                                                             EventID = gc.EventID,
                                                                                                             MinPassGrade = gc.MinPassGrade,
                                                                                                             TotalPoint = gc.TotalPoint,
                                                                                                             TimesTaken = gc.TimesTaken,
                                                                                                             EndPoint = gc.EndPoint,
                                                                                                             BestScore = gc.BestScore,
                                                                                                             PointsEarned = gc.PointsEarned,
                                                                                                             LinkedInPublish = gc.LinkedInPublish,
                                                                                                             NumberOfQuestions = gc.NumberOfQuestions,
                                                                                                             TimeLimitMinutes = gc.TimeLimitMinutes,
                                                                                                             Overview = gc.Overview,
                                                                                                             AssessmentType = gc.AssessmentType,
                                                                                                         })
                                                             };

                    return transcriptAssessmmentHistoryParent;
                }
                else
                {
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                    return transcriptAssessments;
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<IEnumerable<TranscriptCourseHistoryParent>> GetTranscriptCourseHistory(int UserID, int CategoryID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _transcriptRepo.GetTranscriptCourseHistory(UserID, CategoryID);
                IEnumerable<TranscriptCourseHistoryParent> transcriptCourseHistoryParent = new List<TranscriptCourseHistoryParent>();

                if (result.Any())
                {
                    var transcriptCourseHistoryParent_result = from c in result
                                                               group c by new
                                                               {
                                                                   c.CategoryID,
                                                                   c.CategoryName,
                                                                   c.CategoryImageFileName,
                                                                   c.SubCategoryID,
                                                                   c.SubCategoryName,
                                                                   c.SubCategoryImageFileName,
                                                               } into gcs
                                                               select new TranscriptCourseHistoryParent()
                                                               {
                                                                   CategoryID = gcs.Key.CategoryID,
                                                                   CategoryName = gcs.Key.CategoryName,
                                                                   CategoryImageFileName = gcs.Key.CategoryImageFileName,
                                                                   SubCategoryID = gcs.Key.SubCategoryID,
                                                                   SubCategoryName = gcs.Key.SubCategoryName,
                                                                   SubCategoryImageFileName = gcs.Key.SubCategoryImageFileName,
                                                                   SubTotalEndPoints = gcs.Sum(e => e.EndPoint),
                                                                   SubTotal_TotalPoints = gcs.Sum(e => e.TotalPoint),
                                                                   TranscriptCourseHistoryChildren = (from gc in gcs.ToList()
                                                                                                      select new TranscriptCourseHistoryChild()
                                                                                                      {
                                                                                                          CourseID = gc.CourseID,
                                                                                                          Title = gc.Title,
                                                                                                          LastLaunchDate = gc.LastLaunchDate,
                                                                                                          Progress = gc.Progress,
                                                                                                          TTime = gc.TTime,
                                                                                                          EventID = gc.EventID,
                                                                                                          EndPoint = gc.EndPoint,
                                                                                                          TotalPoint = gc.TotalPoint,
                                                                                                          PointsEarned = gc.PointsEarned,
                                                                                                          LinkedInPublish = gc.LinkedInPublish,
                                                                                                      })
                                                               };

                    return transcriptCourseHistoryParent_result;
                }
                else
                {
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                    return transcriptCourseHistoryParent;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<TranscriptUserDetails>> GetTranscriptUserDetails(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _transcriptRepo.GetTranscriptUserDetails(UserID);
                if (!result.Any())
                    ErrorsMessages.Add(LearnerAppConstants.NoRecordsFound);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TranscriptUserPublicURL> GetTranscriptUserPublicURL(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                var result = await _transcriptRepo.GetTranscriptUserPublicURL(UserID);
                if (result != null)
                    ErrorsMessages.Add(string.Format(LearnerAppConstants.NoRecordsFoundDynamic, "certificates"));
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
