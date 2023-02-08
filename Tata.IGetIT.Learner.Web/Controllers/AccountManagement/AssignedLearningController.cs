using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.TermStore;
using Microsoft.Identity.Client;
using Razorpay.Api;
using Recurly.Resources;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Net;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.AccountManagement;
using Tata.IGetIT.Learner.Service.Interfaces.AccountManagement;

namespace Tata.IGetIT.Learner.Web.Controllers.AccountManagement
{

    [Route("api/[controller]")]
    [ApiController]
    public class AssignedLearningController : BaseController
    {
        private readonly IAssignedLearningService _assignedLearningService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public AssignedLearningController(IAssignedLearningService assignedLearningService)
        {
            _assignedLearningService = assignedLearningService ?? throw new ArgumentNullException(nameof(IAssignedLearningService));
        }

        #region GET


        [HttpGet("PathIDtoCreateLP")]
        [SwaggerResponse(200, "Ok", typeof(int))]
        [SwaggerResponse(500, "Internal Server Error", typeof(int))]
        public async Task<IActionResult> GetPathIDtoCreateLP()
        {
            try
            {
                var result = await _assignedLearningService.GetPathIDtoCreateLP();
                if (result>0)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<int>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<int>() { Message = LearnerAppConstants.NoRecordsFound, Data = 0 });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<int>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("Categories/{UserID}/{Type}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<AssignedLearningCatagories>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<AssignedLearningCatagories>))]
        public async Task<IActionResult> GetCatagories(int UserID = 516518, int Type = 1)
        {
            try
            {
                var result = await _assignedLearningService.GetCatagories(UserID, Type);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AssignedLearningCatagories>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AssignedLearningCatagories>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<AssignedLearningCatagories>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        [HttpGet("SubCategories/{UserID}/{Type}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<AssignedLearningSubCatagories>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<AssignedLearningSubCatagories>))]
        public async Task<IActionResult> GetSubCatagories(int UserID = 516518, int CategoryID=0, int Type = 1)
        {
            try
            {
                var result = await _assignedLearningService.GetSubCatagories(UserID, CategoryID, Type);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AssignedLearningSubCatagories>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AssignedLearningSubCatagories>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<AssignedLearningSubCatagories>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// GetLPByManager
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ShowPredefined"></param>
        /// <returns></returns>
        [HttpGet("LearningAssignments/{UserID}/{ShowPredefined}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<LearningAssignments>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<LearningAssignments>))]
        public async Task<IActionResult> LearningAssignments(int UserID = 47856, int ShowPredefined = 0)
        {
            try
            {
                var result = await _assignedLearningService.GetLearningAssignments(UserID, ShowPredefined);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<LearningAssignments>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<LearningAssignments>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<LearningAssignments>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// lp_GetLPByManager
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ShowPredefined"></param>
        /// <returns></returns>
        [HttpGet("PreDefinedLearningPath/{UserID}/{ShowPredefined}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<PreDefinedLearningPathsParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<PreDefinedLearningPathsParent>))]
        public async Task<IActionResult> PreDefinedLearningPath(int UserID = 516356, int ShowPredefined = 1, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _assignedLearningService.GetPreDefinedLearningPaths(UserID, ShowPredefined);
                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);
                /*if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<PreDefinedLearningPaths>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<PreDefinedLearningPaths>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/
                PreDefinedLearningPathsParent preDefinedLearningPathsParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        preDefinedLearningPathsParent.PreDefinedLearningPaths = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<PreDefinedLearningPathsParent>()
                    {
                        Message = message,
                        Data = preDefinedLearningPathsParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<PreDefinedLearningPathsParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = preDefinedLearningPathsParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<PreDefinedLearningPaths>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// lp_GetCoursesForLP
        /// </summary>
        /// <param name="AccountID"></param>
        /// <param name="SubcategoryID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [HttpGet("CoursesForLearningPath/{AccountID}/{SubcategoryID}/{CategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CoursesForLearningPathParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CoursesForLearningPathParent>))]
        public async Task<IActionResult> CoursesForLearningPath(int AccountID = 10, int SubcategoryID = 194, int CategoryID = 0, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _assignedLearningService.GetCoursesForLearningPath(AccountID, SubcategoryID, CategoryID);
                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                /*if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CoursesForLearningPath>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CoursesForLearningPath>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/


                CoursesForLearningPathParent coursesForLearningPathParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        coursesForLearningPathParent.CoursesForLearningPaths = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CoursesForLearningPathParent>()
                    {
                        Message = message,
                        Data = coursesForLearningPathParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CoursesForLearningPathParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = coursesForLearningPathParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<CoursesForLearningPathParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// lp_GetAssessmentsForLP
        /// </summary>
        /// <param name="AccountID"></param>
        /// <param name="SubcategoryID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        [HttpGet("AssessmentsForLearningPath/{AccountID}/{SubcategoryID}/{CategoryID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<AssessmentsForLearningPathParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<AssessmentsForLearningPathParent>))]
        public async Task<IActionResult> AssessmentsForLearningPath(int AccountID = 120, int SubcategoryID = 194, int CategoryID = 0, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _assignedLearningService.GetAssessmentsForLearningPath(AccountID, SubcategoryID, CategoryID);
                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                /*if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AssessmentsForLearningPath>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AssessmentsForLearningPath>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/

                AssessmentsForLearningPathParent assessmentsForLearningPathParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        assessmentsForLearningPathParent.AssessmentsForLearningPath = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssessmentsForLearningPathParent>()
                    {
                        Message = message,
                        Data = assessmentsForLearningPathParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssessmentsForLearningPathParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = assessmentsForLearningPathParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<AssessmentsForLearningPathParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// V2_lp_GetLearningPlatformCoursesForLP
        /// </summary>
        /// <returns></returns>
        [HttpGet("IntegrationsForLearningPath")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<IntegrationsForLearningPathParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<IntegrationsForLearningPathParent>))]
        public async Task<IActionResult> IntegrationsForLearningPath(int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _assignedLearningService.GetIntegrationsForLearningPath();
                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);
                /*if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<IntegrationsForLearningPathParent>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<IntegrationsForLearningPathParent>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/
                IntegrationsForLearningPathParent integrationsForLearningPathParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        integrationsForLearningPathParent.IntegrationsForLearningPath = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IntegrationsForLearningPathParent>()
                    {
                        Message = message,
                        Data = integrationsForLearningPathParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IntegrationsForLearningPathParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = integrationsForLearningPathParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IntegrationsForLearningPathParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// lp_GetAssignedUsersForLP_v2 : To get user details by user ids or Path ID
        /// </summary>
        /// <param name="UserIDs"></param>
        /// <param name="PathID"></param>
        /// <returns></returns>
        [HttpGet("AssignedUsersForLearningPath/{UserIDs}/{PathID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<AssignedUsersForLearningPathParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<AssignedUsersForLearningPathParent>))]
        public async Task<IActionResult> AssignedUsersForLearningPath(string UserIDs = "47856,47857", int PathID = 1820, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _assignedLearningService.GetAssignedUsersForLearningPath(UserIDs, PathID);

                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);
                /*if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AssignedUsersForLearningPathParent>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AssignedUsersForLearningPathParent>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/

                AssignedUsersForLearningPathParent assignedUsersForLearningPathParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        assignedUsersForLearningPathParent.AssignedUsersForLearningPaths = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssignedUsersForLearningPathParent>()
                    {
                        Message = message,
                        Data = assignedUsersForLearningPathParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssignedUsersForLearningPathParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = assignedUsersForLearningPathParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<AssignedUsersForLearningPath>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// acc_GetGroupListByManager : Owner,Admin,Author,Reporter will see all groups, Manager, Student,Temporary only see assinged groups
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ShowAll"></param>
        /// <returns></returns>
        [HttpGet("GroupListByManager/{UserID}/{ShowAll}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<GroupListByManagerParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<GroupListByManagerParent>))]
        public async Task<IActionResult> GroupListByManager(int UserID = 47856, bool ShowAll = true, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _assignedLearningService.GetGroupListByManagerForLearningPath(UserID, ShowAll); 
                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);
                /*
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<GroupListByManager>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<GroupListByManager>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }
                */

                GroupListByManagerParent groupListByManagerParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        groupListByManagerParent.GroupListByManager = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<GroupListByManagerParent>()
                    {
                        Message = message,
                        Data = groupListByManagerParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<GroupListByManagerParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = groupListByManagerParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<GroupListByManagerParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// lp_GetConditionalUsers_V2 : List of Users for the selected Condition: 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="PathID"></param>
        /// <returns></returns>
        [HttpGet("ConditionalUsersForLearningPath/{UserID}/{PathID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<ConditionalUsersParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<ConditionalUsersParent>))]
        public async Task<IActionResult> ConditionalUsersForLearningPath(int UserID = 47856, int PathID = 4946, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _assignedLearningService.GetConditionalUsersForLearningPath(UserID, PathID);
                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);
                /*
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<ConditionalUsers>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<ConditionalUsers>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/

                ConditionalUsersParent conditionalUsersParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        conditionalUsersParent.ConditionalUsers = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<ConditionalUsersParent>()
                    {
                        Message = message,
                        Data = conditionalUsersParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<ConditionalUsersParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = conditionalUsersParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<ConditionalUsers>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// lp_GetAssignedGroupByPathID : To find group details of Owner,Admin,Manager or Author,Student,Reporter,Temporary by path ID
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="PathID"></param>
        /// <returns></returns>
        [HttpGet("AssignedGroupByPathID/{UserID}/{PathID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<AssignedGroupByPathIDParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<AssignedGroupByPathIDParent>))]
        public async Task<IActionResult> AssignedGroupByPathID(int UserID = 47856, int PathID = 4240, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _assignedLearningService.GetAssignedGroupByPathID(UserID, PathID); string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);
                /*
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AssignedGroupByPathID>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<AssignedGroupByPathID>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/

                AssignedGroupByPathIDParent assignedGroupByPathIDParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        assignedGroupByPathIDParent.AssignedGroupByPathID = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssignedGroupByPathIDParent>()
                    {
                        Message = message,
                        Data = assignedGroupByPathIDParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssignedGroupByPathIDParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = assignedGroupByPathIDParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<AssignedGroupByPathID>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// lp_GetUnAssignedGroupByPathID: To get unassigned group details of Owner,Admin or Manager, Author,Student,Reporter,Temporary
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="PathID"></param>
        /// <returns></returns>
        [HttpGet("UnAssignedGroupByPathID/{UserID}/{PathID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<UnAssignedGroupByPathIDParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<UnAssignedGroupByPathIDParent>))]
        public async Task<IActionResult> UnAssignedGroupByPathID(int UserID = 47856, int PathID = 4240, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _assignedLearningService.GetUnAssignedGroupByPathID(UserID, PathID);
                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);
                /*
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<UnAssignedGroupByPathID>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<UnAssignedGroupByPathID>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/

                UnAssignedGroupByPathIDParent unAssignedGroupByPathIDParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        unAssignedGroupByPathIDParent.UnAssignedGroupByPathID = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UnAssignedGroupByPathIDParent>()
                    {
                        Message = message,
                        Data = unAssignedGroupByPathIDParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UnAssignedGroupByPathIDParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = unAssignedGroupByPathIDParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<UnAssignedGroupByPathIDParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// app_GetSubcategories_V2: To load sub categories based on Type. 
        /// Type=22 --Predefined Learning Paths
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CategoryID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        [HttpGet("SubCategories/{UserID}/{CategoryID}/{Type}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<SubcategoriesForLearningPath>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<SubcategoriesForLearningPath>))]
        public async Task<IActionResult> SubCategories(int UserID = 47856, int CategoryID = 0, int Type = 22)
        {
            try
            {
                var result = await _assignedLearningService.GetSubcategories(UserID, CategoryID, Type);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SubcategoriesForLearningPath>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SubcategoriesForLearningPath>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SubcategoriesForLearningPath>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// lp_GetNotificationEmail: To get EmailMessage, EmailSubject, MessageWithItems, ForceOrder, Name, Description by PathID
        /// </summary>
        /// <param name="PathID"></param>
        /// <returns></returns>
        [HttpGet("NotificationEmail/{PathID}")]
        [SwaggerResponse(200, "Ok", typeof(NotificationEmail))]
        [SwaggerResponse(500, "Internal Server Error", typeof(NotificationEmail))]
        public async Task<IActionResult> GetNotificationEmail(int PathID = 261)
        {
            try
            {
                var result = await _assignedLearningService.GetNotificationEmail(PathID);
                if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<NotificationEmail>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<NotificationEmail>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<NotificationEmail>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// V2_acc_GetDynamicFieldOptions
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="FieldID"></param>
        /// <returns></returns>
        [HttpGet("DynamicFieldOptions/{UserID}/{FieldID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<DynamicFieldOptions>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<DynamicFieldOptions>))]
        public async Task<IActionResult> GetDynamicFieldOptions(int UserID = 516465, int FieldID = 2)
        {
            try
            {
                var result = await _assignedLearningService.GetDynamicFieldOptions(UserID, FieldID);
                if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<DynamicFieldOptions>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<DynamicFieldOptions>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<DynamicFieldOptions>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// usr_GetUserListByAccountIDOrGroupID
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="GroupID"></param>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        [HttpGet("UserListByAccountIDOrGroupID/{AccountID}/{GroupID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<UserListByAccountIDOrGroupIDParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<UserListByAccountIDOrGroupIDParent>))]
        public async Task<IActionResult> GetUserListByAccountIDOrGroupID(DateTime? StartDate, DateTime? EndDate, int GroupID = 3, int AccountID = 166428, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _assignedLearningService.GetUserListByAccountIDOrGroupID(AccountID, GroupID, StartDate, EndDate);
                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);
                /*
                if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<UserListByAccountIDOrGroupID>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<UserListByAccountIDOrGroupID>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/

                UserListByAccountIDOrGroupIDParent userListByAccountIDOrGroupIDParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        userListByAccountIDOrGroupIDParent.UserListByAccountIDOrGroupIDs = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UserListByAccountIDOrGroupIDParent>()
                    {
                        Message = message,
                        Data = userListByAccountIDOrGroupIDParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UserListByAccountIDOrGroupIDParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = userListByAccountIDOrGroupIDParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<UserListByAccountIDOrGroupIDParent>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// V2_rp_GetLearningPathItems
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="UserID"></param>
        /// <param name="PathID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        [HttpGet("LearningPathItems/{UserID}/{PathID}/{GroupID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<LearningPathItems>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<LearningPathItems>))]
        public async Task<IActionResult> GetLearningPathItems(DateTime? StartDate, DateTime? EndDate, int UserID = 163975, int PathID = 1820, int GroupID = 0)
        {
            try
            {
                var result = await _assignedLearningService.GetLearningPathItems(UserID, PathID, GroupID, StartDate, EndDate);
                if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<LearningPathItems>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<LearningPathItems>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<LearningPathItems>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        [HttpGet("UserListByManager/{UserID}/{GroupID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<UserListByManager>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<UserListByManager>))]
        public async Task<IActionResult> GetUserListByManager(int UserID= 516518, int GroupID=0)
        {
            try
            {
                var result = await _assignedLearningService.GetUserListByManager(UserID,GroupID);
                if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<UserListByManager>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<UserListByManager>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<UserListByManager>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// lp_AssignUserToLP_V2
        /// </summary>
        /// <param name="assignedLearningInputs"></param>
        /// <returns></returns>
        [HttpPost("AssignUserToLearningPath")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<AssignedLearningInputs>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<AssignedLearningInputs>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<AssignedLearningInputs>))]
        public async Task<IActionResult> AssignUserToLearningPath([FromBody] AssignedLearningInputs assignedLearningInputs)
        {
            try
            {
                var result = await _assignedLearningService.AssignUserToLearningPath(assignedLearningInputs);
                if (result)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssignedLearningInputs>() { Message = LearnerAppConstants.Success, Data = assignedLearningInputs });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordIsInserted);
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<AssignedLearningInputs>() { Message = LearnerAppConstants.NoRecordIsInserted, Data = assignedLearningInputs });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<AssignedLearningInputs>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// V2_lp_SetDynamicGroupAttribute
        /// </summary>
        /// <param name="setDynamicGroupAttribute"></param>
        /// <returns></returns>
        [HttpPost("SetDynamicGroupAttribute")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<SetDynamicGroupAttribute>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<SetDynamicGroupAttribute>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<SetDynamicGroupAttribute>))]
        public async Task<IActionResult> SetDynamicGroupAttribute([FromBody] SetDynamicGroupAttribute setDynamicGroupAttribute)
        {
            try
            {
                var result = await _assignedLearningService.SetDynamicGroupAttribute(setDynamicGroupAttribute);
                if (result)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<SetDynamicGroupAttribute>() { Message = LearnerAppConstants.Success });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.DatabaseOperationFailed);
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<SetDynamicGroupAttribute>() { Message = LearnerAppConstants.DatabaseOperationFailed });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<SetDynamicGroupAttribute>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// SP: V2_lp_LPItemAction, Action Parameter accepts, 'ADD'-- ADD ITEM, DELETE'-- REMOVE ITEM, 'SEQ'-- Change sequence of the ITEM, 'SETDUE' -- set due date for item, 'SETSTART'-- set start date for item, 'SETNULL'-- set start and end date as null
        /// </summary>
        /// <param name="learningPathInput"></param>
        /// <returns></returns>
        [HttpPost("LPItemAction")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<LearningPathInput>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<LearningPathInput>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<LearningPathInput>))]
        public async Task<IActionResult> LPItemAction([FromBody] LearningPathInput learningPathInput)
        {
            try
            {
                var result = await _assignedLearningService.LPItemAction(learningPathInput);
                if (result)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<LearningPathInput>() { Message = LearnerAppConstants.Success });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.DatabaseOperationFailed);
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<LearningPathInput>() { Message = LearnerAppConstants.DatabaseOperationFailed });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<LearningPathInput>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// lp_LearningPathActions
        /// </summary>
        /// <param name="learningPathActionsInput"></param>
        /// <returns></returns>
        [HttpPost("LearningPathActionsInput")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<LearningPathActionsInput>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<LearningPathActionsInput>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<LearningPathActionsInput>))]
        public async Task<IActionResult> LearningPathActions([FromBody] LearningPathActionsInput learningPathActionsInput)
        {
            try
            {
                var result = await _assignedLearningService.LearningPathActions(learningPathActionsInput);
                if (result>0)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<LearningPathActionsInput>() { Message=LearnerAppConstants.Success, Output = result.ToString() });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.DatabaseOperationFailed);
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<LearningPathActionsInput>() { Message = LearnerAppConstants.DatabaseOperationFailed, Output = result.ToString() });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<LearningPathActionsInput>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// V2_lp_GetLearningPlatformCoursesForLP
        /// </summary>
        /// <param name="LearningPlatformCourseAction"></param>
        /// <returns></returns>
        [HttpPost("LPIntegrationsAction")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<LearningPlatformCourseAction>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<LearningPlatformCourseAction>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<LearningPlatformCourseAction>))]
        public async Task<IActionResult> LPIntegrationsAction([FromBody] LearningPlatformCourseAction LearningPlatformCourseAction)
        {
            try
            {
                var result = await _assignedLearningService.LPIntegrationsAction(LearningPlatformCourseAction);
                if (result)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<LearningPlatformCourseAction>() { Message = LearnerAppConstants.Success });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.DatabaseOperationFailed);
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<LearningPlatformCourseAction>() { Message = LearnerAppConstants.DatabaseOperationFailed });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<LearningPlatformCourseAction>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// lp_AssignGroupToLP
        /// </summary>
        /// <param name="AssignGroupToLP"></param>
        /// <returns></returns>
        [HttpPost("AssignGroupToLP")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<AssignGroupToLP>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<AssignGroupToLP>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<AssignGroupToLP>))]
        public async Task<IActionResult> AssignGroupToLP([FromBody] AssignGroupToLP AssignGroupToLP)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _assignedLearningService.InsertAssignGroupToLP(AssignGroupToLP);
                    if (result)
                    {
                        logger.LogDebug(LearnerAppConstants.Success);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AssignGroupToLP>() { Message = LearnerAppConstants.Success });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.DatabaseOperationFailed);
                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<AssignGroupToLP>() { Message = LearnerAppConstants.DatabaseOperationFailed });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = AssignGroupToLP
                    });

                    return await PopulateModelErrorsAsync();
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<AssignGroupToLP>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion

        #region DELETE

        [HttpDelete("RemoveGroupFromLP")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<RemoveGroupFromLP>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<RemoveGroupFromLP>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<RemoveGroupFromLP>))]
        public async Task<IActionResult> RemoveGroupFromLP([FromBody] RemoveGroupFromLP RemoveGroupFromLP)
        {
            try
            {
                var result = await _assignedLearningService.RemoveGroupFromLP(RemoveGroupFromLP);
                if (result)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<RemoveGroupFromLP>() { Message = LearnerAppConstants.Success });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.DatabaseOperationFailed);
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<RemoveGroupFromLP>() { Message = LearnerAppConstants.DatabaseOperationFailed });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<RemoveGroupFromLP>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpDelete("RemoveUserFromLP")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<RemoveUserFromLP>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<RemoveUserFromLP>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<RemoveUserFromLP>))]
        public async Task<IActionResult> RemoveUserFromLP([FromBody] RemoveUserFromLP removeUserFromLP)
        {
            try
            {
                var result = await _assignedLearningService.RemoveUserFromLP(removeUserFromLP);
                if (result)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<RemoveUserFromLP>() { Message = LearnerAppConstants.Success });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.DatabaseOperationFailed);
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<RemoveUserFromLP>() { Message = LearnerAppConstants.DatabaseOperationFailed });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<RemoveUserFromLP>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion
    }
}
