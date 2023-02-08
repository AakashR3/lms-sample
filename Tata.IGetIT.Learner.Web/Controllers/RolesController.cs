using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController
    {
        private readonly IRolesService _RolesService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public RolesController(IRolesService RolesService)
        {
            _RolesService = RolesService ?? throw new ArgumentNullException(nameof(RolesService));

        }

        /// <summary>
        /// Get Roles List
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>RolesListData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<RolesListData>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<RolesListData>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<RolesListData>))]
        [Route("RolesListData/{UserID}")]
        public async Task<IActionResult> GetRolesList(int UserID, int Mode, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                string message = string.Empty;
                AdminRole AdminRole = new AdminRole();
                AdminRole.UserID = UserID;
                AdminRole.Mode = Mode;

                var result = await _RolesService.GetRolesList(AdminRole);

                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                RolesGridDataParent RolesGridDataParent = new RolesGridDataParent();
                RolesGridDataParent.PageNumber = PageNumber;
                RolesGridDataParent.PageSize = PageSize;
                RolesGridDataParent.TotalItems = totalRecords;
                RolesGridDataParent.TotalPages = validFilter.TotalPages;

                if (result != null && result.Count() > 0)
                {
                    result = result
                            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize).ToList();
                    RolesGridDataParent.RolesListData = result;
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.ROLESKILLCOMPETENCY_GETROLESLIST_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<RolesGridDataParent>()
                {
                    Message = message,
                    Data = RolesGridDataParent
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetRolesList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Role Details
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>RolesListData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(RolesListData))]
        [SwaggerResponse(400, "Bad Request", typeof(RolesListData))]
        [SwaggerResponse(500, "Internal Server Error", typeof(RolesListData))]
        [Route("RoleDetails/{ID}")]
        public async Task<IActionResult> GetRoleDetails(int ID, int UserID = 0, int Mode = 1, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                string message = string.Empty;
                AdminRole AdminRole = new AdminRole();
                AdminRole.ID = ID;
                AdminRole.UserID = UserID;
                AdminRole.Mode = Mode;

                var result = await _RolesService.GetRoleDetails(AdminRole);

                if (result != null)
                {
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.ROLESKILLCOMPETENCY_GETROLESDETAILS_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<RolesListData>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetRolesList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// To add or edit a Role
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        [HttpPost("AddEditRole")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> AddEditRole([FromBody] MultipleRoles role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _RolesService.AddEditRole(role, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<MultipleRoles>() { Message = errMsg, Data = role });
                    }
                    else
                    {
                        string returnMessage = string.Empty;

                        if (role.ID > 0)
                            returnMessage = LearnerAppConstants.ROLESKILLCOMPETENCY_ROLE_UPDATED;
                        else
                            returnMessage = LearnerAppConstants.ROLESKILLCOMPETENCY_ROLE_ADDED;

                        logger.LogDebug(returnMessage);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Role>()
                        {
                            Message = returnMessage
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = role
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(AddEditRole)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<MultipleRoles>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = role });
            }
        }

        /// <summary>
        /// To delete a role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpDelete("DeleteRole")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> DeleteRole([FromBody] DeleteRole role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _RolesService.DeleteRole(role, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<DeleteRole>() { Message = errMsg, Data = role });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.ROLESKILLCOMPETENCY_ROLE_DELETED);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<DeleteRole>()
                        {
                            Message = LearnerAppConstants.ROLESKILLCOMPETENCY_ROLE_DELETED
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = role
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(DeleteRole)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<DeleteRole>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = role });
            }
        }

        /// <summary>
        /// To add or edit a Role
        /// </summary>
        /// <param name="MultipleRoles"></param>
        /// <returns></returns>
        [HttpPost("AddPublicRoles")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> AddMultipleRoles([FromBody] List<MultipleRoles> role)
        {
            int successCount = 0;
            int failedCount = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();

                    if (role.Count > 0)
                    {
                        for (int i = 0; i < role.Count; i++)
                        {
                            var result = await _RolesService.AddEditRole(role[i], errorMessage);
                            if (errorMessage.Any())
                            {
                                var errMsg = errorMessage.FirstOrDefault();
                                role[i].ReturnMessage = errMsg;
                                role[i].Success = 0;
                                failedCount = failedCount + 1;
                            }
                            else
                            {
                                role[i].ReturnMessage = LearnerAppConstants.ROLESKILLCOMPETENCY_ROLE_ADDED;
                                role[i].Success = 1;
                                successCount = successCount + 1;
                            }
                        }
                    }

                    PublicRoles PublicRoles = new PublicRoles();
                    PublicRoles.FailedCount = failedCount;
                    PublicRoles.SuccessCount = successCount;
                    PublicRoles.Role = role;

                    string returnMessage = string.Empty;

                    returnMessage = failedCount > 0 ? LearnerAppConstants.ROLES_PUBLICROLES_ADDED_WITHFAILURE : LearnerAppConstants.ROLES_PUBLICROLES_ADDED;

                    logger.LogDebug(returnMessage);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<PublicRoles>()
                    {
                        Message = returnMessage,
                        Data = PublicRoles
                    });
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = role
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(AddMultipleRoles)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<List<MultipleRoles>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = role });
            }
        }
    }
}