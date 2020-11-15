using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp_promoción_peliculas_cravero.Models;

namespace Tp_promocón_peliculas_cravero.ViewModel
{
    public class FilmViewModel
    {
        public List<Person> Persons { get; set; }
        public SelectList ListPersons { get; set; }
        public int? PersonId { get; set; }
    }
}
