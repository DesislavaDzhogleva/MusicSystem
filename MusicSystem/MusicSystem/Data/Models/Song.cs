using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Data.Models
{
    public class Song : BaseEntity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        public Genre Genre { get; set; }

        public decimal? Price { get; set; }

        
        public int AlbumId { get; set; }

        public Album Album { get; set; }

        [Required]
        public int WriterId { get; set; }

        public Writer Writer { get; set; }

        public ICollection<SongPerformer> Performers { get; set; }
    }
}
