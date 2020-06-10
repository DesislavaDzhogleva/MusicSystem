using MusicSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.DataProccessor
{
    public class SongDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 2 and 50 characters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public decimal? Price { get; set; }

        public int AlbumId { get; set; }

        [Required]
        public int WriterId { get; set; }

        public ICollection<SongPerformer> Performers { get; set; }
    }
}
