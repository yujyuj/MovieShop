using Microsoft.EntityFrameworkCore; //DbContext, OnModelCreating()
using Microsoft.EntityFrameworkCore.Metadata.Builders; //EntityTypeBuilder
using MovieShop.Core.Entities; //Genre, Trailer
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }

        //constructor
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
        }

        // This method is called by the framework when your context is first created to build the model and its mappings in memory
        // add our own configuration to specify DbSet rules
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // use Fluent API for entites. Action
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<Movie>().HasMany(m => m.Genres).WithMany(g => g.Movies)
                .UsingEntity<Dictionary<string, object>>("MovieGenre",
                    m => m.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                    g => g.HasOne<Movie>().WithMany().HasForeignKey("MovieId"));

        }
        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            //rules for Movie table entity
            builder.ToTable("Movie");  //name the table Movie
            builder.HasKey(m => m.Id); //set primary key
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m); //default value 9.9
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            //rules for Trailer
            builder.ToTable("Trailer"); //name the table Trailer
            builder.HasKey(t => t.Id);  //set primary key
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }

    }
}
