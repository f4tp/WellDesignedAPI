using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellDesignedAPI.Common.Models.Request;

namespace WellDesignedAPI.Tests.Unit.Fixtures.Movie
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
                    new FilterState { PropertyName = "MovieTitle", ValueFromOrEqualTo = "Spider-Man" },
                    new FilterState { PropertyName = "ReleaseDate", ValueFromOrEqualTo = "2022-01-01", ValueTo = "2023-01-01" }
                },
                SortBy = "MovieTitle",
                SortAscending = true,
                SearchTerm = "Spider-Man"

            };
        }
    }
}
