using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string message)
        {
            //HttpContext.Request.BodyReader.TryRead(out var buffer);
            //var message = Encoding.UTF8.GetString(buffer.Buffer);
            await Console.Out.WriteLineAsync($"sent messgage: {message}");
            await Task.CompletedTask;
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string message)
        {
            //HttpContext.Request.BodyReader.TryRead(out var buffer);
            //var message = Encoding.UTF8.GetString(buffer.Buffer);
            await Console.Out.WriteLineAsync($"sent messgage: {message}");
            await Task.CompletedTask;
            return Ok();
        }
    }


}
