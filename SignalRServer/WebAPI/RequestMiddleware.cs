
using System.Linq;

namespace WebAPI
{
    public class RequestMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var request = context.Request;
            using var stream = new StreamReader(request.Body);
            var body = await stream.ReadToEndAsync();

            await Console.Out.WriteLineAsync("body: ==> " + body);

            var headerString = string.Join(';', context.Request.Headers.Select(x => $"{x.Key}:{x.Value}"));
            await Console.Out.WriteLineAsync("header: " + headerString);


            await next(context);
        }
    }
}
