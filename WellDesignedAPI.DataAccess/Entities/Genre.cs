using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellDesignedAPI.DataAccess.Entities
{
    [Table("Genres")]
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("GenreId", TypeName = "INTEGER")]
        public int Id { get; set; }
        [Required]
        [Column("Name", TypeName = "NVARCHAR(500)")]
        public string GenreName { get; set; }
    }
}
