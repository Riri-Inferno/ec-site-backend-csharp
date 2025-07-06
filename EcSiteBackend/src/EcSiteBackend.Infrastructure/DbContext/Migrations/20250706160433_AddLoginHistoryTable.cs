using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcSiteBackend.Infrastructure.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class AddLoginHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "login_histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    AttemptedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false),
                    FailureReason = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DeviceInfo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Browser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_login_histories_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_login_histories_AttemptedAt",
                table: "login_histories",
                column: "AttemptedAt");

            migrationBuilder.CreateIndex(
                name: "IX_login_histories_Email",
                table: "login_histories",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_login_histories_IsSuccess",
                table: "login_histories",
                column: "IsSuccess");

            migrationBuilder.CreateIndex(
                name: "IX_login_histories_UserId",
                table: "login_histories",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "login_histories");
        }
    }
}
