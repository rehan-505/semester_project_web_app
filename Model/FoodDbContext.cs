using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SMS.Models;

namespace semester_project_web_app.Model;

public partial class FoodDbContext : DbContext
{
    public int userno = 0;
    public FoodDbContext()
    {
    }

    public FoodDbContext(DbContextOptions<FoodDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=food_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cartItem__3214EC078396805B");

            entity.ToTable("cartItem");

            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC0734683467");

            entity.ToTable("Product");

            entity.Property(e => e.Des)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("des");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0770B97FF9");

            entity.ToTable("User");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image ");
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pass");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    public override int SaveChanges()
        {
            ProcessSave();
            return base.SaveChanges();
        }
        private void ProcessSave()
        {
            var currentTime = DateTimeOffset.UtcNow;
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is FullAuditModel))
            {
                var entity = entry.Entity as FullAuditModel;
                entity.CreatedDate = DateTime.Now;
                entity.CreatedByUserId = userno.ToString();
                entity.LastModifiedDate = DateTime.Now;
                entity.LastModifiedUserId = userno.ToString();
            }
            foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is FullAuditModel))
            {
                var entity = item.Entity as FullAuditModel;
                entity.LastModifiedDate = DateTime.Now;
                entity.LastModifiedUserId = userno.ToString();
                item.Property(nameof(entity.CreatedDate)).IsModified = false;
                item.Property(nameof(entity.CreatedByUserId)).IsModified = false;
            }
        }
}
