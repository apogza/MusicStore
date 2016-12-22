using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public class Artist : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Album> Albums { get; set; }
    }
}

