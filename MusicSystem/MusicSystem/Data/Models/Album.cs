using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Data.Models
{
    public class Album : BaseEntity<int>
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public int ProducerId { get; set; }

        public Producer Producer { get; set; }

       
    }
}
