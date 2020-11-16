using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_promoción_peliculas_cravero.Models
{
    public class MovieActor
    {
        [Display(Name = "Pelicula")]
        public int FilmId { get; set; }


        [Display(Name = "Pelicula")]
        public Film Film { get; set; }


        [Display(Name = "Actor")]
        public int PersonId { get; set; }


        [Display(Name = "Actor")]
        public Person Person { get; set; }
    }
}
