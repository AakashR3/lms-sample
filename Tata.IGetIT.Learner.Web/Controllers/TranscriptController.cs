using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TranscriptController : BaseController
    {
        private readonly ITranscriptService _transcriptService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public TranscriptController(ITranscriptService transcriptService)
        {
            _transcriptService = transcriptService ?? throw new ArgumentNullException(nameof(ITranscriptService));
        }
        #region GET

        /// <summary>
        /// To get Transcript user details
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("TranscriptUserDetails/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<TranscriptUserDetails>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<TranscriptUserDetails>))]
        public async Task<IActionResult> TranscriptUserDetails(int UserID)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _transcriptService.GetTranscriptUserDetails(UserID, errorMsg);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<TranscriptUserDetails>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TranscriptUserDetails>() { Message = LearnerAppConstants.Failure, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.EXCEPTION_MESSAGE,
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<TranscriptUserDetails>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// To get Transcript Course History
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [HttpGet("TranscriptCourseHistory/{UserID}/{CategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(TranscriptCourseHistoryParentGrid))]
        [SwaggerResponse(500, "Internal Server Error", typeof(TranscriptCourseHistoryParentGrid))]
        public async Task<IActionResult> TranscriptCourseHistory(int UserID, int CategoryID, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _transcriptService.GetTranscriptCourseHistory(UserID, CategoryID,errorMsg);
                
                if (result.Any())
                {
                    string message = string.Empty;
                    int totalRecords = result.Count();
                    var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                    TranscriptCourseHistoryParentGrid transcriptCourseHistoryParentGrid = new()
                    {
                        PageNumber = PageNumber,
                        PageSize = PageSize,
                        TotalItems = totalRecords,
                        TotalPages = validFilter.TotalPages
                    };

                    if (result != null && result.Any())
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();
                        transcriptCourseHistoryParentGrid.transcriptCourseHistoryParents = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.Failure;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TranscriptCourseHistoryParentGrid>()
                    {
                        Message = message,
                        Data = transcriptCourseHistoryParentGrid
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TranscriptCourseHistoryParentGrid>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.EXCEPTION_MESSAGE,
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<TranscriptCourseHistoryParentGrid>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// To get Transcript Assessment History
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [HttpGet("TranscriptAssessmentHistory/{UserID}/{CategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(TranscriptAssessmmentHistoryParentGrid))]
        [SwaggerResponse(500, "Internal Server Error", typeof(TranscriptAssessmmentHistoryParentGrid))]
        public async Task<IActionResult> TranscriptAssessmentHistory(int UserID, int CategoryID, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _transcriptService.GetTranscriptAssessmentHistory(UserID, CategoryID, errorMsg);
                
                if (result.Any())
                {
                    string message = string.Empty;
                    int totalRecords = result.Count();
                    var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                    TranscriptAssessmmentHistoryParentGrid transcriptAssessmmentHistoryParentGrid = new()
                    {
                        PageNumber = PageNumber,
                        PageSize = PageSize,
                        TotalItems = totalRecords,
                        TotalPages = validFilter.TotalPages
                    };

                    if (result != null && result.Any())
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();
                        transcriptAssessmmentHistoryParentGrid.transcriptAssessmmentHistoryParents = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.Failure;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TranscriptAssessmmentHistoryParentGrid>()
                    {
                        Message = message,
                        Data = transcriptAssessmmentHistoryParentGrid
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TranscriptCourseHistoryParentGrid>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.EXCEPTION_MESSAGE,
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<TranscriptAssessmmentHistoryParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("AssessmentProperties/{UserID}/{AssessmentID}")]
        [SwaggerResponse(200, "Ok", typeof(AssessmentProperties))]
        [SwaggerResponse(500, "Internal Server Error", typeof(AssessmentProperties))]
        public async Task<IActionResult> GetAssessmentProperties(int UserID,int AssessmentID)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _transcriptService.GetAssessmentProperties(UserID, AssessmentID, errorMsg);
                if (result!=null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssessmentProperties>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssessmentProperties>() { Message = LearnerAppConstants.Failure, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.EXCEPTION_MESSAGE,
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<AssessmentProperties>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("TranscriptUserPublicURL/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(TranscriptUserPublicURL))]
        [SwaggerResponse(500, "Internal Server Error", typeof(TranscriptUserPublicURL))]
        public async Task<IActionResult> GetTranscriptUserPublicURL(int UserID)
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _transcriptService.GetTranscriptUserPublicURL(UserID, errorMsg);
                if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TranscriptUserPublicURL>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TranscriptUserPublicURL>() { Message = LearnerAppConstants.Failure, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.EXCEPTION_MESSAGE,
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<TranscriptUserPublicURL>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion
    }
}
