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
        public DbSet<UserModel> Users { get; set; }
        public DbSet<DataRequestModel> DataRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relação entre UserModel e AddressModel
            modelBuilder.Entity<UserModel>()
                .HasOne(u => u.Address)
                .WithMany()
                .HasForeignKey(u => u.AddressId);

            // Relação entre ClientModel e UserModel
            modelBuilder.Entity<ClientModel>()
                .HasBaseType<UserModel>();

            // Relação entre CompanyModel e UserModel
            modelBuilder.Entity<CompanyModel>()
                .HasBaseType<UserModel>();

            // Relação entre DataRequestModel e ClientModel
            modelBuilder.Entity<DataRequestModel>()
                .HasOne(d => d.Client)
                .WithMany()
                .HasForeignKey(d => d.ClientId);

            // Relação entre DataRequestModel e CompanyModel
            modelBuilder.Entity<DataRequestModel>()
                .HasOne(d => d.Company)
                .WithMany()
                .HasForeignKey(d => d.CompanyId);
        }
    }
}
