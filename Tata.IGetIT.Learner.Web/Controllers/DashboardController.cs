using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Tata.IGetIT.Learner.Repository.Models;
using Subscription = Tata.IGetIT.Learner.Repository.Models.Subscription;

namespace Tata.IGetIT.Learner.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        private readonly IDashboardService _dashboardService;
        ILogger logger = LogManager.GetCurrentClassLogger();

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService ?? throw new ArgumentNullException(nameof(dashboardService));

        }


        /// <summary>
        /// Get Scorecard for the UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>Dashboard</returns>
        //[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(Dashboard))]
        [SwaggerResponse(400, "Bad Request", typeof(Dashboard))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Dashboard))]
        [Route("Scorecard/{UserID}")]
        public async Task<IActionResult> GetScorecard(int UserID)
        {
            try
            {

                ScoreCard scoreCard = new();
                scoreCard.UserID = UserID;
                var result = await _dashboardService.GetScoreCard(scoreCard);

                //return StatusCode((int)HttpStatusCode.OK, result);
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<Dashboard>()
                {
                    Message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetScorecard)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Trending Subscription List
        /// </summary>
        /// <param name="CurrencyCode"></param>
        /// <returns>SubscriptionList</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<SubscriptionList>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<SubscriptionList>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<SubscriptionList>))]
        [Route("TrendingSubscription/{CurrencyCode}")]
        public async Task<IActionResult> GetTrendingSubscription(string CurrencyCode)
        {
            try
            {
                string message = string.Empty;
                Subscription subscription = new Subscription();
                subscription.CurrencyCode = CurrencyCode;
                var result = await _dashboardService.GetTrendingSubscription(subscription);
                //return StatusCode((int)HttpStatusCode.OK, result);
                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_TRENDINGSUBSCRIPTION_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<SubscriptionList>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetTrendingSubscription)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Catalog List
        /// </summary>
        /// <returns>CatalogList</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CatalogListParent>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<CatalogList>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CatalogList>))]
        [Route("CatalogList")]
        public async Task<IActionResult> GetCatalog()
        {
            try
            {
                var result = await _dashboardService.GetCatalog();
                //return StatusCode((int)HttpStatusCode.OK, result);
                string message = string.Empty;
                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_CATALOG_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CatalogList>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetCatalog)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get New Course List
        /// </summary>
        /// <returns>CourseList</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CourseList>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<CourseList>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CourseList>))]
        [Route("NewCoursesList")]
        public async Task<IActionResult> GetNewCoursesList()
        {
            try
            {
                var result = await _dashboardService.GetNewCoursesList();
                //return StatusCode((int)HttpStatusCode.OK, result);
                string message = string.Empty;
                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_NEWCOURSE_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CourseList>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetNewCoursesList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get In-progress Course List for the UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>CourseListInProgress</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CourseListInProgress>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<CourseListInProgress>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CourseListInProgress>))]
        [Route("CourseListInProgress/{UserID}")]
        public async Task<IActionResult> GetInProgressCourseList(int UserID)
        {
            try
            {

                ScoreCard scoreCard = new ScoreCard();
                scoreCard.UserID = UserID;
                var result = await _dashboardService.GetInProgressCourseList(scoreCard);
                //return StatusCode((int)HttpStatusCode.OK, result);
                string message = string.Empty;
                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_INPROGRESSCOURSE_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CourseListInProgress>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetInProgressCourseList)), ex);
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
        public async Task<IActionResult> GetLearningPath(int UserID, int PageNumber = 1, int PageSize = 10)
        {
            try
            {
                LearningPath learningPath = new LearningPath();
                learningPath.UserID = UserID;
                var result = await _dashboardService.GetLearningPath(learningPath);

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
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                }
                else
                    message = LearnerAppConstants.DASHBOARD_LEARNINGPATH_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<MyLearningPathGridDataParent>()
                {
                    Message = message,
                    Data = MyLearningPathGridDataParent
                });

            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetLearningPath)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = ex.ToString() });
            }
        }

        /// <summary>
        /// Get Recommended Course List for the UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>CourseList</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CourseList>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<CourseList>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CourseList>))]
        [Route("RecommendedCourseList/{UserID}")]
        public async Task<IActionResult> GetRecommendedCourseList(int UserID)
        {
            try
            {
                LearningPath learningPath = new LearningPath();
                learningPath.UserID = UserID;
                var result = await _dashboardService.GetRecommendedCourseList(learningPath);
                //return StatusCode((int)HttpStatusCode.OK, result);
                string message = string.Empty;
                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_RECOMMENDEDCOURSE_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CourseList>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetRecommendedCourseList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Peers Course List for the UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>CourseList</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<CourseList>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<CourseList>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<CourseList>))]
        [Route("PeersCourseList/{UserID}")]
        public async Task<IActionResult> GetPeersCourseList(int UserID)
        {
            try
            {
                LearningPath learningPath = new LearningPath();
                learningPath.UserID = UserID;
                var result = await _dashboardService.GetPeersCourseList(learningPath);
                //return StatusCode((int)HttpStatusCode.OK, result);
                string message = string.Empty;
                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_PEERSCOURSE_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<CourseList>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetPeersCourseList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Profile Data for the UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>ProfileData</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<ProfileData>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<ProfileData>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<ProfileData>))]
        [Route("ProfileData/{UserID}")]
        public async Task<IActionResult> GetProfile(int UserID)
        {
            try
            {
                ScoreCard scoreCard = new ScoreCard();
                scoreCard.UserID = UserID;
                var result = await _dashboardService.GetProfile(scoreCard);
                //return StatusCode((int)HttpStatusCode.OK, result);
                string message = string.Empty;
                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_PROFILE_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<ProfileData>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetProfile)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Transcript for the UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>TranscriptList</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<TranscriptList>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<TranscriptList>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<TranscriptList>))]
        [Route("TranscriptList/{UserID}")]
        public async Task<IActionResult> GetTranscriptList(int UserID)
        {
            try
            {
                ScoreCard scoreCard = new ScoreCard();
                scoreCard.UserID = UserID;
                var result = await _dashboardService.GetTranscriptList(scoreCard);
                //return StatusCode((int)HttpStatusCode.OK, result);
                string message = string.Empty;
                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_TRANSCRIPT_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<TranscriptList>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetTranscriptList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Upcoming Events List for the UserID
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="EventPeriod"></param>
        /// <returns>UpcomingEventsList</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<UpcomingEventsList>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<UpcomingEventsList>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<UpcomingEventsList>))]
        [Route("UpcomingEventsList/{UserID}/{EventPeriod}")]
        public async Task<IActionResult> GetUpcomingEventsList(int UserID, string EventPeriod)
        {
            try
            {
                string message = string.Empty;
                UpcomingEvents upcomingEvents = new UpcomingEvents();
                upcomingEvents.UserID = UserID;
                upcomingEvents.EventPeriod = EventPeriod;
                var result = await _dashboardService.GetUpcomingEventsList(upcomingEvents);
                //return StatusCode((int)HttpStatusCode.OK, result);

                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_UPCOMINGEVENTS_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<UpcomingEventsList>>()
                {
                    Message = message,
                    Data = result
                });

            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetUpcomingEventsList)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// Get leading users based on points.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetLeaderBoard/{userId}")]
        [SwaggerResponse(200, "Ok", typeof(Response))]
        [SwaggerResponse(400, "Bad Request", typeof(Response))]
        [SwaggerResponse(500, "Internal Server Error", typeof(Response))]
        public async Task<IActionResult> GetLeaderBoardAsync(int userId)
        {

            var leadingUsers = await _dashboardService.GetLeadingUsersAsync(userId, 15, 365);
            // if (leadingUsers.Any())
            // {
            //     return Ok(leadingUsers);
            // }

            string message = string.Empty;
            if (leadingUsers.Any())
                message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
            else
                message = LearnerAppConstants.DASHBOARD_LEADERBOARD_FAILUREMESSAGE;
            return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<LeadingUserDetail>>()
            {
                Message = message,
                Data = leadingUsers
            });
        }

        //[Authorize]
        [HttpGet("PopularRoles/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<PopularRolesList>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<PopularRolesList>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<PopularRolesList>))]
        public async Task<IActionResult> GetPopularRoles(int UserID)
        {
            try
            {
                ScoreCard scoreCard = new ScoreCard();
                scoreCard.UserID = UserID;
                var result = await _dashboardService.GetPopularRoles(scoreCard);

                //return StatusCode((int)HttpStatusCode.OK, result);
                string message = string.Empty;
                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_POPULARROLES_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<PopularRolesList>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetPopularRoles)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        /// <summary>
        /// To show popular roles graph
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("PopularRolesGraph/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<PopularRoleGraph>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<PopularRoleGraph>))]
        public async Task<IActionResult> PopularRolesGraph(int UserID)
        {
            try
            {
                ScoreCard scoreCard = new()
                {
                    UserID = UserID
                };
                var result = await _dashboardService.GetPopularRolesGraph(scoreCard);

                string message = string.Empty;
                if (result != null && result.Any())
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_FAILUREMESSAGE;

                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<PopularRoleGraph>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogDetails(new LogDetails(LogType.Error)
                {
                    Message = LearnerAppConstants.EXCEPTION_MESSAGE,
                    AppException = ex,
                    AppUrl = Request.GetDisplayUrl()
                });
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ReturnResponse<IEnumerable<PopularRoleGraph>>() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
        //[Authorize]
        [HttpGet("TimeSpent/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(TimeSpentMetrics))]
        [SwaggerResponse(400, "Bad Request", typeof(TimeSpentMetrics))]
        [SwaggerResponse(500, "Internal Server Error", typeof(TimeSpentMetrics))]
        public async Task<IActionResult> GetTimeSpent(int UserID)
        {
            try
            {
                ScoreCard scoreCard = new ScoreCard();
                scoreCard.UserID = UserID;
                var result = await _dashboardService.GetTimeSpent(scoreCard);

                string message = string.Empty;
                if (result != null)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<TimeSpentMetrics>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetTimeSpent)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        // //[Authorize]
        [HttpGet("TimeSpentGraph/{UserID}")]
        [SwaggerResponse(200, "Ok", typeof(IEnumerable<TimeSpentGraph>))]
        [SwaggerResponse(400, "Bad Request", typeof(IEnumerable<TimeSpentGraph>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(IEnumerable<TimeSpentGraph>))]
        public async Task<IActionResult> GetTimeSpentGraph(int UserID)
        {
            try
            {
                ScoreCard scoreCard = new ScoreCard();
                scoreCard.UserID = UserID;
                var result = await _dashboardService.GetTimeSpentGraph(scoreCard);

                //return StatusCode((int)HttpStatusCode.OK, result);
                string message = string.Empty;
                if (result != null && result.Count() > 0)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<IEnumerable<TimeSpentGraph>>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetTimeSpentGraph)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }

        /// <summary>
        /// Get Hero Section details for the user
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns>HeroSectionDetails</returns>
        ///[Authorize]
        [HttpGet]
        [SwaggerResponse(200, "Ok", typeof(HeroSectionDetails))]
        [SwaggerResponse(400, "Bad Request", typeof(HeroSectionDetails))]
        [SwaggerResponse(500, "Internal Server Error", typeof(HeroSectionDetails))]
        [Route("HeroSectionDetails/{UserID}")]
        public async Task<IActionResult> GetDashboardHeroSectionDetails(int UserID)
        {
            try
            {

                ScoreCard scoreCard = new ScoreCard();
                scoreCard.UserID = UserID;
                var result = await _dashboardService.GetDashboardHeroSectionDetails(scoreCard);

                string message = string.Empty;
                if (result != null)
                    message = LearnerAppConstants.DASHBOARD_SUCCESSMESSAGE;
                else
                    message = LearnerAppConstants.DASHBOARD_HEROSECTION_FAILUREMESSAGE;
                return StatusCode((int)HttpStatusCode.OK, new ReturnResponse<HeroSectionDetails>()
                {
                    Message = message,
                    Data = result
                });
            }
            catch (Exception ex)
            {
                logger.LogError(string.Format(LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE, nameof(GetDashboardHeroSectionDetails)), ex);
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new Response() { Message = LearnerAppConstants.GENERIC_EXCEPTION_MESSAGE });
            }
        }
    }
}