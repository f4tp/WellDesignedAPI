using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellDesignedAPI.Domain.DomainEntities
{
    public class MovieGenreDomEnt
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public MovieDomEnt Movie { get; set; }
        public int GenreId { get; set; }
        public GenreDomEnt Genre { get; set; }

        public MovieGenreDomEnt(MovieDomEnt movie, GenreDomEnt genre)
        {
            if (movie == null)
                throw new ArgumentException("Object creation requires a movie");
            if (genre == null)
                throw new ArgumentException("Object creation requires a movie");
            if (movie.Id < 1)
                throw new ArgumentException("Object creation requires a movie id");
            if (genre.Id < 1)
                throw new ArgumentException("Object creation requires a genre id");

            MovieId = movie.Id;
            GenreId = genre.Id;
        }
    }
}
