
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public class Album : BaseEntity
    {
        [Required]
        [DisplayAttribute(Name="Album Name")]
        public string Name { get; set; }
        
        [DisplayAttribute(Name = "Year of Release")]
        public int YearRelease { get; set; }
        public ICollection<Song> Songs { get; set; }

        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }
        public int ArtistID { get; set; }
        public virtual Artist Artist { get; set; }
    }
}