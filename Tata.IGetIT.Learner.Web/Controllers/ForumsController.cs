using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SecuredApi]
    public class ForumsController : BaseController
    {
        private readonly IForumsService _forumsService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public ForumsController(IForumsService forumsService)
        {
            _forumsService = forumsService ?? throw new ArgumentNullException(nameof(IForumsService));
        }

        #region POST
        /// <summary>
        /// To Create Posting
        /// </summary> 
        /// <param name="posting"></param>
        /// <returns></returns>
        [HttpPost("CreatePosting")]
        [SwaggerResponse(200, "Ok", typeof(CreatePosting))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> CreatePosting([FromForm] CreatePosting posting)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    int userId = base.GetUserId();
                    List<string> errorMessage = new();
                    var result = await _forumsService.CreatePosting(posting, userId, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<CreatePosting>() { Message = errMsg });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.POSTING_ADDED_SUCCESSFULLY);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CreatePosting>()
                        {
                            Message = LearnerAppConstants.POSTING_ADDED_SUCCESSFULLY
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = posting
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(CreatePosting)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<CreatePosting>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = { } });
            }
        }

        /// <summary>
        /// To Update Posting
        /// </summary>    
        /// <param name="posting"></param>
        /// <returns></returns>
        [HttpPost("UpdatePosting")]
        [SwaggerResponse(200, "Ok", typeof(UpdatePosting))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> UpdatePosting([FromForm] UpdatePosting posting)
        {
            try
            {
                if (ModelState.IsValid)
                {                   
                    int userId = base.GetUserId();
                    List<string> errorMessage = new();
                    var result = await _forumsService.UpdatePosting(posting, userId, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<UpdatePosting>() { Message = errMsg });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.POSTING_UPDATED_SUCCESSFULLY);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UpdatePosting>()
                        {
                            Message = LearnerAppConstants.POSTING_UPDATED_SUCCESSFULLY
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = posting
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(UpdatePosting)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<UpdatePosting>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = { } });
            }
        }


        /// <summary>
        /// To Delete Posting
        /// </summary> 
        /// <param name="posting"></param>
        /// <returns></returns>
        [HttpPost("DeletePosting")]
        [SwaggerResponse(200, "Ok", typeof(DeletePosting))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> DeletePosting([FromBody] DeletePosting posting)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    int userId = base.GetUserId();
                    List<string> errorMessage = new();
                    var result = await _forumsService.DeletePosting(posting, userId, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<DeletePosting>() { Message = errMsg });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.POSTING_DELETED_SUCCESSFULLY);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<DeletePosting>()
                        {
                            Message = LearnerAppConstants.POSTING_DELETED_SUCCESSFULLY
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = posting
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(DeletePosting)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<DeletePosting>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = { } });
            }
        }

        /// <summary>
        /// To Create Reply
        /// </summary>    
        /// <param name="createReply"></param>
        /// <returns></returns>
        [HttpPost("CreateReply")]
        [SwaggerResponse(200, "Ok", typeof(CreateReply))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> CreateReply([FromForm] CreateReply createReply)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = base.GetUserId();
                    List<string> errorMessage = new();
                    var result = await _forumsService.CreateReply(createReply, userId, errorMessage);


                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<CreateReply>() { Message = errMsg });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.REPLY_ADDED_SUCCESSFULLY);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<CreateReply>()
                        {
                            Message = LearnerAppConstants.REPLY_ADDED_SUCCESSFULLY
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = createReply
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(CreateReply)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<CreateReply>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = { } });
            }
        }

        /// <summary>
        /// To Create Reply
        /// </summary>    
        /// <param name="updateReply"></param>
        /// <returns></returns>
        [HttpPost("UpdateReply")]
        [SwaggerResponse(200, "Ok", typeof(CreateReply))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> UpdateReply([FromForm] UpdateReply updateReply)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (Request.Form.Files.Count > 0)
                    {
                        var file = Request.Form.Files[0];
                        var folderName = Path.Combine("Resources", "Forum", "Reply");
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var newFileName = UtilityHelper.GetUniqueFileName(fileName);
                            var fullPath = Path.Combine(pathToSave, newFileName);
                            var dbPath = Path.Combine(folderName, newFileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            updateReply.FilePath = dbPath;
                        }
                    }
                    int userId = base.GetUserId();
                    List<string> errorMessage = new();
                    var result = await _forumsService.UpdateReply(updateReply, userId, errorMessage);


                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<UpdateReply>() { Message = errMsg });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.REPLY_UPDATED_SUCCESSFULLY);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UpdateReply>()
                        {
                            Message = LearnerAppConstants.REPLY_UPDATED_SUCCESSFULLY
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = updateReply
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(UpdateReply)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<UpdateReply>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = { } });
            }
        }


        /// <summary>
        /// To Delete Reply
        /// </summary> 
        /// <param name="reply"></param>
        /// <returns></returns>
        [HttpPost("DeleteReply")]
        [SwaggerResponse(200, "Ok", typeof(DeleteReply))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> DeleteReply([FromBody] DeleteReply reply)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = base.GetUserId();
                    List<string> errorMessage = new();
                    var result = await _forumsService.DeleteReply(reply, userId, errorMessage);

                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<DeleteReply>() { Message = errMsg });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.REPLY_DELETED_SUCCESSFULLY);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<DeleteReply>()
                        {
                            Message = LearnerAppConstants.REPLY_DELETED_SUCCESSFULLY
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = reply
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(DeleteReply)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<DeleteReply>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = { } });
            }
        }

        /// <summary>
        /// Raise concern against reply
        /// </summary> 
        /// <param name="reply"></param>
        /// <returns></returns>
        [HttpPost("HideReply")]
        [SwaggerResponse(200, "Ok", typeof(HideReply))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> HideReply([FromBody] HideReply reply)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    reply.Status = 2;
                    int userId = base.GetUserId();
                    List<string> errorMessage = new();
                    var result = await _forumsService.HideReply(reply, userId, errorMessage);


                    if (errorMessage.Any())
                    {
                        var errMsg = errorMessage.FirstOrDefault();
                        logger.LogDebug(errMsg);

                        return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<HideReply>() { Message = errMsg });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.REPLY_REPORTED_SUCCESSFULLY);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<HideReply>()
                        {
                            Message = LearnerAppConstants.REPLY_REPORTED_SUCCESSFULLY
                        });
                    }
                }
                else
                {
                    logger.LogDetails(new LogDetails(LogType.Error)
                    {
                        Message = LearnerAppConstants.MODEL_VALIDATION_FAILED,
                        AppUrl = Request.GetDisplayUrl(),
                        RequestParam = reply
                    });

                    return await PopulateModelErrorsAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(HideReply)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<HideReply>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = { } });
            }
        }

        [HttpPost("Download")]
        [SwaggerResponse(200, "Ok", typeof(DownloadReponse))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> Download([FromBody] DownloadFile downloadFile)
        {
            try
            {
                List<string> errorMessage = new();
                var result = await _forumsService.DownloadFile(downloadFile, errorMessage);

                if (errorMessage.Any())
                {
                    var errMsg = errorMessage.FirstOrDefault();
                    logger.LogDebug(errMsg);

                    return StatusCode((int)HttpStatusCode.NotFound, new ReturnResponse<Response>() { Message = errMsg });

                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.RESOURCE_FOUND);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<DownloadReponse>() { Message = LearnerAppConstants.RESOURCE_FOUND, Data = result });
                }
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = string.Format(LearnerAppConstants.EXCEPTION_MESSAGE, nameof(Download)),
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });

                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Response>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, Data = { } });
            }
        }
        #endregion


        #region GET

        /// <summary>
        /// To get Transcript Assessment History
        /// </summary>
        /// <param name="CourseID"></param>
        /// <param name="SearchText"></param>
        /// <param name="SearchType">ALL or MINE</param>
        /// <returns></returns>
        [HttpGet("CourseForum/{CourseID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Forums>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Forums>))]
        public async Task<IActionResult> CourseForum(int CourseID, string SearchText = "", string SearchType = "ALL")
        {
            try
            {
                int UserID = base.GetUserId();
                List<string> errorMsg = new();
                var result = await _forumsService.CourseForum(UserID, CourseID, SearchText, SearchType, errorMsg);

                if (errorMsg.Any())
                {
                    logger.LogDebug(LearnerAppConstants.NoRecordsFound);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Forums>() { Message = LearnerAppConstants.NoRecordsFound, Data = { } });

                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Forums>()
                    {
                        Message = LearnerAppConstants.Success,
                        Data = result
                    });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<IEnumerable<Forums>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        #endregion

    }
}
