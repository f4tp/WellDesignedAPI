using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDesignedAPI.Application.ApplicationServices;
using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.Common.Models.Response;
using WellDesignedAPI.Tests.Unit.Fixtures.Movie;
using WellDesignedAPI.Web.Host.Controllers;
using Xunit;

namespace WellDesignedAPI.Tests.Unit.Systems.Controllers
{
    public class MovieControllerTests
    {
        private MovieController _systemUnderTest;
        private readonly Mock<ILogger<MovieController>> _mockLogger;
        private readonly Mock<IMovieAppService> _mockMovieAppService;
        public MovieControllerTests()
        {
            _mockLogger = new Mock<ILogger<MovieController>>();
            _mockMovieAppService = new Mock<IMovieAppService>();
        }

        private void UseDefaultSetup()
        {
            _systemUnderTest = new MovieController(
                _mockLogger.Object, 
                _mockMovieAppService.Object            
            );
        }

        //TODO
        //Only sorting tested in here, finish all testing
        //Routine not written using TDD, write next one using it
        //No integration tests as not that far through the logic, get them sorted

        [Fact]
        public async Task GetMovies_SortByPopulatedButSortAscendingNot_ReturnsBadRequestObjectResult()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchRequestState = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();
            validRecordSearchRequestState.SortAscending = null;

            //Act
            var result = await _systemUnderTest.GetMovies(validRecordSearchRequestState);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetMovies_SortByPopulatedButSortAscendingNot_ReturnsRecordSearchResponseTypeOnPayloadOfBadRequestObjectResult()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchRequestState = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();
            validRecordSearchRequestState.SortAscending = null;

            // Act
            var result = await _systemUnderTest.GetMovies(validRecordSearchRequestState) as BadRequestObjectResult;

            //Assert
            result.Value.Should().BeOfType<RecordSearchResponse>();
        }


        [Fact]
        public async Task GetMovies_SortAscendingPopulatedBySortByNot_ReturnsBadRequestObjectResult()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchRequestState = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();
            validRecordSearchRequestState.SortBy = null;

            //Act
            var result = await _systemUnderTest.GetMovies(validRecordSearchRequestState);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetMovies_SortAscendingPopulatedBySortByNot_ReturnsRecordSearchResponseTypeOnPayloadOfBadRequestObjectResult()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchRequestState = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();
            validRecordSearchRequestState.SortBy = null;

            // Act
            var result = await _systemUnderTest.GetMovies(validRecordSearchRequestState) as BadRequestObjectResult;

            //Assert
            result.Value.Should().BeOfType<RecordSearchResponse>();
        }


        [Fact]
        public async Task GetMovies_InvalidSortByColumnInRecordSearchRequestState_ReturnsBadRequestObjectResult()
        {
            //Arrange
            UseDefaultSetup();
            var validRecordSearchRequestState = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();
            validRecordSearchRequestState.SortBy = "IDoNotExist";

            //Act
            var result = await _systemUnderTest.GetMovies(validRecordSearchRequestState);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetMovies_InvalidSortByColumnInRecordSearchRequestState_ReturnsRecordSearchResponseTypeOnPayloadOfBadRequestObjectResult()
        {
            // Arrange
            UseDefaultSetup();
            var validRecordSearchRequestState = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();
            validRecordSearchRequestState.SortBy = "IDoNotExist";

            // Act
            var result = await _systemUnderTest.GetMovies(validRecordSearchRequestState) as BadRequestObjectResult;
            
            //Assert
            result.Value.Should().BeOfType<RecordSearchResponse>();
        }

        [Fact]
        public async Task GetMovies_InvalidSortByColumnInRecordSearchRequestState_ReturnsValidErrorMessage()
        {
            // Arrange
            UseDefaultSetup();
            var validRecordSearchRequestState = MovieSearchFilterStateFixtures.GetValidRecordSearchRequest();
            validRecordSearchRequestState.SortBy = "IDoNotExist";

            // Act
            var result = await _systemUnderTest.GetMovies(validRecordSearchRequestState) as BadRequestObjectResult;
            var payload = result.Value as RecordSearchResponse;

            //Assert
            payload.ErrorMessage.Should().Contain("Issue with property name");
        }

    }
}
