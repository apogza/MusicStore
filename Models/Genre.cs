using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models
 {
     public class Genre : BaseEntity
     {
         [Required]
         [DisplayAttribute(Name = "Label")]
         public string Label { get; set; }
        
         public ICollection<Album> Albums { get; set; }
     }
 }