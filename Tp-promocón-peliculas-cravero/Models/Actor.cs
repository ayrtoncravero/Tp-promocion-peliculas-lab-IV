using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_promoción_peliculas_cravero.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Name { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "El campo es requerido")]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Biografia")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Biography { get; set; }

        [Display(Name = "Foto de persona")]
        public string Photo { get; set; }
    }
}
