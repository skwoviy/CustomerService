using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DbContext.Models
{
    public partial class CustomerServiceContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CustomerServiceContext()
        {
        }

        public CustomerServiceContext(DbContextOptions<CustomerServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=CustomerService;User ID=test;Password=test;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.MobileNo).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Currency");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Customer");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaction_Status");
            });
        }
    }
}
