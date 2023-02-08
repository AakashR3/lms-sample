using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.AccountManagement;
using Tata.IGetIT.Learner.Service.Interfaces.AccountManagement;

namespace Tata.IGetIT.Learner.Web.Controllers.AccountManagement
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : BaseController
    {
        private readonly IAdminService _adminService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public AdminDashboardController(IAdminService adminService)
        {
            _adminService = adminService ?? throw new ArgumentNullException(nameof(IAdminService));
        }

        [HttpGet("TopAssessments/{AccountID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<TopAssessmentParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<TopAssessmentParent>))]
        public async Task<IActionResult> GetTopAssessments(int AccountID = 139773, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _adminService.GetTopAssessments(AccountID);
                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                /*if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<TopAssessments>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<TopAssessments>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/

                TopAssessmentParent topAssessmentParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result.Any())
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        topAssessmentParent.topAssessments = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TopAssessmentParent>()
                    {
                        Message = message,
                        Data = topAssessmentParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TopAssessmentParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = topAssessmentParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<TopAssessmentParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("TopCourses/{AccountID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<TopCourseParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<TopCourseParent>))]
        public async Task<IActionResult> GetTopCourses(int AccountID, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _adminService.GetTopCourses(AccountID);
                string message = string.Empty;
                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);
                /*
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<TopCourses>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<TopCourses>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/

                TopCourseParent topCourseParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = totalRecords,
                    TotalPages = validFilter.TotalPages
                };

                if (result.Any())
                {
                    if (result != null && result.Count() > 0)
                    {
                        result = result
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        topCourseParent.topCourses = result;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TopCourseParent>()
                    {
                        Message = message,
                        Data = topCourseParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TopCourseParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = topCourseParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<TopCourseParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        [HttpGet("TopUsers/{AccountID}/{Num}/{GroupID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Topusers>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Topusers>))]
        public async Task<IActionResult> GetTopUsers(int AccountID, int Num, int GroupID)
        {
            try
            {
                var result = await _adminService.GetTopUsers(AccountID, Num, GroupID);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Topusers>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Topusers>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Topusers>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        [HttpGet("LoginMonths/{AccountID}/{GroupID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<LoginMonths>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<LoginMonths>))]
        public async Task<IActionResult> GetLoginMonths(int AccountID, int GroupID)
        {
            try
            {
                var result = await _adminService.GetLoginMonths(AccountID, GroupID);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<LoginMonths>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<LoginMonths>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<LoginMonths>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /*
                [HttpGet("AdminDashboardDetails/{AccountID}")]
                [SwaggerResponse(200, "Ok", typeof(AdminDashboardDetailParent))]
                [SwaggerResponse(500, "Internal Server Error", typeof(AdminDashboardDetailParent))]
                public async Task<IActionResult> GetAdminDashboardDetails(int AccountID, int PageNumber = 1, int PageSize = 10)
                {
                    try
                    {
                        var result = await _adminService.GetAdminDashboardDetails(AccountID);
                        string message = string.Empty;
                        int totalRecords = result.currentSubscriptions.Count();
                        var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);
                        *//*if (result!=null)
                        {
                            logger.LogDebug(LearnerAppConstants.Success);
                            return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AdminDashboardDetails>() { Message = LearnerAppConstants.Success, Data = result });
                        }
                        else
                        {
                            logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                            return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AdminDashboardDetails>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                        }*//*
                        AdminDashboardDetailParent adminDashboardDetailParent = new()
                        {
                            PageNumber = PageNumber,
                            PageSize = PageSize,
                            TotalItems = totalRecords,
                            TotalPages = validFilter.TotalPages
                        };

                        if (result != null)
                        {
                            if (result != null && result.currentSubscriptions.Count() > 0)
                            {
                                var resutl_userScoreCards = result.userScoreCards;
                                var resultCurrentSubscriptions = result.currentSubscriptions
                                        .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                        .Take(validFilter.PageSize).ToList();

                                adminDashboardDetailParent.userScoreCards = resutl_userScoreCards;
                                adminDashboardDetailParent.currentSubscriptions = resultCurrentSubscriptions;
                                message = LearnerAppConstants.Success;
                            }
                            else
                                message = LearnerAppConstants.NoRecordsFound;

                            return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AdminDashboardDetailParent>()
                            {
                                Message = message,
                                Data = adminDashboardDetailParent
                            });
                        }
                        else
                        {
                            logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                            return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<AdminDashboardDetailParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = adminDashboardDetailParent });
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
                        return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<AdminDashboardDetailParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
                    }
                }*/

        [HttpGet("DownloadUserRepor/{AccountID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<DownloadUserReport>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<DownloadUserReport>))]
        public async Task<IActionResult> DownloadUserRepor(int AccountID)
        {
            try
            {
                var result = await _adminService.DownloadUserReport(AccountID);
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<DownloadUserReport>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<DownloadUserReport>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<DownloadUserReport>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("UserScoreCards/{AccountID}")]
        [SwaggerResponse(200, "Ok", typeof(UserScoreCards))]
        [SwaggerResponse(500, "Internal Server Error", typeof(UserScoreCards))]
        public async Task<IActionResult> GetUserScoreCards(int AccountID)
        {
            try
            {
                var result = await _adminService.GetUserScoreCards(AccountID);
                if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UserScoreCards>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UserScoreCards>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<UserScoreCards>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        [HttpGet("CurrentSubscription/{AccountID}/{PageNumber}/{PageSize}")]
        [SwaggerResponse(200, "Ok", typeof(CurrentSubscriptionParent))]
        [SwaggerResponse(500, "Internal Server Error", typeof(CurrentSubscriptionParent))]
        public async Task<IActionResult> GetCurrentSubscription(int AccountID, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                var result = await _adminService.GetCurrentSubscription(AccountID, PageNumber, PageSize);

                string message = string.Empty;
                var validFilter = new PaginationFilter(PageNumber, PageSize, result.TotalItems);

                /*if (result != null)
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CurrentSubscriptionParent>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CurrentSubscriptionParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
                }*/
                
                CurrentSubscriptionParent currentSubscriptionParent = new()
                {
                    PageNumber = PageNumber,
                    PageSize = PageSize,
                    TotalItems = result.TotalItems,
                    TotalPages = validFilter.TotalPages
                };

                if (result != null)
                {
                    if (result != null && result.currentSubscriptions.Count() > 0)
                    {
                        var currentSub = result.currentSubscriptions
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize).ToList();

                        currentSubscriptionParent.currentSubscriptions = currentSub;
                        message = LearnerAppConstants.Success;
                    }
                    else
                        message = LearnerAppConstants.NoRecordsFound;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CurrentSubscriptionParent>()
                    {
                        Message = message,
                        Data = currentSubscriptionParent
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CurrentSubscriptionParent>() { Message = LearnerAppConstants.NoRecordsFound, Data = currentSubscriptionParent });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<CurrentSubscriptionParent>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
    }
}
