using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesStructureController : BaseController
    {
        private readonly IRolesStructureService _RolesStructureService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public RolesStructureController(IRolesStructureService RolesStructureService)
        {
            _RolesStructureService = RolesStructureService ?? throw new ArgumentNullException(nameof(RolesStructureService));

        }

        /// <summary>
        /// Get RolesStructure List
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>RolesStructureListData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<RolesStructureListData>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<RolesStructureListData>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<RolesStructureListData>))]
        [Route("RolesStructureListData/{UserID}")]
        public async Task<IActionResult> GetRolesStructureList(int UserID, string SearchText="", int Mode=1, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                string message = string.Empty;
                AdminRoleStructure AdminRoleStructure = new AdminRoleStructure();
                AdminRoleStructure.UserID = UserID;
                AdminRoleStructure.Mode = Mode;
                AdminRoleStructure.SearchText = SearchText;

                var result = await _RolesStructureService.GetRoleStructureList(AdminRoleStructure);

                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                RolesStructureGridDataParent RolesStructureGridDataParent = new RolesStructureGridDataParent();
                RolesStructureGridDataParent.PageNumber = PageNumber;
                RolesStructureGridDataParent.PageSize = PageSize;
                RolesStructureGridDataParent.TotalItems = totalRecords;
                RolesStructureGridDataParent.TotalPages = validFilter.TotalPages;

                if (result != null && result.Count() > 0)
                {
                    result = result
                            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize).ToList();
                    RolesStructureGridDataParent.RolesStructureListData = result;
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.STRUCTURE_GETLIST_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<RolesStructureGridDataParent>()
                {
                    Message = message,
                    Data = RolesStructureGridDataParent
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetRolesStructureList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Structure Details
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>RolesStructureListData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(RolesStructureListData))]
        [SwaggerResponse(400, "Bad Request", typeof(RolesStructureListData))]
        [SwaggerResponse(500, "Internal Server Error", typeof(RolesStructureListData))]
        [Route("StructureDetails/{ID}")]
        public async Task<IActionResult> GetRoleStructureDetails(int ID, int UserID = 0, int Mode = 1, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                string message = string.Empty;
                AdminRoleStructure AdminRoleStructure = new AdminRoleStructure();
                AdminRoleStructure.ID = ID;
                AdminRoleStructure.UserID = UserID;
                AdminRoleStructure.Mode = Mode;

                var result = await _RolesStructureService.GetRoleStructureDetails(AdminRoleStructure);

                if (result != null)
                {
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.STRUCTURE_GETDETAILS_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<RolesStructureListData>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetRolesStructureList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// To add or edit a Structure
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        [HttpPost("AddEditStructure")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> AddEditStructure([FromBody] RolesStructureParam structure)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _RolesStructureService.AddEditStructure(structure, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<RolesStructureParam>() { Message = errMsg, Data = structure });
                    }
                    else
                    {
                        string returnMessage = string.Empty;

                        if (structure.ID > 0)
                            returnMessage = LearnerAppConstants.STRUCTURE_UPDATED;
                        else
                            returnMessage = LearnerAppConstants.STRUCTURE_ADDED;

                        logger.LogDebug(returnMessage);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<RolesStructureParam>()
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
                        RequestParam = structure
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(AddEditStructure)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<RolesStructureParam>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = structure });
            }
        }

        /// <summary>
        /// To add a Structure
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        [HttpPost("AddStructure")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> AddStructure([FromBody] RolesStructureParam structure)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _RolesStructureService.AddStructure(structure, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<RolesStructureParam>() { Message = errMsg, Data = structure });
                    }
                    else
                    {
                        string returnMessage = string.Empty;

                        if (structure.ID > 0)
                            returnMessage = LearnerAppConstants.STRUCTURE_UPDATED;
                        else
                            returnMessage = LearnerAppConstants.STRUCTURE_ADDED;

                        logger.LogDebug(returnMessage);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<RolesStructureParam>()
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
                        RequestParam = structure
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(AddStructure)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<RolesStructureParam>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = structure });
            }
        }

        /// <summary>
        /// To delete a structure
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        [HttpDelete("DeleteStructure")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> DeleteStructure([FromBody] DeleteStructure structure)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _RolesStructureService.DeleteStructure(structure, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<DeleteStructure>() { Message = errMsg, Data = structure });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.STRUCTURE_DELETED);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<DeleteStructure>()
                        {
                            Message = LearnerAppConstants.STRUCTURE_DELETED
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = structure
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(DeleteStructure)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<DeleteStructure>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = structure });
            }
        }

        /// <summary>
        /// Get Learning Path for AccountID for Role Mapping 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>RolesStructureListData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<LearningPathRoleMapping>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<LearningPathRoleMapping>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<LearningPathRoleMapping>))]
        [Route("RoleStructureLearningPath/{UserID}")]
        public async Task<IActionResult> GetRoleStructureLearningPath(int UserID, int Mode = 1, int ID=0)
        {
            try
            {
                string message = string.Empty;
                AdminRoleStructure AdminRoleStructure = new AdminRoleStructure();
                AdminRoleStructure.UserID = UserID;
                AdminRoleStructure.Mode = Mode;
                AdminRoleStructure.ID = ID;

                var result = await _RolesStructureService.GetRoleStructureLearningPath(AdminRoleStructure);

                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.SUCCESSMESSAGE;

                else
                    message = Mode == 1 ? LearnerAppConstants.STRUCTURE_GETLPLIST_FAILUREMESSAGE : LearnerAppConstants.STRUCTURE_GETLPLISTROLE_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<LearningPathRoleMapping>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetRoleStructureLearningPath)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

         /// <summary>
        /// Get User's Role, Competency Map
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>RolesStructureListData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(UserRoleCompetency))]
        [SwaggerResponse(400, "Bad Request", typeof(UserRoleCompetency))]
        [SwaggerResponse(500, "Internal Server Error", typeof(UserRoleCompetency))]
        [Route("UserRoleCompetencyMap/{UserID}")]
        public async Task<IActionResult> GetUserRoleCompetencyMap(int UserID, int Mode = 1)
        {
            try
            {
                string message = string.Empty;
                AdminRoleStructure AdminRoleStructure = new AdminRoleStructure();
                AdminRoleStructure.UserID = UserID;
                AdminRoleStructure.Mode = Mode;

                var result = await _RolesStructureService.GetUserRoleCompetencyMap(AdminRoleStructure);

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;

                else
                    message = LearnerAppConstants.USERROLECOMPETENCY_GETLIST_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UserRoleCompetency>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetUserRoleCompetencyMap)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// To add or edit a User's Role Competency Mapping
        /// </summary>
        /// <param name="UserRoleCompetencyMapParam"></param>
        /// <returns></returns>
        [HttpPost("AddEditUserRoleCompetencyMap")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> AddEditUserRoleCompetencyMap([FromBody] UserRoleCompetencyMapParam userRoleCompetencyMapParam)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _RolesStructureService.AddEditUserRoleCompetencyMap(userRoleCompetencyMapParam, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<UserRoleCompetencyMapParam>() { Message = errMsg, Data = userRoleCompetencyMapParam });
                    }
                    else
                    {
                        string returnMessage = string.Empty;

                        if (userRoleCompetencyMapParam.ID > 0)
                            returnMessage = LearnerAppConstants.USERROLECOMPETENCY_ADDED;
                        else
                            returnMessage = LearnerAppConstants.USERROLECOMPETENCY_UPDATED;

                        logger.LogDebug(returnMessage);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<int>()
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
                        RequestParam = userRoleCompetencyMapParam
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(AddEditUserRoleCompetencyMap)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<UserRoleCompetencyMapParam>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = userRoleCompetencyMapParam });
            }
        }

    }
}