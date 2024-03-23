using AutoMapper;
using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.DataAccess.Entities;
using WellDesignedAPI.DataAccess.Repositories;
using WellDesignedAPI.Domain.DomainEntities;

#region Domain Layer
//Domain
//  • Hosts DomainServices
//  • Hosts DomEnts
//      • Behaviours
//          • Rich Domain Model
//      • Data content
//          • Properties
//          • Validation attributes (for in program object building)
//  • Map DomEnts<> Ents
//  • Purposes
//      • Calls DataAccess layer
//      • Perform business logic
//      • Performs cross domain logic
//  •	Never interacts with the database
#endregion


namespace WellDesignedAPI.Domain.DomainServices
{
    public class MovieDomainService : IMovieDomainService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;

        public MovieDomainService(IMapper mapper, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
        }

        public IQueryable<Movie> GetEntityFrameworkQueryForSearchSortFilter(RecordSearchRequest recordSearchRequestParams)
        {
            return _movieRepository.BuildEntityFrameworkQueryForSearchSortFilter(recordSearchRequestParams);
        }

        public IQueryable<Movie>ApplyPagingToEntityFrameworkQuery(RecordSearchRequest recordSearchRequestParams, IQueryable<Movie> queryToAmend)
        {
            return _movieRepository.ApplyPagingToEntityFrameworkQuery(recordSearchRequestParams, queryToAmend);
        }

        public async Task<int> GetCountOfFilteredResults(IQueryable<Movie> queryToExecute)
        {
            return await _movieRepository.GetCountOfFilteredResults(queryToExecute);
        }

        public async Task<IEnumerable<MovieDomEnt>> RetrieveMoviesPagedResultsSearch(IQueryable<Movie> queryToExecute)
        {
            return _mapper.Map<IEnumerable<MovieDomEnt>>(await _movieRepository.RetrieveMoviesPagedResultsSearch(queryToExecute));
        }
    }
}
