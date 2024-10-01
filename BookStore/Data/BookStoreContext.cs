using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStore.Models
{
    public partial class BookStoreContext : DbContext
    {
        public BookStoreContext()
        {
        }

        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BookAuthor> BookAuthors { get; set; } = null!;
        public virtual DbSet<BookBrand> BookBrands { get; set; } = null!;
        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<PurchasedEbook> PurchasedEbooks { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<TypeBook> TypeBooks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=localhost;Database=BookStore;User Id=sa;Password=binhanvy1.;Trusted_Connection=False;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("dateOfBirth");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("rating");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.TypeBookId).HasColumnName("type_book_id");

                entity.Property(e => e.UploadDate)
                    .HasColumnType("datetime")
                    .HasColumnName("upload_date");

                entity.HasOne(d => d.TypeBook)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.TypeBookId)
                    .HasConstraintName("FK__Book__type_book___5441852A");
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.ToTable("BookAuthor");

                entity.Property(e => e.BookAuthorId).HasColumnName("book_author_id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__BookAutho__autho__534D60F1");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookAuthors)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BookAutho__book___52593CB8");
            });

            modelBuilder.Entity<BookBrand>(entity =>
            {
                entity.ToTable("BookBrand");

                entity.Property(e => e.BookBrandId).HasColumnName("book_brand_id");

                entity.Property(e => e.BandId).HasColumnName("band_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.HasOne(d => d.Band)
                    .WithMany(p => p.BookBrands)
                    .HasForeignKey(d => d.BandId)
                    .HasConstraintName("FK__BookBrand__band___5629CD9C");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookBrands)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BookBrand__book___5535A963");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("CartItem");

                entity.Property(e => e.CartItemId).HasColumnName("cart_item_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__CartItem__book_i__5BE2A6F2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__CartItem__user_i__5CD6CB2B");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Address)
                    .HasColumnType("text")
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_amount");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Order__user_id__571DF1D5");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__OrderItem__book___59063A47");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__order__5812160E");
            });

            modelBuilder.Entity<PurchasedEbook>(entity =>
            {
                entity.ToTable("PurchasedEbook");

                entity.Property(e => e.PurchasedEbookId).HasColumnName("purchased_ebook_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("purchase_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.PurchasedEbooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Purchased__book___5EBF139D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PurchasedEbooks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Purchased__user___5DCAEF64");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("review_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasColumnName("comment");

                entity.Property(e => e.Rating)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("rating");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Review__book_id__5AEE82B9");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Review__user_id__59FA5E80");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<TypeBook>(entity =>
            {
                entity.ToTable("TypeBook");

                entity.Property(e => e.TypeBookId).HasColumnName("type_book_id");

                entity.Property(e => e.TypeBookName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("type_book_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ__User__AB6E6164B0682FD6")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__User__F3DBC572FC6C9CFE")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasColumnType("text")
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("fullname");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.ProfileImage)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("profile_image");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__User__role_id__5165187F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
