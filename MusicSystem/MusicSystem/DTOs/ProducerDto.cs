using MusicSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.DTOs
{
    public class ProducerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name must be between 6 and 50 characters", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Pseudonym must be between 6 and 100 characters", MinimumLength = 5)]
        public string Pseudonym { get; set; }

        [Required]
        [RegularExpression(@"\+359 [0-9]{3} [0-9]{3} [0-9]{3}")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
