using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public class Song : BaseEntity
    {
        [DisplayAttribute(Name="Track Number")]
        public int TrackNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        [DisplayAttribute(Name="Album")]
        [Required]
        public int AlbumID { get; set; }
        public virtual Album Album { get; set; }
    }
}