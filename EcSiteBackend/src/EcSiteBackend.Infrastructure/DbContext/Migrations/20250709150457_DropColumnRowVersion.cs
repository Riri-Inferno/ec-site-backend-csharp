using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcSiteBackend.Infrastructure.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class DropColumnRowVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "users");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "user_coupons");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "user_addresses");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "tax_rates");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "system_settings");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "stocks");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "shippings");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "shipping_methods");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "products");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "product_images");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "payment_methods");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "password_reset_tokens");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "order_status_histories");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "login_histories");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "favorites");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "coupons");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "cart_items");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "users",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "user_coupons",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "user_addresses",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "tax_rates",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "system_settings",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "stocks",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "shippings",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "shipping_methods",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "roles",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "reviews",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "products",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "product_images",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "payments",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "payment_methods",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "password_reset_tokens",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "orders",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "order_status_histories",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "order_items",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "login_histories",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "favorites",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "coupons",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "categories",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "carts",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "cart_items",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                table: "users");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "user_coupons");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "user_addresses");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "tax_rates");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "system_settings");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "stocks");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "shippings");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "shipping_methods");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "products");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "product_images");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "payment_methods");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "password_reset_tokens");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "order_status_histories");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "login_histories");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "favorites");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "coupons");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "cart_items");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "user_coupons",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "user_addresses",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "tax_rates",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "system_settings",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "stocks",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "shippings",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "shipping_methods",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "roles",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "reviews",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "products",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "product_images",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "payments",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "payment_methods",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "password_reset_tokens",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "orders",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "order_status_histories",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "order_items",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "login_histories",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "favorites",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "coupons",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "categories",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "carts",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "cart_items",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
