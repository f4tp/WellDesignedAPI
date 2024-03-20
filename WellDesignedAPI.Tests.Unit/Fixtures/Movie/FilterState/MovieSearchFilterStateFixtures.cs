using WellDesignedAPI.Application.DTOs.Entity.GetMovie;
using WellDesignedAPI.Common.Models.Request;
using WellDesignedAPI.Common.Models.Response;
using WellDesignedAPI.DataAccess.Entities;

namespace WellDesignedAPI.Tests.Unit.Fixtures.MovieFixtures
{
    public static class MovieSearchFilterStateFixtures
    {
        public static RecordSearchRequest GetValidRecordSearchRequest()
        {
            return new RecordSearchRequest()
            {
                RequiredPageNumber = 1,
                NumberOfResultsPerPage = 100,
                Filters = new List<FilterState>
                {
                    new FilterState { PropertyName = "MainLanguage", ValueFromOrEqualTo = "en" },
                    new FilterState { PropertyName = "DateReleased", ValueFromOrEqualTo = "2022-01-01", ValueTo = "2023-01-01" }
                },
                SortBy = "MovieTitle",
                SortAscending = true,
                SearchTerm = "Spider-Man"
            };
        }

        //Json for the above
        //{
        //  "requiredPageNumber": 1,
        //  "numberOfResultsPerPage": 100,
        //  "filters": [
        //    {
        //      "propertyName": "MainLanguage",
        //      "valueFromOrEqualTo": "en"
        //    },
        //    {
        //      "propertyName": "DateReleased",
        //      "valueFromOrEqualTo": "2022-01-01",
        //      "valueTo" : "2023-01-01"

        //    }
        //  ],
        //  "sortBy": "MovieTitle",
        //  "sortAscending": true,
        //  "searchTerm": "Spider-Man"
        //}


        //Json for a return
        //{
        //  "records": [
        //    {
        //      "id": 798,
        //      "dateReleased": "2022-10-07T00:00:00",
        //      "movieTitle": "Spider-Man: Across the Spider-Verse (Part One)",
        //      "synopsis": "Miles Morales returns for the next chapter of the Spider-Verse saga; an epic adventure that will transport Brooklyn’s full-time; friendly neighborhood Spider-Man across the Multiverse to join forces with Gwen Stacy and a new team of Spider-People to face off with a villain more powerful than anything they have ever encountered.",
        //      "popularityLevel": 69.174,
        //      "noOfVotes": 0,
        //      "voteAverageScore": 0,
        //      "mainLanguage": "en",
        //      "urlForPoster": "https://image.tmdb.org/t/p/original/l2hjrByNNohRZhNvQrw6TFzAF5i.jpg",
        //      "movieGenres": [
        //        {
        //          "id": 2494,
        //          "movieId": 798,
        //          "genreId": 7,
        //          "genre": {
        //            "id": 7,
        //            "genreName": "Animation"
        //          }
        //},
        //        {
        //    "id": 2495,
        //          "movieId": 798,
        //          "genreId": 1,
        //          "genre": {
        //        "id": 1,
        //            "genreName": "Action"
        //          }
        //},
        //        {
        //    "id": 2496,
        //          "movieId": 798,
        //          "genreId": 3,
        //          "genre": {
        //        "id": 3,
        //            "genreName": "Science Fiction"
        //          }
        //},
        //        {
        //    "id": 2497,
        //          "movieId": 798,
        //          "genreId": 8,
        //          "genre": {
        //        "id": 8,
        //            "genreName": "Comedy"
        //          }
        //},
        //        {
        //    "id": 2498,
        //          "movieId": 798,
        //          "genreId": 2,
        //          "genre": {
        //        "id": 2,
        //            "genreName": "Adventure"
        //          }
        //}
        //      ]
        //    }
        //  ],
        //  "recordTotalCount": 1,
        //  "currentPage": 1,
        //  "errorMessage": null
        //}


        
    }
}
