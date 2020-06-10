using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSystem.Data.Models
{
    public abstract class BaseEntity<TKey> 
    {
        [Key]
        public TKey Id { get; set; }
    }
}
