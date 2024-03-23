using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.DataAccess.Entities;
using WellDesignedAPI.Domain.DomainEntities;

namespace WellDesignedAPI.Domain.DomainServices
{
    public interface IMovieDomainService
    {
        IQueryable<Movie> GetEntityFrameworkQueryForSearchSortFilter(RecordSearchRequest recordSearchRequestParams);
        Task<int> GetCountOfFilteredResults(IQueryable<Movie> queryToExecute);
        IQueryable<Movie> ApplyPagingToEntityFrameworkQuery(RecordSearchRequest recordSearchRequestParams, IQueryable<Movie> queryToAmend);
        Task<IEnumerable<MovieDomEnt>> RetrieveMoviesPagedResultsSearch(IQueryable<Movie> queryToExecute);
    }
}
