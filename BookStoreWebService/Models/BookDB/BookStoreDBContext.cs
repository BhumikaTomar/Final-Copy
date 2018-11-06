using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStoreWebService.Models.BookDB
{
    public partial class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext()
        {
        }

        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<ReorderTransaction> ReorderTransaction { get; set; }
        public virtual DbSet<SubCategory> SubCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=localhost;database=BookStoreDB;trusted_connection=yes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookTitle)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Book)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("BookFK");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryTitle)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.BookId });

                entity.Property(e => e.OrderPrice).HasColumnType("money");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderDetails2FK");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderAmount).HasColumnType("money");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrderFK");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.InvoiceNumber);

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Orders");
            });

            modelBuilder.Entity<ReorderTransaction>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.BookId });

                entity.ToTable("Reorder Transaction");

                entity.Property(e => e.RequestId).ValueGeneratedOnAdd();

                entity.Property(e => e.ProductOrderAmount).HasColumnType("money");

                entity.Property(e => e.ProductOrderDate).HasColumnType("date");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ReorderTransaction)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Book");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => new { e.SubCategoryId, e.CategoryId });

                entity.Property(e => e.SubCategoryId).ValueGeneratedOnAdd();

                entity.Property(e => e.SubCategoryTitle)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubCategorysFK");
            });
        }
    }
}
