using ID_model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ID_repository.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<UserModel> Users { get; set; }

    }
}
