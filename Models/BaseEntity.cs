using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
{
    public abstract class BaseEntity
    {
        [Required]
        public int ID { get; set;}
    }
}