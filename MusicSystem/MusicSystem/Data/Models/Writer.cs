using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Data.Models
{
    public class Writer : BaseEntity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Pseudonym { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
