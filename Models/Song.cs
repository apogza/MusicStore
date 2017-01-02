using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicStore.Models
{
    public class Song : BaseEntity
    {
        [DisplayAttribute(Name="Track Number")]
        public int TrackNumber { get; set; }
        [DisplayAttribute(Name="Song Name")]
        [Required]
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        [NotMapped]
        [DisplayAttribute(Name="Duration")]
        public string Duration 
        {
            get
            {
                return Minutes.ToString() + ":" + Seconds.ToString();
            }
        }
        [DisplayAttribute(Name="Album")]
        [Required]
        public int AlbumID { get; set; }
        public virtual Album Album { get; set; }
    }
}