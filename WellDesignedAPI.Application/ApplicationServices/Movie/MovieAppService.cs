using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WellDesignedAPI.Application.DTOs.Entity.GetMovie;
using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.Common.Models.Response;
using WellDesignedAPI.Domain.DomainServices;
using WellDesignedAPI.DataAccess.DbContexts;

#region Application Layer
//Application
//  •	Host AppServices
//  •	Hosts DTOs
//      • Behaviours
//          • Anaemic Domain Model
//      • Data content
//          • Properties
//          • Validation attributes (for JSON to DTO mapping)
//  • Map DTOs<> DomEnts
//  • Purposes
//      • Calls DomainServices
//      • Validate requests when that validation extends past the responsibility of the Controller
//          • Goes through DomainService > DataAccess layer to retrieve then validation logic exists in app services
//      • Manages database transactions
//  • Never interacts with the database

#endregion

namespace WellDesignedAPI.Application.ApplicationServices
{
    public class MovieAppService : IMovieAppService
    {
        private readonly ILogger<MovieAppService> _logger;
        private readonly IMapper _mapper;
        private readonly IMovieDomainService _movieDomainService;
        private readonly AppDbContext _dbContext;

        public MovieAppService(ILogger<MovieAppService> logger, IMapper mapper, IMovieDomainService movieDomainService, AppDbContext dbContext)
        {
            _logger = logger;
            _mapper = mapper;
            _movieDomainService = movieDomainService;
            _dbContext = dbContext;
        }

        public async Task<RecordSearchResponse> RetrieveMoviesPagedResultsSearch(RecordSearchRequest recordSearchRequestParams)
        {
            //Get both the COUNT and paged number of records in a transaction, so the results aren't skewed (unlikely but this ensures they are relative at the time of retrieving them)
            try
            {
                var executionStrategy = _dbContext.Database.CreateExecutionStrategy();
                RecordSearchResponse result = null;

                await executionStrategy.ExecuteAsync(async () =>
                {
                    using (var transaction = _dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            //build query - minus paging
                            var queryToExecute = _movieDomainService.GetEntityFrameworkQueryForSearchSortFilter(recordSearchRequestParams);
                            //count the total results that would be brought back after search and filter applied
                            var totalMovieCount = await _movieDomainService.GetCountOfFilteredResults(queryToExecute);
                            //then apply paging
                            queryToExecute = _movieDomainService.ApplyPagingToEntityFrameworkQuery(recordSearchRequestParams, queryToExecute);
                            //run the query to return the found movies
                            var foundMovieDomEnts = await _movieDomainService.RetrieveMoviesPagedResultsSearch(queryToExecute);
                            await transaction.CommitAsync();
                            result = new RecordSearchResponse() { Records = _mapper.Map<IEnumerable<MovieDto>>(foundMovieDomEnts), CurrentPage = recordSearchRequestParams.RequiredPageNumber, RecordTotalCount = totalMovieCount };
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error retrieving Movies");
                            await transaction.RollbackAsync();
                        }
                    }
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing database operation");
                return null;
            }
        }
    }
}
