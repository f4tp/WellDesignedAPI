using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellDesignedAPI.EntityFramework.Entities
{
    [Table("Genres")]
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("GenreId", TypeName = "INTEGER")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        private Genre(){} //for EF, requires a parameterless constructor
        public Genre(string genreName)
        {
            if(string.IsNullOrWhiteSpace(genreName))
                throw new ArgumentException("Object creation requires a genre name");

            Name = genreName;
        }

    }
}
