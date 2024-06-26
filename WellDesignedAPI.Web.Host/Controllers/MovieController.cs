﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WellDesignedAPI.Application.ApplicationServices;
using WellDesignedAPI.Application.DTOs.Entity.GetMovie;
using WellDesignedAPI.Common.Helpers;
using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.Common.Models.Response;

#region Presentation Layer
//Web.Host
//  • Host Controllers
//  • Map JSON to DTO
//  • Accepts HTTP Requests
//  • Validates requests
//  • Calls Application Services
//  • Returns HTTP Responses
#endregion

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
        [HttpPost("GetMovies")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetMovies(RecordSearchRequest recordSearchRequestParams)
        {

            //TODO
            //Apply logic to allow searching on all properties of the Movie class

            var potentialBadRequestToReturn = ValidateGetMoviesEfSearchSortFIlterState(recordSearchRequestParams);
            if (potentialBadRequestToReturn != null)
                return potentialBadRequestToReturn;


            var movieSearchResponse = await _movieAppService.RetrieveMoviesPagedResultsSearch(recordSearchRequestParams);

            if (movieSearchResponse != null)
                return Ok(movieSearchResponse);


            return StatusCode(500, new RecordSearchResponse() { ErrorMessage = "There was an error with the search. Please try again" });

        }

        private IActionResult? ValidateGetMoviesEfSearchSortFIlterState(RecordSearchRequest recordSearchRequestParams)
        {
            if((recordSearchRequestParams.RequiredPageNumber - 1) * recordSearchRequestParams.NumberOfResultsPerPage < 0)
                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Please check the required page no and the number of results requested per page" });

            if (!string.IsNullOrWhiteSpace(recordSearchRequestParams.SortBy) && !recordSearchRequestParams.SortAscending.HasValue)
                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with sort state (SortAscending needs setting)" });

            if (recordSearchRequestParams.SortAscending.HasValue && string.IsNullOrWhiteSpace(recordSearchRequestParams.SortBy))
                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with sort state (The sort by property needs providing)" });

            if (!string.IsNullOrWhiteSpace(recordSearchRequestParams.SortBy) && !EntityAndSqlPropertyHelper.IsPropertyNameValid<MovieDto>(recordSearchRequestParams.SortBy))
                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {recordSearchRequestParams.SortBy} on sort" });

            if (recordSearchRequestParams.Filters != null)
            {
                foreach (var filter in recordSearchRequestParams.Filters)
                {
                    //Catering for the one filterable prop on Genre as well as all props on Movie
                    if (!string.Equals(filter.PropertyName, nameof(GenreDto.GenreName)) && !EntityAndSqlPropertyHelper.IsPropertyNameValid<MovieDto>(filter.PropertyName))
                        return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });

                    if (EntityAndSqlPropertyHelper.PropertyIsDateTimeType<MovieDto>(filter.PropertyName))
                    {
                        if (!string.IsNullOrWhiteSpace(filter.ValueFromOrEqualTo))
                        {
                            if (DateTimeHelper.IsValidDateTimeString(filter.ValueFromOrEqualTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });

                            if (DateTimeHelper.IsValidDateTimeString(filter.ValueFromOrEqualTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });
                        }

                        if (!string.IsNullOrWhiteSpace(filter.ValueTo))
                        {
                            if (DateTimeHelper.IsValidDateTimeString(filter.ValueTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });
                        }
                    }

                    if (EntityAndSqlPropertyHelper.PropertyIsDecimalType<MovieDto>(filter.PropertyName))
                    {
                        if (!string.IsNullOrWhiteSpace(filter.ValueFromOrEqualTo))
                        {
                            if (NumericDataTypeHelper.StringCanBeParsedToDecimal(filter.ValueFromOrEqualTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });

                            if (NumericDataTypeHelper.StringCanBeParsedToDecimal(filter.ValueFromOrEqualTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });
                        }

                        if (!string.IsNullOrWhiteSpace(filter.ValueTo))
                        {
                            if (NumericDataTypeHelper.StringCanBeParsedToDecimal(filter.ValueTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });

                            if (NumericDataTypeHelper.StringCanBeParsedToDecimal(filter.ValueTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });
                        }
                    }

                    if (EntityAndSqlPropertyHelper.PropertyIsIntegerType<MovieDto>(filter.PropertyName))
                    {
                        if (!string.IsNullOrWhiteSpace(filter.ValueFromOrEqualTo))
                        {
                            if (NumericDataTypeHelper.StringCanBeParsedToInteger(filter.ValueFromOrEqualTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });

                            if (NumericDataTypeHelper.StringCanBeParsedToInteger(filter.ValueFromOrEqualTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });
                        }

                        if (!string.IsNullOrWhiteSpace(filter.ValueTo))
                        {
                            if (NumericDataTypeHelper.StringCanBeParsedToInteger(filter.ValueTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });

                            if (NumericDataTypeHelper.StringCanBeParsedToInteger(filter.ValueTo))
                                return BadRequest(new RecordSearchResponse() { ErrorMessage = $@"Issue with property name {filter.PropertyName} on filter" });
                        }
                    }
                }

            }

            return null;
        }

    }

}
