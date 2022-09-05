using Billing_Api.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Billing_Api.Core.Database
{
    public class BillingApiDbContext : DbContext
    {
        public BillingApiDbContext(DbContextOptions<BillingApiDbContext> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentGateway> PaymentGateways { get; set; }
        public DbSet<User> Users { get; set; }
    }
}