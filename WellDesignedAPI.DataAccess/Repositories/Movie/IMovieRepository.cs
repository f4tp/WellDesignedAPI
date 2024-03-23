using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.DataAccess.Entities;

namespace WellDesignedAPI.DataAccess.Repositories
{

    public interface IMovieRepository
    {
        Task<Movie> GetByIdAsync(int id);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int id);
        IQueryable<Movie> BuildEntityFrameworkQueryForSearchSortFilter(RecordSearchRequest recordSearchRequestParams);
        Task<int> GetCountOfFilteredResults(IQueryable<Movie> queryToExecute);
        IQueryable<Movie> ApplyPagingToEntityFrameworkQuery(RecordSearchRequest recordSearchRequestParams, IQueryable<Movie> queryToAmend);
        Task<IEnumerable<Movie>> RetrieveMoviesPagedResultsSearch(IQueryable<Movie> queryToExecute);
    }
    
}
