using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MovieDatabase.Models
{
    public class MovieDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieActors> MovieActors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(
            //    "Server = (localdb)\\mssqllocaldb; Database = MovieDatabase; Trusted_Connection = True; ");
            optionsBuilder.UseSqlServer(
                "Server=cullbranddb.database.windows.net,1433;Initial Catalog=MovieDatabase;Persist Security Info=False;User " + 
                "ID=sebastian;Password=Password123LOL;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActors>().HasKey(t => new { t.ActorId, t.MovieId });

            modelBuilder.Entity<MovieActors>()
                .HasOne(x => x.Movie)
                .WithMany(x => x.MovieActors)
                .HasForeignKey(x => x.MovieId);

            modelBuilder.Entity<MovieActors>()
                .HasOne(x => x.Actor)
                .WithMany(x => x.MovieActors)
                .HasForeignKey(x => x.ActorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
