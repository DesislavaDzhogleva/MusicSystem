using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.DTOs
{
    public class AlbumDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Name must be between 3 and 200 characters", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public int? ProducerId { get; set; }

    }
}
