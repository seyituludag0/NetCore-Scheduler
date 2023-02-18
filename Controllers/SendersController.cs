using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendersController : ControllerBase
    {
        private readonly INewsService _newsService;

        public SendersController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("Send")]
        public void Send()
        {
            _newsService.CreateNewsSummary();
        }
    }
}
