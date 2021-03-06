﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace IMDB.Models
{
    public class MovieVM
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Plot { get; set; }
        [Required]
        [StringLength(50)]
        public string Poster { get; set; }
        [Required]
        public int ProducerID { get; set; }
        public IEnumerable<int> ActorList { get; set; }
        //public virtual Producer Producer { get; set; }

        //public virtual ICollection<ActorMovie> ActorsMovies { get; set; }
    }
}