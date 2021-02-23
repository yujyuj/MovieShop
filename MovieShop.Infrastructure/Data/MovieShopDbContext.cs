using Microsoft.EntityFrameworkCore; //DbContext, OnModelCreating()
using Microsoft.EntityFrameworkCore.Metadata.Builders; //EntityTypeBuilder
using MovieShop.Core.Entities;
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
        public DbSet<Cast> Casts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }

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
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);

            // Movie many <-> Genre many
            modelBuilder.Entity<Movie>().HasMany(m => m.Genres).WithMany(g => g.Movies)
                .UsingEntity<Dictionary<string, object>>("MovieGenre",
                    m => m.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                    g => g.HasOne<Movie>().WithMany().HasForeignKey("MovieId"));

            // User many <-> Role many
            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>("UserRole",
                    u => u.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId"));

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

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            //rules for User
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(128);
            builder.Property(u => u.LastName).HasMaxLength(128);          
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.HashedPassword).HasMaxLength(1024);
            builder.Property(u => u.Salt).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).HasMaxLength(16);
        }

        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            //rules for Role
            builder.ToTable("Role");
            builder.HasKey(r => r.Id);
            builder.Property(t => t.Name).HasMaxLength(20);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            //rules for Cast
            builder.ToTable("Cast");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            //rules for Review
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId });                            //comsopite primary key
            builder.Property(r => r.Rating).HasColumnType("decimal(3, 2)").IsRequired(); //not null
        }


        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            //rules for Purchase
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);          
            builder.Property(p => p.PurchaseNumber).HasColumnType("UniqueIdentifier").IsRequired(); //not null, uniqueidentifier
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18, 2)").IsRequired();       //not null            
            builder.Property(p => p.PurchaseDateTime).IsRequired();                                 //not null           

        }
        
        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            //rules for Favorite
            builder.ToTable("Favorite");
            builder.HasKey(r => r.Id);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            //rules for MovieCast
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new { mc.MovieId, mc.CastId, mc.Character });   //comsopite primary key
            builder.Property(mc => mc.Character).HasMaxLength(450).IsRequired();
        }

    }
}
