using Microsoft.AspNetCore.Mvc;
using WellDesignedAPI.Application.ApplicationServices;
using WellDesignedAPI.Common.Models.Request;

namespace WellDesignedAPI.Web.Host.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieAppService _movieAppService;


        public MovieController(ILogger<MovieController> logger, IMovieAppService movieAppService)
        {
            _logger = logger;
            _movieAppService = movieAppService;
        }
      
        //NOTE - there is so much more that I had planned to do, please see the README file for examples

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordSearchRequestParams"></param>
        /// <returns></returns>
        /// <remarks>Is a POST method even though it is a stereotypical GET because query params for searching too complex for a query string</remarks>
        [HttpPost("SearchMoviesEF")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetMoviesEf(RecordSearchRequest recordSearchRequestParams)
        {

            //TODO
            // Validate filter propoerties:
            //// Any DateFrom and DateTo are in the correct format - coming through as a strign representation for better generic application
            ////As above for integer
            ////As above for decimal
            ////As above for double
            ////Validate all property names passed in through magic strings exist on the Movie entity
            /////As much of the above can go in the Movie class

            var movieSearchResponse = await _movieAppService.RetrieveMoviesPagedResultsSearch(recordSearchRequestParams);

            if (movieSearchResponse == null)
                return StatusCode(500, "There was an error with the search. Please try again");

            return Ok(movieSearchResponse);
        }

    }

}
