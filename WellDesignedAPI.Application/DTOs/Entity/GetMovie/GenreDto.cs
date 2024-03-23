
using System.ComponentModel.DataAnnotations;

namespace WellDesignedAPI.Application.DTOs.Entity.GetMovie
{
    public class GenreDto
    {
        public int Id { get; set; }
        [Required]
        public string GenreName { get; set; }
    }
}
