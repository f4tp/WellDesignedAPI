using Microsoft.EntityFrameworkCore;
using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.EntityFramework.DbContexts;
using WellDesignedAPI.EntityFramework.Entities;
using WellDesignedAPI.EntityFramework.Extensions;

namespace WellDesignedAPI.Domain.DomainServices
{
    public class MovieDomainService : IMovieDomainService
    {
        private readonly AppDbContext _dbContext;

        public MovieDomainService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetCountOfMovies()
        {
            return await _dbContext.Movies.CountAsync();
        }


        public async Task<IEnumerable<Movie>> RetrieveMoviesPagedResultsSearch(RecordSearchRequest recordSearchRequestParams)
        {
            var query = _dbContext.Movies.AsQueryable();


            // Sorting filter
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

            // Search filter
            if (!string.IsNullOrEmpty(recordSearchRequestParams.SearchTerm))
                query = query.Where(m => m.MovieTitle.Contains(recordSearchRequestParams.SearchTerm));

            // Paging (uses Requested Page Number and Count per page)
            
            int skipCount = (recordSearchRequestParams.RequiredPageNumber - 1) * recordSearchRequestParams.NumberOfResultsPerPage;
            query = query.Skip(skipCount).Take(recordSearchRequestParams.NumberOfResultsPerPage);
            
            return await query.Include(mov => mov.MovieGenres).ThenInclude(movGen => movGen.Genre).ToListAsync();

        }


        /// <summary>
        /// 
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
                    query = query.Where(m => m.MainLanguage.Contains(filter.Value));
                    break;
                case nameof(Movie.DateReleased):
                    if (!string.IsNullOrEmpty(filter.RangeFrom) && DateTime.TryParse(filter.RangeFrom, out DateTime fromDate))
                    {
                        query = query.Where(m => m.DateReleased >= fromDate);
                    }
                    if (!string.IsNullOrEmpty(filter.RangeTo) && DateTime.TryParse(filter.RangeTo, out DateTime toDate))
                    {
                        query = query.Where(m => m.DateReleased <= toDate);
                    }
                    break;
                case nameof(Movie.PopularityLevel):
                    if (!string.IsNullOrEmpty(filter.RangeFrom) && decimal.TryParse(filter.RangeFrom, out decimal fromPopularity))
                    {
                        query = query.Where(m => m.PopularityLevel >= fromPopularity);
                    }
                    if (!string.IsNullOrEmpty(filter.RangeTo) && decimal.TryParse(filter.RangeTo, out decimal toPopularity))
                    {
                        query = query.Where(m => m.PopularityLevel <= toPopularity);
                    }
                    break;
                case nameof(Movie.NoOfVotes):
                    if (!string.IsNullOrEmpty(filter.RangeFrom) && int.TryParse(filter.RangeFrom, out int fromVoteCount))
                    {
                        query = query.Where(m => m.NoOfVotes >= fromVoteCount);
                    }
                    if (!string.IsNullOrEmpty(filter.RangeTo) && int.TryParse(filter.RangeTo, out int toVoteCount))
                    {
                        query = query.Where(m => m.NoOfVotes <= toVoteCount);
                    }
                    break;
                case nameof(Movie.VoteAverageScore):
                    if (!string.IsNullOrEmpty(filter.RangeFrom) && double.TryParse(filter.RangeFrom, out double fromVoteAverage))
                    {
                        query = query.Where(m => m.VoteAverageScore >= fromVoteAverage);
                    }
                    if (!string.IsNullOrEmpty(filter.RangeTo) && double.TryParse(filter.RangeTo, out double toVoteAverage))
                    {
                        query = query.Where(m => m.VoteAverageScore <= toVoteAverage);
                    }
                    break;
                case nameof(MovieGenre.Genre):
                    query = query.Where(m => m.MovieGenres.Any(mg => mg.Genre.Name == filter.Value));
                    break;
            }
            return query;
        }

    }

}
