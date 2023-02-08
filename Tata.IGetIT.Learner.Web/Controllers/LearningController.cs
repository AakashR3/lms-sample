using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningController : BaseController
    {
        private readonly ILearningService _learningService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public LearningController(ILearningService learningService)
        {
            _learningService = learningService ?? throw new ArgumentNullException(nameof(learningService));

        }

        /// <summary>
        /// Get User History Data
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>UserHistoryGridData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<UserHistoryGridData>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<UserHistoryGridData>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<UserHistoryGridData>))]
        [Route("UserHistoryGridData/{UserID}")]
        public async Task<IActionResult> GetUserHistoryGridData(int UserID, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                string message = string.Empty;
                UserHistory userHistory = new UserHistory();
                userHistory.UserID = UserID;
                userHistory.ItemType = 0;

                var result = await _learningService.GetUserHistoryGridData(userHistory);

                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                UserHistoryGridDataParent UserHistoryGridDataParent = new UserHistoryGridDataParent();
                UserHistoryGridDataParent.PageNumber = PageNumber;
                UserHistoryGridDataParent.PageSize = PageSize;
                UserHistoryGridDataParent.TotalItems = totalRecords;
                UserHistoryGridDataParent.TotalPages = validFilter.TotalPages;

                if (result != null && result.Count() > 0)
                {
                    result = result
                            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize).ToList();
                    UserHistoryGridDataParent.UserHistoryGridData = result;
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.LEARNING_GETHISTORY_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<UserHistoryGridDataParent>()
                {
                    Message = message,
                    Data = UserHistoryGridDataParent
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetUserHistoryGridData)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get User's Learning Data
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ItemType"></param>
        /// <param name="Favorites"></param>
        /// <param name="SearchText"></param>
        /// <param name="CategoryID"></param>
        /// <param name="Status"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <returns>UserHistoryGridData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<MyLearningGridData>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<MyLearningGridData>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<MyLearningGridData>))]
        [Route("MyLearningGridData/{UserID}/")]
        public async Task<IActionResult> GetMyLearningGridData(int UserID, int ItemType = 1, int Favorites = 0, string SearchText = "", int CategoryID = 0, int Status = 2, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                MyLearningGridData MyLearningGridData = new MyLearningGridData();
                string message = string.Empty;
                MyLearning myLearning = new MyLearning();
                myLearning.UserID = UserID;
                myLearning.ItemType = ItemType;
                myLearning.SearchText = SearchText;
                myLearning.Favorites = Favorites;
                myLearning.CategoryID = CategoryID;
                myLearning.Status = Status;

                var result = await _learningService.GetMyLearningGridData(myLearning);

                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                MyLearningGridDataParent MyLearningGridDataParent = new MyLearningGridDataParent();
                MyLearningGridDataParent.PageNumber = PageNumber;
                MyLearningGridDataParent.PageSize = PageSize;
                MyLearningGridDataParent.TotalItems = totalRecords;
                MyLearningGridDataParent.TotalPages = validFilter.TotalPages;

                if (result != null && result.Count() > 0)
                {
                    result = result
                            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize).ToList();
                    MyLearningGridDataParent.MyLearningGridData = result;
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = ItemType == 1 ? LearnerAppConstants.LEARNING_GETMYLEARNINGCOURSE_FAILUREMESSAGE : LearnerAppConstants.LEARNING_GETMYLEARNINGASSESSMENT_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<MyLearningGridDataParent>()
                {
                    Message = message,
                    Data = MyLearningGridDataParent
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetMyLearningGridData)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Add Favorite Item
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Type"></param>
        /// <param name="ItemID"></param>
        /// <returns>string</returns>
        ///[Authorize]
        [HttpPut("AddFavoriteItem")]
        [SwaggerResponse(200, "Ok", typeof(string))]
        [SwaggerResponse(400, "Bad Request", typeof(string))]
        [SwaggerResponse(500, "Internal Server Error", typeof(string))]
        //[Route("AddFavoriteItem/{UserID}/")]
        public async Task<IActionResult> AddFavoriteItem(AddRemoveFavorite AddRemoveFavorite)
        {
            try
            {
                string message = string.Empty;
                // AddRemoveFavorite AddRemoveFavorite = new AddRemoveFavorite();
                // AddRemoveFavorite.UserID = UserID;
                // AddRemoveFavorite.Type = ItemType;
                // AddRemoveFavorite.ItemID = ItemID;

                var result = await _learningService.AddFavoriteItem(AddRemoveFavorite);

                if (result != null && result.Split('^')[0] == "1")
                {
                    string errMsg = result.ToString().Split('^')[1];
                    logger.LogDebug(errMsg);

                    return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                    {
                        Message = errMsg
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.LEARNING_ADDFAVORITE_SUCCESSMESSAGE);
                    return StatusCode((int)HttpStatusCode.OK, new Response()
                    {
                        Message = LearnerAppConstants.LEARNING_ADDFAVORITE_SUCCESSMESSAGE
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(AddFavoriteItem)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Add Favorite Item
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="Type"></param>
        /// <param name="ItemID"></param>
        /// <returns>string</returns>
        ///[Authorize]
        [HttpPut("RemoveFavoriteItem")]
        [SwaggerResponse(200, "Ok", typeof(string))]
        [SwaggerResponse(400, "Bad Request", typeof(string))]
        [SwaggerResponse(500, "Internal Server Error", typeof(string))]
        //[Route("AddFavoriteItem/{UserID}/")]
        public async Task<IActionResult> RemoveFavoriteItem(AddRemoveFavorite AddRemoveFavorite)
        {
            try
            {
                string message = string.Empty;
                // AddRemoveFavorite AddRemoveFavorite = new AddRemoveFavorite();
                // AddRemoveFavorite.UserID = UserID;
                // AddRemoveFavorite.Type = ItemType;
                // AddRemoveFavorite.ItemID = ItemID;

                var result = await _learningService.RemoveFavoriteItem(AddRemoveFavorite);

                if (result != null && result.Split('^')[0] == "1")
                {
                    string errMsg = result.ToString().Split('^')[1];
                    logger.LogDebug(errMsg);

                    return StatusCode((int)HttpStatusCode.BadRequest, new Response()
                    {
                        Message = errMsg
                    });
                }
                else
                {
                    logger.LogDebug(LearnerAppConstants.LEARNING_REMOVEFAVORITE_SUCCESSMESSAGE);
                    return StatusCode((int)HttpStatusCode.OK, new Response()
                    {
                        Message = LearnerAppConstants.LEARNING_REMOVEFAVORITE_SUCCESSMESSAGE
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(RemoveFavoriteItem)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Learning Path for the UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>LearningPathDetailedList</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<LearningPathDetailedList>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<LearningPathDetailedList>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<LearningPathDetailedList>))]
        [Route("LearningPath/{UserID}")]
        public async Task<IActionResult> GetMyLearningPath(int UserID, string SearchText = "", int Status = 2, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                MyLearning myLearning = new MyLearning();
                myLearning.UserID = UserID;
                myLearning.SearchText = SearchText;
                myLearning.Status = Status;

                var result = await _learningService.GetMyLearningPath(myLearning);


                int totalRecords = result.Count();
                var validFilter = new PaginationFilter(PageNumber, PageSize, totalRecords);

                MyLearningPathGridDataParent MyLearningPathGridDataParent = new MyLearningPathGridDataParent();
                MyLearningPathGridDataParent.PageNumber = PageNumber;
                MyLearningPathGridDataParent.PageSize = PageSize;
                MyLearningPathGridDataParent.TotalItems = totalRecords;
                MyLearningPathGridDataParent.TotalPages = validFilter.TotalPages;

                string message = string.Empty;
                if (result != null && result.Count() > 0)
                {
                    result = result
                            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                            .Take(validFilter.PageSize).ToList();
                    MyLearningPathGridDataParent.LearningPathDetailedList = result;
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.LEARNING_GETLEARNINGPATH_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<MyLearningPathGridDataParent>()
                {
                    Message = message,
                    Data = MyLearningPathGridDataParent
                });

            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetMyLearningPath)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message =  LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Info for Download Certificate
        /// </summary>
        /// <param name="EventID"></param>
        /// <param name="ItemID"></param>
        /// <param name="UserID"></param>
        /// <param name="IDType"></param>
        /// <param name="Mode"></param>
        /// <returns>DownloadCertificateInfo</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(DownloadCertificateInfo))]
        [SwaggerResponse(400, "Bad Request", typeof(DownloadCertificateInfo))]
        [SwaggerResponse(500, "Internal Server Error", typeof(DownloadCertificateInfo))]
        [Route("CertificateInfo/{EventID}/{ItemID}/{UserID}")]
        public async Task<IActionResult> GetDownloadCertificate(int EventID, int ItemID, int UserID, int IDType = 1, int Mode = 1)
        {
            try
            {
                string message = string.Empty;
                DownloadCertificate downloadCertificate = new DownloadCertificate();
                downloadCertificate.EventID = EventID;
                downloadCertificate.ItemID = ItemID;
                downloadCertificate.UserID = UserID;
                downloadCertificate.IDType = IDType;
                downloadCertificate.Mode = Mode;

                var result = await _learningService.GetDownloadCertificate(downloadCertificate);

                if (result != null)
                    message = LearnerAppConstants.SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.LEARNING_DOWNLOAD_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<DownloadCertificateInfo>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetDownloadCertificate)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

    }
}