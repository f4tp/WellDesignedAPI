using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WellDesignedAPI.Application.DTOs.Entity.GetMovie;
using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.Common.Models.Response;
using WellDesignedAPI.Domain.DomainServices;
using WellDesignedAPI.EntityFramework.DbContexts;

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
                            var queryToExecute = _movieDomainService.BuildEntityFrameworkQueryForSearchSortFilter(recordSearchRequestParams);
                            //count the total results that would be brought back after search and filter applied
                            var totalMovieCount = await _movieDomainService.GetCountOfFilteredResults(queryToExecute);
                            //then apply paging
                            queryToExecute = _movieDomainService.ApplyPagingToEntityFrameworkQuery(recordSearchRequestParams, queryToExecute);
                            //run the query to return the found movies
                            var foundMovieDtos = _mapper.Map<IEnumerable<MovieDto>>(await _movieDomainService.RetrieveMoviesPagedResultsSearch(queryToExecute));
                            await transaction.CommitAsync();
                            result = new RecordSearchResponse() { Records = foundMovieDtos, CurrentPage = recordSearchRequestParams.RequiredPageNumber, RecordTotalCount = totalMovieCount };
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
