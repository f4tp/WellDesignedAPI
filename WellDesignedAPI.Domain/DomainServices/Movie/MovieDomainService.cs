using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.EntityFramework.DbContexts;
using WellDesignedAPI.EntityFramework.Entities;
using WellDesignedAPI.EntityFramework.Extensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WellDesignedAPI.Domain.DomainServices
{
    public class MovieDomainService : IMovieDomainService
    {
        private readonly AppDbContext _dbContext;

        public MovieDomainService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Movie> BuildEntityFrameworkQueryForSearchSortFilter(RecordSearchRequest recordSearchRequestParams)
        {
            var query = _dbContext.Movies.AsQueryable();


            // Sorting
            if (!string.IsNullOrEmpty(recordSearchRequestParams.SortBy) && recordSearchRequestParams.SortAscending.HasValue)
                query = query.OrderByDynamic(recordSearchRequestParams.SortBy, recordSearchRequestParams.SortAscending ?? true);

            // filtering (includes a staright match for a string, or ranges for date and numeric data types)
            if (recordSearchRequestParams.Filters != null)
            {
                foreach (var filter in recordSearchRequestParams.Filters)
                {
                    query = ApplyFilter(query, filter);
                }
            }

            // Search
            if (!string.IsNullOrEmpty(recordSearchRequestParams.SearchTerm))
            {
                //TODO - allow to search on all fields
                query = query.Where(mov => mov.MovieTitle.Contains(recordSearchRequestParams.SearchTerm));
            }

            return query;
        }


        public IQueryable<Movie>ApplyPagingToEntityFrameworkQuery(RecordSearchRequest recordSearchRequestParams, IQueryable<Movie> queryToAmend)
        {
            // Paging (uses Requested Page Number and Count per page)
            int skipCount = (recordSearchRequestParams.RequiredPageNumber - 1) * recordSearchRequestParams.NumberOfResultsPerPage;
            queryToAmend = queryToAmend.Skip(skipCount).Take(recordSearchRequestParams.NumberOfResultsPerPage);
            return queryToAmend;
        }


        public async Task<int> GetCountOfFilteredResults(IQueryable<Movie> queryToExecute)
        {
            return await queryToExecute.CountAsync();

        }

        public async Task<IEnumerable<Movie>> RetrieveMoviesPagedResultsSearch(IQueryable<Movie> queryToExecute)
        {
            return await queryToExecute.Include(mov => mov.MovieGenres).ThenInclude(movGen => movGen.Genre).ToListAsync();

        }


        /// <summary>
        /// Applie sfiltering to the Entity Framework query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <remarks>This likely can be made to better work with the open-closed principle (made more generic), look to refactor</remarks>
        /// <returns></returns>

        private IQueryable<Movie> ApplyFilter(IQueryable<Movie> query, FilterState filter)
        {
            switch (filter.PropertyName)
            {
                case nameof(Movie.MainLanguage):
                    query = query.Where(mov => mov.MainLanguage.Contains(filter.ValueFromOrEqualTo));
                    break;
                case nameof(Movie.DateReleased):
                    if (!string.IsNullOrEmpty(filter.ValueFromOrEqualTo) && DateTime.TryParse(filter.ValueFromOrEqualTo, out DateTime fromDate))
                    {
                        query = query.Where(mov => mov.DateReleased >= fromDate);
                    }
                    if (!string.IsNullOrEmpty(filter.ValueTo) && DateTime.TryParse(filter.ValueTo, out DateTime toDate))
                    {
                        query = query.Where(mov => mov.DateReleased <= toDate);
                    }
                    break;
                case nameof(Movie.PopularityLevel):
                    if (!string.IsNullOrEmpty(filter.ValueFromOrEqualTo) && decimal.TryParse(filter.ValueFromOrEqualTo, out decimal fromPopularity))
                    {
                        query = query.Where(mov => mov.PopularityLevel >= fromPopularity);
                    }
                    if (!string.IsNullOrEmpty(filter.ValueTo) && decimal.TryParse(filter.ValueTo, out decimal toPopularity))
                    {
                        query = query.Where(mov => mov.PopularityLevel <= toPopularity);
                    }
                    break;
                case nameof(Movie.NoOfVotes):
                    if (!string.IsNullOrEmpty(filter.ValueFromOrEqualTo) && int.TryParse(filter.ValueFromOrEqualTo, out int fromVoteCount))
                    {
                        query = query.Where(mov => mov.NoOfVotes >= fromVoteCount);
                    }
                    if (!string.IsNullOrEmpty(filter.ValueTo) && int.TryParse(filter.ValueTo, out int toVoteCount))
                    {
                        query = query.Where(mov => mov.NoOfVotes <= toVoteCount);
                    }
                    break;
                case nameof(Movie.VoteAverageScore):
                    if (!string.IsNullOrEmpty(filter.ValueFromOrEqualTo) && decimal.TryParse(filter.ValueFromOrEqualTo, out decimal fromVoteAverage))
                    {
                        query = query.Where(mov => mov.VoteAverageScore >= fromVoteAverage);
                    }
                    if (!string.IsNullOrEmpty(filter.ValueTo) && decimal.TryParse(filter.ValueTo, out decimal toVoteAverage))
                    {
                        query = query.Where(mov => mov.VoteAverageScore <= toVoteAverage);
                    }
                    break;
                case nameof(Genre.GenreName):
                    query = query.Where(mov => mov.MovieGenres.Any(movGen => movGen.Genre.GenreName == filter.ValueFromOrEqualTo));
                    break;
            }
            return query;
        }

    }

}
