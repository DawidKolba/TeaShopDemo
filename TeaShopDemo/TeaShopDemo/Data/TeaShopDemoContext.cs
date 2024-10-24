using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeaShopDemo.Models;

public class TeaShopDemoContext : IdentityDbContext<IdentityUser>
{
    public TeaShopDemoContext(DbContextOptions<TeaShopDemoContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
   // public DbSet<CartItem> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
     
        builder.Entity<Product>()
            .ToTable("Products")
            .HasKey(p => p.ProductId);

        //builder.Entity<CartItem>()
        //    .ToTable("CartItems")
        //    .HasKey(ci => ci.ProductId);
    }
}
