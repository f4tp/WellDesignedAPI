using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Logging;
using Moq;
using WellDesignedAPI.Application.ApplicationServices;
using WellDesignedAPI.Application.DTOs.Entity.GetMovie;
using WellDesignedAPI.Common.Models.Response;
using WellDesignedAPI.DataAccess.DbContexts;
using WellDesignedAPI.DataAccess.Entities;
using WellDesignedAPI.Domain.DomainServices;
using WellDesignedAPI.Tests.Unit.Fixtures.MovieFixtures;
using Xunit;

//integration tests use actual DB connection in instances so required to obtain connection string
[assembly: UserSecretsId("3277df5b-7658-439b-8f67-9fb37c651b0e")]

namespace WellDesignedAPI.Tests.Integration.Systems.AppServices
{
    //TODO - not finished / comprehensive
    public class MovieAppServiceIntegrationTests
    {
        public IMovieAppService _systemUnderTestMovieAppService { get; set; }
        public IMovieDomainService _systemUnderMovieDomainService { get; set; }

        private readonly Mock<ILogger<MovieAppService>> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;


        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public MovieAppServiceIntegrationTests()
        {
            _mockLogger = new Mock<ILogger<MovieAppService>>();
            _mockMapper = new Mock<IMapper>();

            //Integration tests use actual DB connection so building an IConfiguration in scope to grab this from the designated location
            _configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables() // For test automation in deployment pipeline
                .AddUserSecrets<MovieAppServiceIntegrationTests>() // For dev environment
                .Build();

            // Set up the DbContext with the actual connection string
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(_configuration["ConnectionStrings:WellDesignedAPIMain"], providerOptions => providerOptions.EnableRetryOnFailure());

            _dbContext = new AppDbContext(optionsBuilder.Options);

        }

        internal void UseDefaultSetup()
        {
            //config mock mapper to map any IEnumerable<MovieDto> mapping to IEnumerable<Movie>, explicitly specifying properties to match
            //Not used yet
            _mockMapper.Setup(mapper => mapper.Map<MovieDto>(It.IsAny<Movie>()))
                .Returns((Movie source) =>
                {
                    return new MovieDto
                    {
                        Id = source.Id,
                        DateReleased = source.DateReleased,
                        MovieTitle = source.MovieTitle,
                        Synopsis = source.Synopsis,
                        PopularityLevel = source.PopularityLevel,
                        NoOfVotes = source.NoOfVotes,
                        VoteAverageScore = source.VoteAverageScore,
                        MainLanguage = source.MainLanguage,
                        UrlForPoster = source.UrlForPoster,
                        MovieGenres = source.MovieGenres?.Select(mg =>
                            new MovieGenreDto
                            {
                                Id = mg.Id,
                                MovieId = mg.MovieId,
                                GenreId = mg.GenreId,
                                Genre = new GenreDto
                                {
                                    Id = mg.Genre.Id,
                                    GenreName = mg.Genre.GenreName
                                }
                            })
                    };
                });

            _mockMapper.Setup(mapper => mapper.Map<MovieGenreDto>(It.IsAny<MovieGenre>()))
                .Returns((MovieGenre source) =>
                {
                    return new MovieGenreDto
                    {
                        Id = source.Id,
                        MovieId = source.MovieId,
                        GenreId = source.GenreId,
                        Genre = new GenreDto
                        {
                            Id = source.Genre.Id,
                            GenreName = source.Genre.GenreName
                        }
                    };
                });

            _mockMapper.Setup(mapper => mapper.Map<GenreDto>(It.IsAny<Genre>()))
                .Returns((Genre source) =>
                {
                    return new GenreDto
                    {
                        Id = source.Id,
                        GenreName = source.GenreName
                    };
                });

            _systemUnderMovieDomainService = new MovieDomainService(_dbContext);
            _systemUnderTestMovieAppService = new MovieAppService(_mockLogger.Object, _mockMapper.Object, _systemUnderMovieDomainService, _dbContext);

        }

        [Fact]
        internal async Task GetMovies_UsingGetValidRecordSearchRequest_ResponseIsNotNull()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchreauest = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();

            //Act
            var recordSearchResponse = await _systemUnderTestMovieAppService.RetrieveMoviesPagedResultsSearch(validRecordSearchreauest);

            //Assert
            recordSearchResponse.Should().NotBeNull();
        }

        [Fact]
        internal async Task GetMovies_UsingGetValidRecordSearchRequest_ErrorMessageOnResponseIsNullOrWhiteSpace()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchreauest = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();

            //Act
            var recordSearchResponse = await _systemUnderTestMovieAppService.RetrieveMoviesPagedResultsSearch(validRecordSearchreauest);

            //Assert
            recordSearchResponse.ErrorMessage.Should().BeNullOrWhiteSpace();
        }

        [Fact]
        internal async Task GetMovies_UsingGetValidRecordSearchRequest_RecordTotalCountGreaterThanZero()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchreauest = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();

            //Act
            var recordSearchResponse = await _systemUnderTestMovieAppService.RetrieveMoviesPagedResultsSearch(validRecordSearchreauest);

            //Assert
            recordSearchResponse.RecordTotalCount.Should().BeGreaterThan(0);
        }


        [Fact]
        internal async Task GetMovies_UsingGetValidRecordSearchRequest_CurrentPageNoGreaterThanZero()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchreauest = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();

            //Act
            var recordSearchResponse = await _systemUnderTestMovieAppService.RetrieveMoviesPagedResultsSearch(validRecordSearchreauest);

            //Assert
            recordSearchResponse.CurrentPage.Should().BeGreaterThan(0);
        }


        [Fact]
        internal async Task GetMovies_UsingGetValidRecordSearchRequest_ReturnedRecordsNotNull()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchreauest = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();

            //Act
            var recordSearchResponse = await _systemUnderTestMovieAppService.RetrieveMoviesPagedResultsSearch(validRecordSearchreauest);

            //Assert
            recordSearchResponse.Records.Should().NotBeNull();

        }


        [Fact]
        internal async Task GetMovies_UsingGetValidRecordSearchRequest_ReturnedRecordsAssignableToIEnumerableOfMovieDtos()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchreauest = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();

            //Act
            var recordSearchResponse = await _systemUnderTestMovieAppService.RetrieveMoviesPagedResultsSearch(validRecordSearchreauest);

            //Assert
            recordSearchResponse.Records.Should().BeAssignableTo<IEnumerable<MovieDto>>();

        }

        [Fact]
        internal async Task GetMovies_UsingGetValidRecordSearchRequest_ReturnedRecordsParsedToIEnumerableOfMovieDtosNotNull()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchreauest = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();

            //Act
            var recordSearchResponse = await _systemUnderTestMovieAppService.RetrieveMoviesPagedResultsSearch(validRecordSearchreauest);

            //Assert
            var movies = recordSearchResponse.Records as IEnumerable<MovieDto>; // Cast to IEnumerable<Movie>
            movies.Should().NotBeNull();

        }

    }
}
