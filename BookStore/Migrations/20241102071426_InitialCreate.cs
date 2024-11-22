using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    brand_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.brand_id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "TypeBook",
                columns: table => new
                {
                    type_book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_book_name = table.Column<string>(type: "varchar(55)", unicode: false, maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeBook", x => x.type_book_id);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    voucher_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voucher_code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    release_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    expired_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    min_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    discount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.voucher_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    profile_image = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: true),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    fullname = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "text", nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                    table.ForeignKey(
                        name: "FK__User__role_id__52593CB8",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    author_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    image = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: true),
                    type_book_id = table.Column<int>(type: "int", nullable: true),
                    upload_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    link_ebook = table.Column<string>(type: "varchar(1024)", unicode: false, maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.book_id);
                    table.ForeignKey(
                        name: "FK__Book__type_book___534D60F1",
                        column: x => x.type_book_id,
                        principalTable: "TypeBook",
                        principalColumn: "type_book_id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    order_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__Order__user_id__5629CD9C",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "VoucherUser",
                columns: table => new
                {
                    voucher_user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voucher_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    is_used = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherUser", x => x.voucher_user_id);
                    table.ForeignKey(
                        name: "FK__VoucherUs__user___5FB337D6",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__VoucherUs__vouch__5EBF139D",
                        column: x => x.voucher_id,
                        principalTable: "Voucher",
                        principalColumn: "voucher_id");
                });

            migrationBuilder.CreateTable(
                name: "BookBrand",
                columns: table => new
                {
                    book_brand_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_id = table.Column<int>(type: "int", nullable: true),
                    band_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBrand", x => x.book_brand_id);
                    table.ForeignKey(
                        name: "FK__BookBrand__band___5535A963",
                        column: x => x.band_id,
                        principalTable: "Brand",
                        principalColumn: "brand_id");
                    table.ForeignKey(
                        name: "FK__BookBrand__book___5441852A",
                        column: x => x.book_id,
                        principalTable: "Book",
                        principalColumn: "book_id");
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    cart_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    book_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.cart_item_id);
                    table.ForeignKey(
                        name: "FK__CartItem__book_i__5BE2A6F2",
                        column: x => x.book_id,
                        principalTable: "Book",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK__CartItem__user_i__5AEE82B9",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "PurchasedEbook",
                columns: table => new
                {
                    purchased_ebook_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    book_id = table.Column<int>(type: "int", nullable: true),
                    purchase_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasedEbook", x => x.purchased_ebook_id);
                    table.ForeignKey(
                        name: "FK__Purchased__book___5DCAEF64",
                        column: x => x.book_id,
                        principalTable: "Book",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK__Purchased__user___5CD6CB2B",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    book_id = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__Review__book_id__59FA5E80",
                        column: x => x.book_id,
                        principalTable: "Book",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK__Review__user_id__59063A47",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    order_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    book_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.order_item_id);
                    table.ForeignKey(
                        name: "FK__OrderItem__book___5812160E",
                        column: x => x.book_id,
                        principalTable: "Book",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK__OrderItem__order__571DF1D5",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_type_book_id",
                table: "Book",
                column: "type_book_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookBrand_band_id",
                table: "BookBrand",
                column: "band_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookBrand_book_id",
                table: "BookBrand",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_book_id",
                table: "CartItem",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_user_id",
                table: "CartItem",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_user_id",
                table: "Order",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_book_id",
                table: "OrderItem",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_order_id",
                table: "OrderItem",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedEbook_book_id",
                table: "PurchasedEbook",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedEbook_user_id",
                table: "PurchasedEbook",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_book_id",
                table: "Review",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Review_user_id",
                table: "Review",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E616412225281",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__User__F3DBC5728539A012",
                table: "User",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUser_user_id",
                table: "VoucherUser",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUser_voucher_id",
                table: "VoucherUser",
                column: "voucher_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookBrand");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "PurchasedEbook");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "VoucherUser");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "TypeBook");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
