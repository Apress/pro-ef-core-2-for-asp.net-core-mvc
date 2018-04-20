using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExistingDb.Models.Scaffold
{
    public partial class ScaffoldContext : DbContext
    {
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Fittings> Fittings { get; set; }
        public virtual DbSet<SalesCampaigns> SalesCampaigns { get; set; }
        public virtual DbSet<ShoeCategoryJunction> ShoeCategoryJunction { get; set; }
        public virtual DbSet<Shoes> Shoes { get; set; }

        public ScaffoldContext(DbContextOptions<ScaffoldContext> options) 
            : base(options) { }


//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ZoomShoesDb");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.Property(e => e.HighlightColor).IsRequired();

                entity.Property(e => e.MainColor).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Fittings>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SalesCampaigns>(entity =>
            {
                entity.HasIndex(e => e.ShoeId)
                    .IsUnique();

                entity.Property(e => e.LaunchDate).HasColumnType("date");

                entity.HasOne(d => d.Shoe)
                    .WithOne(p => p.SalesCampaigns)
                    .HasForeignKey<SalesCampaigns>(d => d.ShoeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SalesCampaigns_Shoes");
            });

            modelBuilder.Entity<ShoeCategoryJunction>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ShoeCategoryJunction)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoeCategoryJunction_Categories");

                entity.HasOne(d => d.Shoe)
                    .WithMany(p => p.ShoeCategoryJunction)
                    .HasForeignKey(d => d.ShoeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoeCategoryJunction_Shoes");
            });

            modelBuilder.Entity<Shoes>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.Shoes)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shoes_Colors");

                entity.HasOne(d => d.Fitting)
                    .WithMany(p => p.Shoes)
                    .HasForeignKey(d => d.FittingId)
                    .HasConstraintName("FK_Shoes_Fittings");
            });
        }
    }
}
