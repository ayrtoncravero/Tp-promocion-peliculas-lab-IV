using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_promoción_peliculas_cravero.Models
{
    public class MovieActor
    {
        public int PersonId { get; set; }
        public int FilmId { get; set; }
        public Person Persons { get; set; }
        public Film Films { get; set; }
    }
}
