﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.DTOs
{
    public class SongPerformerDto
    {
        [Required]
        public int SongId { get; set; }

        [Required]
        public int PerformerId { get; set; }

    }
}
