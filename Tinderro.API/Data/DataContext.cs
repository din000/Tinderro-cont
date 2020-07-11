using Microsoft.EntityFrameworkCore;
using Tinderro.API.Models;

namespace Tinderro.API.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        //public DbSet<Dane> chuj { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Photo> photos { get; set; }
        public DbSet<Like> Likes { get; set; }

        // metoda tworzy relacje wiele do wielu i ustawia klucze glowne
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>().HasKey(k => new { k.UserLikesId, k.SomeoneLikesMeId}); // ustawienie kluczy glownych do tabeli Likes

            // tworzy relacje wiele do wielu, user moze miec wiele polubien i kluczem do tego jest userid i ustawiamy sposob usuwanie zeby nie byl kaskadowy
            builder.Entity<Like>().HasOne(u => u.UserLikes).WithMany(u => u.UserLikes)
                                  .HasForeignKey(u => u.UserLikesId).OnDelete(DeleteBehavior.Restrict);

            // dopelnienie do stworzenia relacji wiele do wielu
            builder.Entity<Like>().HasOne(u => u.SomeoneLikes).WithMany(u => u.SomeoneLikes)
                                  .HasForeignKey(u => u.SomeoneLikesMeId).OnDelete(DeleteBehavior.Restrict);            
        }
    }
    
}