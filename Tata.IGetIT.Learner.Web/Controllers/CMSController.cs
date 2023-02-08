using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CMSController : BaseController
    {
        private readonly ICMSService _cmsService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public CMSController(ICMSService cmsService)
        {
            _cmsService = cmsService ?? throw new ArgumentNullException(nameof(ICMSService));
        }
        #region GET

        [HttpGet("CMSFormTypes")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<V2_CMSFormType>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<V2_CMSFormType>))]
        public async Task<IActionResult> GetCMSFormTypes()
        {
            try
            {
                List<string> errorMsg = new();
                var result = await _cmsService.GetCMSFormTypes();
                if (result.Any())
                {
                    logger.LogDebug(LearnerAppConstants.Success);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<V2_CMSFormType>>() { Message = LearnerAppConstants.Success, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<V2_CMSFormType>>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<V2_CMSFormType>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion

        #region POST

        [HttpPost("InsertCMSFormData")]
        [SwaggerResponse(200, "Ok", typeof(ReturnResponse<V2_CMSFormDataInsert>))]
        [SwaggerResponse(400, "Bad Request", typeof(ReturnResponse<V2_CMSFormDataInsert>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ReturnResponse<V2_CMSFormDataInsert>))]
        public async Task<IActionResult> InsertCMSFormData([FromBody] V2_CMSFormDataInsert v2_CMSFormDataInsert)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _cmsService.InsertCMSFormData(v2_CMSFormDataInsert);
                    if (result)
                    {
                        logger.LogDebug(LearnerAppConstants.Success);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<V2_CMSFormDataInsert>() { Message = LearnerAppConstants.Success });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.DatabaseOperationFailed);
                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<V2_CMSFormDataInsert>() { Message = LearnerAppConstants.DatabaseOperationFailed });
                    }
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.Invalid_Inputs);
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<V2_CMSFormDataInsert>() { Message = LearnerAppConstants.Invalid_Inputs });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<V2_CMSFormDataInsert>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion
    }
}
