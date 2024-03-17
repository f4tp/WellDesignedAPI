﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WellDesignedAPI.Application.DTOs.Entity.GetMovie
{
    public class MovieGenreDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int GenreId { get; set; }
        [ForeignKey(nameof(GenreId))]
        public GenreDto Genre { get; set; }
    }
}
