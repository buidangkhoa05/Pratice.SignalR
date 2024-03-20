using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHubContext<RealTimeHub> _hubContext;

        public TestController(IHubContext<RealTimeHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DataRequest dataRequest)
        {
            //HttpContext.Request.BodyReader.TryRead(out var buffer);
            //var message = Encoding.UTF8.GetString(buffer.Buffer);
            await Console.Out.WriteLineAsync($"sent messgage: {JsonConvert.SerializeObject(dataRequest)}");
            await Task.CompletedTask;

            await _hubContext.Clients.All.SendAsync("dataUpdated", "test", "test");
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
