using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Tata.IGetIT.Learner.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SecuredApiAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                return;
            }
            object isUserAuthenticated = false;
            context.HttpContext.Items.TryGetValue("IsUserAuthenticated", out isUserAuthenticated);
            if ((bool)isUserAuthenticated == false)
            {
                var message = JsonSerializer.Serialize(new ReturnResponse<string> { Message = LearnerAppConstants.INVALID_TOKEN, Data = null });
                context.Result = new JsonResult(message) { StatusCode = StatusCodes.Status401Unauthorized };
            }


        }
    }
}
