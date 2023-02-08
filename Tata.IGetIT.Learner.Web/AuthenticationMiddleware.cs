using Microsoft.Extensions.Options;
namespace Tata.IGetIT.Learner.Web
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;

        public AuthenticationMiddleware(RequestDelegate next, IUserService userService)
        {
            _next = next;
            _userService = userService ?? throw new ArgumentNullException(nameof(IUserService));
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            context.Items["IsUserAuthenticated"] = false;
            if (!string.IsNullOrWhiteSpace(token))
            {
                var errors = new List<string>();
                Dictionary<string, string> claims = new Dictionary<string, string>();
                var isValid = await userService.TokenValidation(token, errors, claims);
                context.Items["IsUserAuthenticated"] = isValid;
                foreach (var claim in claims)
                {
                    context.Items[claim.Key] = claim.Value;
                }
            }
           
            await _next(context);
        }
    }
}
