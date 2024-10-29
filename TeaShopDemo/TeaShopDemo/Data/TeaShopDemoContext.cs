using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeaShopDemo.Models;
using TeaShopDemo.Models.TeaShopDemo.Models;

public class TeaShopDemoContext : IdentityDbContext<IdentityUser>
{
    public TeaShopDemoContext(DbContextOptions<TeaShopDemoContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
     
        builder.Entity<Product>()
            .ToTable("Products")
            .HasKey(p => p.ProductId);
    }
}
