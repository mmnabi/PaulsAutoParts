using Microsoft.EntityFrameworkCore;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.DataLayer
{
  public partial class PaulsAutoPartsDbContext : DbContext
  {
    public PaulsAutoPartsDbContext(
      DbContextOptions<PaulsAutoPartsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerPayment> CustomerPayments { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<OrderHeader> OrderHeaders { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<PromoCode> PromoCodes { get; set; }
    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnModelCreating(
      ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}
