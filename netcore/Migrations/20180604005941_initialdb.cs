using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace netcore.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ApplicationUserRole = table.Column<bool>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CurrencyRole = table.Column<bool>(nullable: false),
                    CustomerInvoiceLineRole = table.Column<bool>(nullable: false),
                    CustomerInvoiceRole = table.Column<bool>(nullable: false),
                    CustomerRole = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    HomeRole = table.Column<bool>(nullable: false),
                    ItemRole = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    MyCompanyRole = table.Column<bool>(nullable: false),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TaxRole = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    VendorInvoiceLineRole = table.Column<bool>(nullable: false),
                    VendorInvoiceRole = table.Column<bool>(nullable: false),
                    VendorRole = table.Column<bool>(nullable: false),
                    isSuperAdmin = table.Column<bool>(nullable: false),
                    profilePictureUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    currencyId = table.Column<string>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    currencyCode = table.Column<string>(nullable: false),
                    currencyName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.currencyId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customerId = table.Column<string>(nullable: false),
                    additionalInformation = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: false),
                    contactName = table.Column<string>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    customerName = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    fax = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: false),
                    taxRegisteredNumber = table.Column<string>(nullable: false),
                    website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.customerId);
                });

            migrationBuilder.CreateTable(
                name: "MyCompany",
                columns: table => new
                {
                    myCompanyId = table.Column<string>(nullable: false),
                    additionalInformation = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: false),
                    companyName = table.Column<string>(nullable: false),
                    contactName = table.Column<string>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    fax = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: false),
                    taxRegisteredNumber = table.Column<string>(nullable: false),
                    website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCompany", x => x.myCompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    taxId = table.Column<string>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    taxLabel = table.Column<string>(nullable: false),
                    taxRate = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.taxId);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    vendorId = table.Column<string>(nullable: false),
                    additionalInformation = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: false),
                    contactName = table.Column<string>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    fax = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: false),
                    taxRegisteredNumber = table.Column<string>(nullable: false),
                    vendorName = table.Column<string>(nullable: false),
                    website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.vendorId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInvoice",
                columns: table => new
                {
                    customerInvoiceId = table.Column<string>(nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    customerId = table.Column<string>(nullable: false),
                    discount = table.Column<decimal>(nullable: false),
                    dueDate = table.Column<DateTime>(nullable: false),
                    grandTotal = table.Column<decimal>(nullable: false),
                    invoiceDate = table.Column<DateTime>(nullable: false),
                    invoiceNumber = table.Column<string>(nullable: false),
                    invoiceReference = table.Column<string>(nullable: true),
                    isPaid = table.Column<bool>(nullable: false),
                    myCompanyId = table.Column<string>(nullable: false),
                    noteToRecipient = table.Column<string>(nullable: true),
                    shipping = table.Column<decimal>(nullable: false),
                    subTotal = table.Column<decimal>(nullable: false),
                    taxAmount = table.Column<decimal>(nullable: false),
                    termsAndCondition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInvoice", x => x.customerInvoiceId);
                    table.ForeignKey(
                        name: "FK_CustomerInvoice_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerInvoice_MyCompany_myCompanyId",
                        column: x => x.myCompanyId,
                        principalTable: "MyCompany",
                        principalColumn: "myCompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    itemId = table.Column<string>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    currencyId = table.Column<string>(nullable: false),
                    itemDescription = table.Column<string>(nullable: false),
                    itemName = table.Column<string>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    taxId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.itemId);
                    table.ForeignKey(
                        name: "FK_Item_Currency_currencyId",
                        column: x => x.currencyId,
                        principalTable: "Currency",
                        principalColumn: "currencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Tax_taxId",
                        column: x => x.taxId,
                        principalTable: "Tax",
                        principalColumn: "taxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorInvoice",
                columns: table => new
                {
                    vendorInvoiceId = table.Column<string>(nullable: false),
                    HasChild = table.Column<string>(nullable: true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    discount = table.Column<decimal>(nullable: false),
                    dueDate = table.Column<DateTime>(nullable: false),
                    grandTotal = table.Column<decimal>(nullable: false),
                    invoiceDate = table.Column<DateTime>(nullable: false),
                    invoiceNumber = table.Column<string>(nullable: false),
                    invoiceReference = table.Column<string>(nullable: true),
                    isPaid = table.Column<bool>(nullable: false),
                    myCompanyId = table.Column<string>(nullable: false),
                    note = table.Column<string>(nullable: true),
                    originalInvoiceNumber = table.Column<string>(nullable: false),
                    shipping = table.Column<decimal>(nullable: false),
                    subTotal = table.Column<decimal>(nullable: false),
                    taxAmount = table.Column<decimal>(nullable: false),
                    termsAndCondition = table.Column<string>(nullable: true),
                    vendorId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorInvoice", x => x.vendorInvoiceId);
                    table.ForeignKey(
                        name: "FK_VendorInvoice_MyCompany_myCompanyId",
                        column: x => x.myCompanyId,
                        principalTable: "MyCompany",
                        principalColumn: "myCompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorInvoice_Vendor_vendorId",
                        column: x => x.vendorId,
                        principalTable: "Vendor",
                        principalColumn: "vendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInvoiceLine",
                columns: table => new
                {
                    customerInvoiceLineId = table.Column<string>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    customerInvoiceId = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    itemId = table.Column<string>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    quantity = table.Column<float>(nullable: false),
                    taxAmount = table.Column<decimal>(nullable: false),
                    taxId = table.Column<string>(nullable: false),
                    totalAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInvoiceLine", x => x.customerInvoiceLineId);
                    table.ForeignKey(
                        name: "FK_CustomerInvoiceLine_CustomerInvoice_customerInvoiceId",
                        column: x => x.customerInvoiceId,
                        principalTable: "CustomerInvoice",
                        principalColumn: "customerInvoiceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerInvoiceLine_Item_itemId",
                        column: x => x.itemId,
                        principalTable: "Item",
                        principalColumn: "itemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorInvoiceLine",
                columns: table => new
                {
                    vendorInvoiceLineId = table.Column<string>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    itemId = table.Column<string>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    quantity = table.Column<float>(nullable: false),
                    taxAmount = table.Column<decimal>(nullable: false),
                    taxId = table.Column<string>(nullable: false),
                    totalAmount = table.Column<decimal>(nullable: false),
                    vendorInvoiceId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorInvoiceLine", x => x.vendorInvoiceLineId);
                    table.ForeignKey(
                        name: "FK_VendorInvoiceLine_Item_itemId",
                        column: x => x.itemId,
                        principalTable: "Item",
                        principalColumn: "itemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VendorInvoiceLine_VendorInvoice_vendorInvoiceId",
                        column: x => x.vendorInvoiceId,
                        principalTable: "VendorInvoice",
                        principalColumn: "vendorInvoiceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInvoice_customerId",
                table: "CustomerInvoice",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInvoice_myCompanyId",
                table: "CustomerInvoice",
                column: "myCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInvoiceLine_customerInvoiceId",
                table: "CustomerInvoiceLine",
                column: "customerInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInvoiceLine_itemId",
                table: "CustomerInvoiceLine",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_currencyId",
                table: "Item",
                column: "currencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_taxId",
                table: "Item",
                column: "taxId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_myCompanyId",
                table: "VendorInvoice",
                column: "myCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoice_vendorId",
                table: "VendorInvoice",
                column: "vendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_itemId",
                table: "VendorInvoiceLine",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInvoiceLine_vendorInvoiceId",
                table: "VendorInvoiceLine",
                column: "vendorInvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CustomerInvoiceLine");

            migrationBuilder.DropTable(
                name: "VendorInvoiceLine");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CustomerInvoice");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "VendorInvoice");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "MyCompany");

            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
