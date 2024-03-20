using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Middleware
{
    public class AuthensMidlleware : IMiddleware
    {

        //private readonly IAccountService _accountService;

        //public AuthensMidlleware(IAccountService accountService)
        //{
        //    _accountService = accountService;
        //}

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer", "").Trim();
            //if (!string.IsNullOrEmpty(token))
            //{
            //    var claims = await _accountService.GetAuthenticatedAccount(token);
            //    context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Bearer"));
            //}

            //authentication for signalR Hub
            if (context.Request.Path.StartsWithSegments("/hubs"))
            {
                var accessToken = context.Request.Query["access_token"];

                if (string.IsNullOrEmpty(accessToken))
                {
                    var response = new
                    {
                        StatusCode = HttpStatusCode.Unauthorized,
                        Message = "Unauthorized"
                    };
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                    return;
                }
                else
                {
                    var claims = new List<Claim> { }; // todo: get claims from token
                    var identity = new ClaimsIdentity(claims, "Bearer");
                    context.User = new ClaimsPrincipal(identity); // todo: set user to context
                }
            }
            await next(context);
        }
    }
}
