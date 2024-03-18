using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.Common.Models.Response;

namespace WellDesignedAPI.Application.ApplicationServices
{
    public interface IMovieAppService
    {
        Task<RecordSearchResponse> RetrieveMoviesPagedResultsSearch(RecordSearchRequest recordSearchRequestParams);
    }
}
