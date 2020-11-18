using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Tp_promocón_peliculas_cravero.Models;

namespace Tp_promoción_peliculas_cravero.Models
{
    public class Gender
    {
        public int Id { get; set; }


        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Description { get; set; }

        List<Gender> Genders { get; set; }
    }
}
