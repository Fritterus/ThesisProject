using CarSharing.Entities;
using CarSharing.Entities.DataBaseModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.DataLayer.Services
{
    public class Context : IdentityDbContext<User>
    {
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Model> CarModel { get; set; }
        public virtual DbSet<Mark> Make { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Statement> Statement { get; set; }

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("AspNetUsers");
            builder.Entity<Car>().ToTable("Car");
            builder.Entity<Model>().ToTable("CarModel");
            builder.Entity<Mark>().ToTable("Make");
            builder.Entity<Order>().ToTable("Order");
            builder.Entity<Statement>().ToTable("Statement");
        }

    }
}
