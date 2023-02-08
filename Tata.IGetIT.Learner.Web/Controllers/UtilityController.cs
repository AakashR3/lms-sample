using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Tata.IGetIT.Learner.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : BaseController
    {
        ILogger logger = LogManager.GetCurrentClassLogger();

        [HttpGet("GetTimeZones")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public IActionResult GetTimeZones()
        {
            var timeZones = new List<Repository.Models.TimeZone>();

            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                timeZones.Add(new Repository.Models.TimeZone() { Id =z.Id , DisplayName = z.DisplayName });

            }

            return Ok( new ReturnResponse<List<Repository.Models.TimeZone>>(){ Data= timeZones , Message="Available timezones" });
        }
    }
}
