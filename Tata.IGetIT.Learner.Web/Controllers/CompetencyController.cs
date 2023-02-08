using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetencyController : BaseController
    {
        private readonly ICompetencyService _competencyService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public CompetencyController(ICompetencyService competencyService)
        {
            _competencyService = competencyService ?? throw new ArgumentNullException(nameof(competencyService));

        }

        /// <summary>
        /// Get Competency List
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>CompetencyListData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CompetencyListData>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<CompetencyListData>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CompetencyListData>))]
        [Route("CompetencyListData/{UserID}")]
        public async Task<IActionResult> GetCompetencyList(int UserID, int CompetencyType=0, int Mode=1, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                string message = string.Empty;
                AdminCompetency AdminCompetency = new AdminCompetency();
                AdminCompetency.UserID = UserID;
                AdminCompetency.Mode = Mode;
                AdminCompetency.CompetencyType = CompetencyType;

                var result = await _competencyService.GetCompetencyList(AdminCompetency);

                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                CompetencyGridDataParent CompetencyGridDataParent = new CompetencyGridDataParent();
                CompetencyGridDataParent.PageNumber = PageNumber;
                CompetencyGridDataParent.PageSize = PageSize;
                CompetencyGridDataParent.TotalItems = totalRecords;
                CompetencyGridDataParent.TotalPages = validFilter.TotalPages;

                if (result != null && result.Count() > 0)
                {
                    result = result
                            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize).ToList();
                    CompetencyGridDataParent.CompetencyListData = result;
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.COMPETENCY_GETLIST_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CompetencyGridDataParent>()
                {
                    Message = message,
                    Data = CompetencyGridDataParent
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetCompetencyList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Competency Details
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>CompetencyListData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(CompetencyListData))]
        [SwaggerResponse(400, "Bad Request", typeof(CompetencyListData))]
        [SwaggerResponse(500, "Internal Server Error", typeof(CompetencyListData))]
        [Route("CompetencyDetails/{ID}")]
        public async Task<IActionResult> GetCompetencyDetails(int ID, int UserID = 0, int Mode = 1, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                string message = string.Empty;
                AdminCompetency AdminCompetency = new AdminCompetency();
                AdminCompetency.ID = ID;
                AdminCompetency.UserID = UserID;
                AdminCompetency.Mode = Mode;

                var result = await _competencyService.GetCompetencyDetails(AdminCompetency);

                if (result != null)
                {
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.COMPETENCY_GETDETAILS_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CompetencyListData>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetCompetencyList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// To add or edit a Competency
        /// </summary>
        /// <param name="Competency"></param>
        /// <returns></returns>
        [HttpPost("AddEditCompetency")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> AddEditCompetency([FromBody] Competency competency)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();

                    var result = await _competencyService.AddEditCompetency(competency, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<Competency>() { Message = errMsg, Data = competency });
                    }
                    else
                    {
                        string returnMessage = string.Empty;

                        if (competency.ID > 0)
                            returnMessage = LearnerAppConstants.COMPETENCY_UPDATED;
                        else
                            returnMessage = LearnerAppConstants.COMPETENCY_ADDED;

                        logger.LogDebug(returnMessage);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Competency>()
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
                        RequestParam = competency
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(AddEditCompetency)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Competency>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = competency });
            }
        }

        /// <summary>
        /// To delete a competency
        /// </summary>
        /// <param name="competency"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCompetency")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> DeleteCompetency([FromBody] DeleteCompetency competency)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> errorMessage = new();
                    var result = await _competencyService.DeleteCompetency(competency, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<DeleteCompetency>() { Message = errMsg, Data = competency });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.COMPETENCY_DELETED);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<DeleteCompetency>()
                        {
                            Message = LearnerAppConstants.COMPETENCY_DELETED
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = competency
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(DeleteCompetency)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<DeleteCompetency>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = competency });
            }
        }

        /// <summary>
        /// Get Competency List
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>CompetencyLevelList</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CompetencyLevel>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<CompetencyLevel>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CompetencyLevel>))]
        [Route("CompetencyLevelList/{UserID}")]
        public async Task<IActionResult> GetCompetencyLevel(int UserID, int Mode=1)
        {
            try
            {
                string message = string.Empty;
                AdminCompetency AdminCompetency = new AdminCompetency();
                AdminCompetency.UserID = UserID;
                AdminCompetency.Mode = Mode;

                var result = await _competencyService.GetCompetencyLevel(AdminCompetency);
                
                if (result != null && result.Count() > 0)
                {
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.COMPETENCY_GETLEVEL_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CompetencyLevel>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetCompetencyLevel)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Competency Type
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>CompetencyType</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CompetencyType>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<CompetencyType>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CompetencyType>))]
        [Route("CompetencyType/{UserID}")]
        public async Task<IActionResult> GetCompetencyType(int UserID, int Mode=1)
        {
            try
            {
                string message = string.Empty;
                AdminCompetency AdminCompetency = new AdminCompetency();
                AdminCompetency.UserID = UserID;
                AdminCompetency.Mode = Mode;

                var result = await _competencyService.GetCompetencyType(AdminCompetency);
                
                if (result != null && result.Count() > 0)
                {
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.COMPETENCY_GETTYPE_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CompetencyType>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetCompetencyType)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
    }
}