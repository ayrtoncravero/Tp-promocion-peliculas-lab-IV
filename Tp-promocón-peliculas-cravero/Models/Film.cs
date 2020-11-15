using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_promoción_peliculas_cravero.Models
{
    public class Film
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Title { get; set; }

        [Display(Name = "Fecha de estreno")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string ReleaseDate { get; set; }

        [Display(Name = "Imagen de portada")]
        public string Photo { get; set; }

        [Display(Name = "Trailer")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Trailer { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Summary { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Genres { get; set; }

        public List<Person> Person { get; set; }
    }
}
