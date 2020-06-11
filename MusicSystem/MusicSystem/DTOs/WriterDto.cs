using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.DTOs
{
    public class WriterDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 5 and 50 characters", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Pseudonym must be between 5 and 50 characters", MinimumLength = 5)]
        public string Pseudonym { get; set; }

        public DateTime? DateOfBirth { get; set; }

    }
}
