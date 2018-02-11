using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace IMDB.Models
{
    public class ActorMovie
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int ActorID { get; set; }
        [Required]
        public int MovieID { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Movie Movie { get; set; }
    }
}