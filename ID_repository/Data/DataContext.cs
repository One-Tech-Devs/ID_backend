using ID_model.Models;
using Microsoft.EntityFrameworkCore;

namespace ID_repository.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<DataRequestModel> DataRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientModel>()
                .HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressId);

            modelBuilder.Entity<CompanyModel>()
                .HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressId);

            modelBuilder.Entity<DataRequestModel>()
                .HasOne(d => d.Client)
                .WithMany()
                .HasForeignKey(d => d.ClientId);

            modelBuilder.Entity<DataRequestModel>()
                .HasOne(d => d.Company)
                .WithMany()
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}