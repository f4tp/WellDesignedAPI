using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WellDesignedAPI.Web.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger)
        {
            _logger = logger;
        }


        [HttpGet(Name = "GetMovies")]
        public async Task Get()
        {

        }

    }
}
