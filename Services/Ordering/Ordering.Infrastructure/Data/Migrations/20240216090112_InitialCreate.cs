﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false, defaultValue: "Draft"),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    BillingAddress_AddressLine = table.Column<string>(type: "varchar(180)", maxLength: 180, nullable: false),
                    BillingAddress_Country = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BillingAddress_EmailAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    BillingAddress_FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BillingAddress_LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BillingAddress_State = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BillingAddress_ZipCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    OrderName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Payment_CVV = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Payment_CardName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Payment_CardNumber = table.Column<string>(type: "varchar(24)", maxLength: 24, nullable: false),
                    Payment_Expiration = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Payment_PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    ShippingAddress_AddressLine = table.Column<string>(type: "varchar(180)", maxLength: 180, nullable: false),
                    ShippingAddress_Country = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ShippingAddress_EmailAddress = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ShippingAddress_FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ShippingAddress_LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ShippingAddress_State = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ShippingAddress_ZipCode = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
