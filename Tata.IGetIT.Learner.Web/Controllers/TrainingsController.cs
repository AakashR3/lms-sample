using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Tata.IGetIT.Learner.Repository.Interface;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SecuredApi]
    public class TrainingsController : BaseController
    {
        private readonly ITrainingsService _trainingsService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public TrainingsController(ITrainingsService trainingsService)
        {
            _trainingsService = trainingsService ?? throw new ArgumentNullException(nameof(ITrainingsService));
        }


        /// <summary>
        /// Get Platforms
        /// </summary>
        /// <returns>Get Platforms</returns>

        [HttpGet("GetPlatforms")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetPlatforms()
        {
            try
            {           
                var result = await _trainingsService.GetPlatforms();
                if (result != null)
                {
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Platform>>() { Message = LearnerAppConstants.SUCCESSMESSAGE, Data = result });
                }

                return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<string>() { Message = LearnerAppConstants.PLATFORM_DETAILS_NOT_FOUND });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetPlatforms)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Cart>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get trainings
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>Returns paginated results for trainings</returns>
        [HttpGet("all/{pageNumber}/{pageSize}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<TrainingGridDataParent>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<TrainingGridDataParent>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<TrainingGridDataParent>))]
        public async Task<IActionResult> GetTrainingList(int pageNumber = 1, int pageSize = 5, string filter = "")
        {
            try
            {

                if (pageNumber > 0 && pageSize > 0)
                {
                    var userId = base.GetUserId();
                    //return Ok(await _trainingsService.GetTrainings(userId, pageNumber, pageSize));

                    var result = await _trainingsService.GetTrainings(userId, pageNumber, pageSize, filter);

                    if (result.Any())
                    {
                        string message = string.Empty;
                        int totalRecords = result.Count();
                        var validFilter = new PaginationFilter(pageNumber, pageSize, totalRecords);

                        TrainingGridDataParent trainingGridDataParent = new()
                        {
                            PageNumber = pageNumber,
                            PageSize = pageSize,
                            TotalItems = totalRecords,
                            TotalPages = validFilter.TotalPages
                        };

                        if (result != null && result.Count() > 0)
                        {
                            result = result
                                    .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                    .Take(validFilter.PageSize).ToList();
                            trainingGridDataParent.TrainingData = result;
                            message = LearnerAppConstants.SUCCESSMESSAGE;
                        }
                        else
                            message = LearnerAppConstants.LEARNING_GETHISTORY_FAILUREMESSAGE;

                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TrainingGridDataParent>()
                        {
                            Message = message,
                            Data = trainingGridDataParent
                        });
                    }
                    else
                    {
                        logger.LogDebug(LearnerAppConstants.DASHBOARD_FAILUREMESSAGE);
                        return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TrainingGridDataParent>() { Message = LearnerAppConstants.DASHBOARD_FAILUREMESSAGE, Data = { } });
                    }
                }
                //return NotFound();
                return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<string>() { Message = LearnerAppConstants.TRAININGS_NOT_FOUND });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetTrainingList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Cart>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        [HttpGet("GetTrainings/{TrainingID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Training>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<Training>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Training>))]
        public async Task<IActionResult> GetTrainings(int TrainingID)
        {
            try
            {
                if (TrainingID < 1)
                {
                    logger.LogError(LearnerAppConstants.MODEL_VALIDATION_FAILED, new ApiException("GetTraining model validation failed"));
                    return await PopulateModelErrorsAsync();
                }

                var UserID = base.GetUserId();
                var result = await _trainingsService.GetTrainings(TrainingID, UserID);
                if (result != null)
                {
                    if (result.Attendee != null)
                    {
                        var myAttendees = result.Attendee;
                        result.Attendees = myAttendees.Split(',').ToList();
                    }
                    result.Attendee = null;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Training>() { Message = "Training details found", Data = result });
                }

                return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<Training>() { Message = $"Training details not found for training id {TrainingID}" });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetTrainings)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Training>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Create training
        /// </summary>
        /// <param name="request">CreateTrainingRequest</param>
        /// <returns>Creation status</returns>
        [HttpPost("create")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> CreateTraining([FromBody] CreateTrainingRequest request)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    logger.LogError(LearnerAppConstants.MODEL_VALIDATION_FAILED, new ApiException(LearnerAppConstants.UNABLE_TO_CREATE_TRAINING));
                    return await PopulateModelErrorsAsync();
                }
                var userId = base.GetUserId();
                var accountId = base.GetAccountId();
                var data = await _trainingsService.CreateTrainings(request, userId, accountId);
                if (data != 0)
                {
                    logger.Info($"Training created successfully user={userId}");
                    return Ok(new ReturnResponse<int>() { Message = LearnerAppConstants.TRAINING_CREATED_SUCCESSFULLY, Data = data });
                }

                logger.Error($"Training create failed user={userId}");
                //return NotFound("Training creation failed.");
                return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<CreateTrainingRequest>() { Message = LearnerAppConstants.UNABLE_TO_CREATE_TRAINING });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(CreateTraining)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<CreateTrainingRequest>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Update training
        /// </summary>
        /// <param name="request">UpdateTrainingRequest</param>
        /// <returns>Update status</returns>
        [HttpPost("update")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> UpdateTraining([FromBody] UpdateTrainingRequest request)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    logger.LogError(LearnerAppConstants.MODEL_VALIDATION_FAILED, new ApiException(LearnerAppConstants.UNABLE_TO_UPDATE_TRAINING));
                    return await PopulateModelErrorsAsync();
                }

                var userId = base.GetUserId();
                var accountId = base.GetAccountId();

                if (await _trainingsService.UpdateTrainings(request, userId, accountId))
                {
                    logger.Info($"Training updated successfully user={userId} id={request.TrainingID}");
                    return Ok(new ReturnResponse<string>() { Message = LearnerAppConstants.TRAINING_UPDATED_SUCCESSFULLY, Data = null });
                }

                logger.Error($"Training update failed user={userId} id={request.TrainingID}");
                //return NotFound("Training update failed.");
                return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<UpdateTrainingRequest>() { Message = LearnerAppConstants.UNABLE_TO_UPDATE_TRAINING });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(UpdateTraining)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<UpdateTrainingRequest>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// Delete Training
        /// </summary>
        /// <param name="TrainingID">Training id</param>
        /// <returns>Deletion status</returns>
        [HttpPost("delete/{TrainingID}")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteTraining(int TrainingID)
        {
            try
            {

                var userId = base.GetUserId();
                if (await _trainingsService.DeleteTrainings(TrainingID, userId))
                {
                    logger.Info($"Training deleted successfully user={userId} id={TrainingID}");
                    return Ok(new ReturnResponse<string>() { Message = LearnerAppConstants.TRAINING_DELETED_SUCCESSFULLY, Data = null });
                }

                logger.Error($"Training delete failed user={userId} id={TrainingID}");
                //return NotFound("Training delete failed.");
                return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<string>() { Message = LearnerAppConstants.UNABLE_TO_DELETE_TRAINING });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(DeleteTraining)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Cart>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }

        }

        /// <summary>
        /// Get Training Session
        /// </summary>
        /// <param>TrainingID</param>

        //[Authorize]
        [HttpGet("TrainingSession/{TrainingID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Session>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Session>))]
        public async Task<IActionResult> TrainingSession(int TrainingID)
        {
            try
            {

                var UserID = base.GetUserId();


                var result = await _trainingsService.TrainingSession(TrainingID, UserID);
                if (result.Count() > 0)
                {
                    logger.LogDebug(LearnerAppConstants.SUCCESSMESSAGE);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Session>>() { Message = LearnerAppConstants.SUCCESSMESSAGE, Data = result });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.DASHBOARD_FAILUREMESSAGE);
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Session>() { Message = LearnerAppConstants.DASHBOARD_FAILUREMESSAGE, Data = { } });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(TrainingSession)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Session>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get microsoft teams meeting Transcript
        /// </summary>
        /// <param name="meetingId"></param>
        /// <returns></returns>
        [HttpGet("sessions/transcripts/{meetingId}")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetSessionTranscriptAsync(string meetingId)
        {
            await _trainingsService.GetSessionTranscriptAsync(meetingId);

            return Ok();
        }

        /// <summary>
        /// Get microsoft teams meeting recording
        /// </summary>
        /// <returns></returns>
        [HttpGet("sessions/recording/{meetingId}")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetSessionRecordingAsync(string meetingId)
        {
            await _trainingsService.GetSessionRecordingAsync(meetingId);
            return Ok();
        }

        /// <summary>
        /// Get microsoft teams meeting attendance reports
        /// </summary>
        /// <param name="meetingId">Meeting Id</param>
        /// <returns>AttendanceReports</returns>
        [HttpGet("sessions/AttendanceReport/{meetingId}")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetMeetingAttendanceAsync(string meetingId)
        {
            try
            {

                if (string.IsNullOrEmpty(meetingId))
                {
                    return BadRequest();
                }
                var reports = await _trainingsService.GetSessionAteendanceReportsAsync(meetingId);

                if (reports != null && reports.Count > 0)
                {

                    return Ok(new ReturnResponse<List<AttendanceReport>>() { Message = "Meeting Ateendance records", Data = reports });
                }
                return NotFound(new ReturnResponse<string>() { Message = "No meeting ateendance records available!", Data = null });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetMeetingAttendanceAsync)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Cart>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }

        }

        /// <summary>
        /// Create microsoft teams meeting
        /// </summary>
        /// <param name="request">CreateMeetingRequest</param>
        /// <returns></returns>
        [HttpPost("sessions/create")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> CreateMeetingAsync(CreateMeetingRequest request)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    logger.LogError(LearnerAppConstants.MODEL_VALIDATION_FAILED, new ApiException("CreateMeetingRequest model validation failed"));
                    return await PopulateModelErrorsAsync();
                }
                var userId = base.GetUserId();
                List<string> errorMessages = new List<string>();
                await _trainingsService.CreateSessionAsync(request, userId, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<VerifySessionData>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<VerifySessionData>() { Message = LearnerAppConstants.MEETING_CREATED_SUCCESSFULLY, Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(CreateMeetingAsync)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<CreateMeetingRequest>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Update microsoft teams meeting
        /// </summary>
        /// <param name="request">UpdateMeetingRequest</param>
        /// <returns></returns>
        [HttpPost("sessions/update")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> UpdateMeetingAsync(UpdateMeetingRequest request)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    logger.LogError(LearnerAppConstants.MODEL_VALIDATION_FAILED, new ApiException("UpdateMeetingRequest model validation failed"));
                    return await PopulateModelErrorsAsync();
                }

                var userId = base.GetUserId();
                List<string> errorMessages = new List<string>();

                await _trainingsService.UpdateSessionAsync(request, userId, errorMessages);
                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<VerifySessionData>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<VerifySessionData>() { Message = LearnerAppConstants.MEETING_UPDATED_SUCCESSFULLY, Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(UpdateMeetingAsync)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<UpdateMeetingRequest>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Delete microsoft teams meeting
        /// </summary>
        /// <param name="request">DeleteMeetingRequest</param>
        /// <returns></returns>
        [HttpPost("sessions/delete")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteMeetingAsync(DeleteMeetingRequest request)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    logger.LogError(LearnerAppConstants.MODEL_VALIDATION_FAILED, new ApiException("DeleteMeetingRequest model validation failed"));
                    return await PopulateModelErrorsAsync();
                }

                var userId = base.GetUserId();
                List<string> errorMessages = new List<string>();

                await _trainingsService.DeleteSessionAsync(request, userId, errorMessages);

                if (errorMessages.Any())
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<VerifySessionData>() { Message = errorMessages.FirstOrDefault() });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<VerifySessionData>() { Message = LearnerAppConstants.MEETING_DELETED_SUCCESSFULLY, Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(DeleteMeetingAsync)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Cart>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// GetSessions teams meeting
        /// </summary>
        /// <param name="SessionID">GetSessions</param>
        /// <returns></returns>
        [HttpGet("GetSessions/{SessionID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Session>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<Session>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Session>))]
        public async Task<IActionResult> GetSessions(int SessionID)
        {
            try
            {

                if (SessionID < 1)
                {
                    logger.LogError(LearnerAppConstants.MODEL_VALIDATION_FAILED, new ApiException("GetSessions model validation failed"));
                    return await PopulateModelErrorsAsync();
                }

                var UserID = base.GetUserId();

                var result = await _trainingsService.GetSession(SessionID, UserID);
                if (result != null)
                {
                    if (result.Attendee != null)
                    {
                        var myAttendees = result.Attendee;
                        result.Attendees = myAttendees.Split(',').ToList();
                    }
                    result.Attendee = null;

                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Session>() { Message = "Sesssion details", Data = result });
                }

                return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<string>() { Message = $"Session details not found for session id {SessionID}" });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetSessions)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Session>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }


        /// <summary>
        /// Get Sessions List for Calendar view
        /// </summary>
        /// <param name="Date">CalendarSessions</param>
        /// <returns></returns>
        [HttpGet("Calendar/{Date}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<Session>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<Session>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<Session>))]
        public async Task<IActionResult> CalendarSessions(string Date)
        {
            try
            {
                if (Date.IsNullOrEmptyOrWhiteSpace())
                {
                    logger.LogError(LearnerAppConstants.MODEL_VALIDATION_FAILED, new ApiException("Get Sessions model validation failed"));
                    return await PopulateModelErrorsAsync();
                }

                var userId = base.GetUserId();
                var result = await _trainingsService.CalendarSessions(Date, userId);
                if (result != null)
                {
                    
                    return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<Session>>() { Message = "Sesssion details found", Data = result });
                }

                return StatusCode((int)HttpStatusCode.BadRequest, new ReturnResponse<string>() { Message = $"Session details not found" });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(CalendarSessions)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, new ReturnResponse<Session>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }





    }
}
