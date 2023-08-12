using Microsoft.EntityFrameworkCore;
using OtuGlazevTestDB.DataBase.Entities;
using System.Reflection.Emit;


namespace OtuGlazevTestDB.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<CreditType> CreditTypes {get;set;}

        public DataBaseContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=otus_glazev_test;Username=postgres;Password=123123");
        }

    }
}
