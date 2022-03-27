using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using prmToolkit.NotificationPattern;
using TesteCitel.Domain.Entities;

#nullable disable

namespace TesteCitel.Infra
{
    public partial class bd_citelContext : DbContext
    {
        private static string ConnectionString;
        public bd_citelContext()
        {
        }
        public bd_citelContext(DbContextOptions<bd_citelContext> options) 
            : base(options)
        {
        }
        public bd_citelContext(DbContextOptions<bd_citelContext> options, IConfiguration configuration) 
            : base(options)
        {
            ConnectionString = configuration["ConnectionStrings:DefaultConnection"];
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConnectionString, ServerVersion.Parse("8.0.28-mysql"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");
            modelBuilder.Ignore<Notification>();
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.CategoryId, "fk_product_category_idx");

                entity.Property(e => e.Id)
                    .HasMaxLength(450)
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("category_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasPrecision(10, 2)
                    .HasColumnName("price");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_category");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
