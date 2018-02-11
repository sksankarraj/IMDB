using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IMDB.Models
{
    public class Actor
    {
        [Key]
        public int ID { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(1)]
        public string Sex { get; set; }
        [Required]
        public DateTime DOB { get; set; }

        public virtual ICollection<ActorMovie> ActorsMovies { get; set; }
        
    }
}