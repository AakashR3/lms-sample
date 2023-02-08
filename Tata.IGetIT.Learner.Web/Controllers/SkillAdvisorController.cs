using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SkillAdvisorController : BaseController
    {
        private readonly ISkillAdvisorService _skillAdvisorService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public SkillAdvisorController(ISkillAdvisorService skillAdvisorService)
        {
            _skillAdvisorService = skillAdvisorService ?? throw new ArgumentNullException(nameof(ISkillAdvisorService));
        }
        #region GET

        [HttpGet("UserTypeRoles")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<SkillAdvisor_UserTypeRoles>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<SkillAdvisor_UserTypeRoles>))]
        public async Task<IActionResult> UserTypeRoles()
        {
            try
            {
                List<string> ErrorMessages = new();
                var result = await _skillAdvisorService.GetUserTypeRoles(ErrorMessages);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SkillAdvisor_UserTypeRoles>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SkillAdvisor_UserTypeRoles>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SkillAdvisor_UserTypeRoles>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// To load master categories for skill advisor
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        [HttpGet("Categories/{RoleID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<SkillAdvisor_Categories>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<SkillAdvisor_Categories>))]
        public async Task<IActionResult> GetCategories(int RoleID)
        {
            try
            {
                List<string> ErrorMessages = new();
                var result = await _skillAdvisorService.GetCategories(RoleID,ErrorMessages);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SkillAdvisor_Categories>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SkillAdvisor_Categories>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SkillAdvisor_Categories>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        [HttpGet("SoftwareList")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<SkillAdvisor_Softwares>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<SkillAdvisor_Softwares>))]
        public async Task<IActionResult> GetSoftwareList()
        {
            try
            {
                List<string> ErrorMessages = new();
                var result = await _skillAdvisorService.GetSoftwareList(ErrorMessages);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SkillAdvisor_Softwares>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SkillAdvisor_Softwares>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SkillAdvisor_Softwares>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// Filter for Skill advisor
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="ToolID"></param>
        /// <param name="CountryCode"></param>
        /// <returns></returns>
        [HttpGet("Subscriptions/{RoleID}/{ToolID}/{CountryCode}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<SkillAdvisor_PersonalPlan>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<SkillAdvisor_PersonalPlan>))]
        public async Task<IActionResult> GetSubscriptions(int RoleID, int ToolID, string CountryCode)
        {
            try
            {
                List<string> ErrorMessages = new();
                var result = await _skillAdvisorService.GetSubscriptions(RoleID, ToolID, CountryCode, ErrorMessages);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SkillAdvisor_PersonalPlan>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SkillAdvisor_PersonalPlan>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SkillAdvisor_PersonalPlan>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("Courses/{SID_Y}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<SkillAdvisor_Courses_Parent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<SkillAdvisor_Courses_Parent>))]
        public async Task<IActionResult> GetSkillAdvisorCourses(int SID_Y, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                List<string> ErrorMessages =new ();
                var result = await _skillAdvisorService.GetSkillAdvisorCourses(SID_Y, ErrorMessages);

                if (result.Any())
                {
                    string message = string.Empty;
                    int totalRecords = result.Count();
                    var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                    SkillAdvisor_Courses_Parent skillAdvisor_Courses_Parent = new()
                    {
                        PageNumber = PageNumber,
                        PageSize = PageSize,
                        TotalItems = totalRecords,
                        TotalPages = validFilter.TotalPages
                    };

                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();
                        skillAdvisor_Courses_Parent.skillAdvisor_Courses = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.Failure;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SkillAdvisor_Courses_Parent>()
                    {
                        Message = message,
                        Data = skillAdvisor_Courses_Parent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SkillAdvisor_Courses_Parent>() { Message = LearnerAppConstants.Failure, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SkillAdvisor_Courses_Parent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("Assessments/{SID_Y}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<SkillAdvisor_Assessments_Parent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<SkillAdvisor_Assessments_Parent>))]
        public async Task<IActionResult> GetSkillAdvisorAssessments(int SID_Y, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                List<string> ErrorMessages = new();
                var result = await _skillAdvisorService.GetSkillAdvisorAssessments(SID_Y, ErrorMessages);

                if (result.Any())
                {
                    string message = string.Empty;
                    int totalRecords = result.Count();
                    var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                    SkillAdvisor_Assessments_Parent skillAdvisor_Assessments_Parent = new()
                    {
                        PageNumber = PageNumber,
                        PageSize = PageSize,
                        TotalItems = totalRecords,
                        TotalPages = validFilter.TotalPages
                    };

                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();
                        skillAdvisor_Assessments_Parent.skillAdvisor_Assessments = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.Failure;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SkillAdvisor_Assessments_Parent>()
                    {
                        Message = message,
                        Data = skillAdvisor_Assessments_Parent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Failure);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SkillAdvisor_Assessments_Parent>() { Message = LearnerAppConstants.Failure, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SkillAdvisor_Assessments_Parent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion
    }
}
