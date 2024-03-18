using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.EntityFramework.Entities;

namespace WellDesignedAPI.Domain.DomainServices
{
    public interface IMovieDomainService
    {
        IQueryable<Movie> BuildEntityFrameworkQueryForSearchSortFilter(RecordSearchRequest recordSearchRequestParams);
        Task<int> GetCountOfFilteredResults(IQueryable<Movie> queryToExecute);
        IQueryable<Movie> ApplyPagingToEntityFrameworkQuery(RecordSearchRequest recordSearchRequestParams, IQueryable<Movie> queryToAmend);
        Task<IEnumerable<Movie>> RetrieveMoviesPagedResultsSearch(IQueryable<Movie> queryToExecute);
    }
}
