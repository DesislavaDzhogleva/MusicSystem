using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Data.Models
{
    public class Performer : BaseEntity<int>
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string StageName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public decimal? NetWorth { get; set; }

        public ICollection<SongPerformer> PerformedSongs { get; set; }
    }
}
