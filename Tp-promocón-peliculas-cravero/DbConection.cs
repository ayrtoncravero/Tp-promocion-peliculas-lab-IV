using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp_promoción_peliculas_cravero.Models;

namespace Tp_promocón_peliculas_cravero
{
    public class DbConection : DbContext
    {
        public DbConection(DbContextOptions<DbConection> options)
            :base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Tp-promocion-peliculas-cravero;Trusted_Connection=True;MultipleActiveResultSets=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>().HasKey(x => new { x.FilmId, x.PersonId });
        }

        public DbSet<Person> Actor { get; set; } 
        public DbSet<Film> Film { get; set; } 
        public DbSet<Gender> Gender { get; set; } 
        public DbSet<MovieActor> MoviesActor { set; get; }
    }
}
