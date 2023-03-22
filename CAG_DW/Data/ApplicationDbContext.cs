using CAG_DW.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CAG_DW.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer>? Customers { get; set; }

    public DbSet<Agreement>? Agreements { get; set; }

    public DbSet<Product>? Products { get; set; }

    public DbSet<AgreementDetail>? agreementDetails { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer(@"Server=localhost,1433; Database=CAGDW;User=sa; Password=SPa$$word!;TrustServerCertificate=True");
        optionsBuilder.UseSqlServer(@"Server=tcp:cagdbsvr.database.windows.net,1433;Initial Catalog=cagdb;Persist Security Info=False;User ID=cagadmin;Password=SommarISverige123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);

    //    builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "user", NormalizedName = "USER", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
    //    builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "admin", NormalizedName = "ADMIN", Id = Guid.NewGuid().ToString(), ConcurrencyStamp = Guid.NewGuid().ToString() });
    //}
}