using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_promoción_peliculas_cravero.Models
{
    public class Gender
    {
        public int Id { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El campo es requerido")]
        public string Description { get; set; }
    }
}
