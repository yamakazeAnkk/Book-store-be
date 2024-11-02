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

        public virtual DbSet<Book> Books { get; set; } = null!;
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
        public virtual DbSet<Voucher> Vouchers { get; set; } = null!;
        public virtual DbSet<VoucherUser> VoucherUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=BookStore;User Id=sa;Password=binhanvy1.;Trusted_Connection=False;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.AuthorName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("author_name");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.LinkEbook)
                    .HasMaxLength(1024)
                    .IsUnicode(false)
                    .HasColumnName("link_ebook");

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
                    .HasConstraintName("FK__Book__type_book___534D60F1");
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
                    .HasConstraintName("FK__BookBrand__band___5535A963");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookBrands)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__BookBrand__book___5441852A");
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
                    .HasConstraintName("FK__CartItem__user_i__5AEE82B9");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Order__user_id__5629CD9C");
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
                    .HasConstraintName("FK__OrderItem__book___5812160E");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__order__571DF1D5");
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
                    .HasConstraintName("FK__Purchased__book___5DCAEF64");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PurchasedEbooks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Purchased__user___5CD6CB2B");
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
                    .HasConstraintName("FK__Review__book_id__59FA5E80");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Review__user_id__59063A47");
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

                entity.HasIndex(e => e.Email, "UQ__User__AB6E616412225281")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__User__F3DBC5728539A012")
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
                    .HasConstraintName("FK__User__role_id__52593CB8");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("Voucher");

                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("discount");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("datetime")
                    .HasColumnName("expired_date");

                entity.Property(e => e.MinCost)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("min_cost");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("datetime")
                    .HasColumnName("release_date");

                entity.Property(e => e.VoucherCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("voucher_code");
            });

            modelBuilder.Entity<VoucherUser>(entity =>
            {
                entity.ToTable("VoucherUser");

                entity.Property(e => e.VoucherUserId).HasColumnName("voucher_user_id");

                entity.Property(e => e.IsUsed)
                    .HasColumnName("is_used")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.VoucherId).HasColumnName("voucher_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VoucherUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__VoucherUs__user___5FB337D6");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.VoucherUsers)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK__VoucherUs__vouch__5EBF139D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
