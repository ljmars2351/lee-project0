using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OrderSystem.DataAccess
{
    public partial class Proj0Context : DbContext
    {
        public Proj0Context()
        {
        }

        public Proj0Context(DbContextOptions<Proj0Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Bundle> Bundle { get; set; }
        public virtual DbSet<BundleProds> BundleProds { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<ProdHist> ProdHist { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Bundle>(entity =>
            {
                entity.ToTable("Bundle", "OrdSys");

                entity.Property(e => e.BundleName).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<BundleProds>(entity =>
            {
                entity.HasKey(e => new { e.BundleId, e.ProductId })
                    .HasName("PK_bundle");

                entity.ToTable("BundleProds", "OrdSys");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.CustId, e.LocId, e.ProdId })
                    .HasName("PK_cart_comp");

                entity.ToTable("Cart", "OrdSys");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.CustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__CustId__04E4BC85");

                entity.HasOne(d => d.Loc)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.LocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__LocId__06CD04F7");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.ProdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__ProdId__05D8E0BE");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "OrdSys");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.HasOne(d => d.PrefLocNavigation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.PrefLoc)
                    .HasConstraintName("FK__Customer__PrefLo__5CD6CB2B");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => new { e.ProdId, e.LocationId })
                    .HasName("PK_comp");

                entity.ToTable("Inventory", "OrdSys");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocID");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ProdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__ProdI__4E88ABD4");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "OrdSys");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ProdHist>(entity =>
            {
                entity.ToTable("ProdHist", "OrdSys");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "OrdSys");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");
            });
        }
    }
}
