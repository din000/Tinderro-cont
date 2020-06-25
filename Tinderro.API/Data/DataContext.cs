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
    }
    
}