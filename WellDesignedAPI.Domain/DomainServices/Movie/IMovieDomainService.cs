using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.EntityFramework.Entities;

namespace WellDesignedAPI.Domain.DomainServices
{
    public interface IMovieDomainService
    {
        Task<IEnumerable<Movie>> RetrieveMoviesPagedResultsSearch(RecordSearchRequest recordSearchRequestParams);

        Task<int> GetCountOfMovies();
    }
}
