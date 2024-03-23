using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WellDesignedAPI.Domain.DomainEntities
{
    public class GenreDomEnt
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public GenreDomEnt(string genreName)
        {
            if (string.IsNullOrWhiteSpace(genreName))
                throw new ArgumentException("Object creation requires a genre name");

            GenreName = genreName;
        }
    }
}
