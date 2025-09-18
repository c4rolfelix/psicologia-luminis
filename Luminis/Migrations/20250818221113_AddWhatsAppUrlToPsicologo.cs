using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luminis.Migrations
{
    /// <inheritdoc />
    public partial class AddWhatsAppUrlToPsicologo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WhatsAppUrl",
                table: "Psicologos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18c66e9-0260-466d-a789-21800f123456",
                column: "ConcurrencyStamp",
                value: "8e711fe9-480e-4893-9564-072a43a572a7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b18c66e9-0260-466d-a789-21800f123457",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c159c113-d894-4fc8-b32a-53dd356ea7f7", "AQAAAAIAAYagAAAAEPDRf03sCt7O3c3m+avA8Y3tYqNO+KRuTunbvRfkdJH5hjr9EDUe+FI7JSV3WaRIug==", "ccd1b262-c94f-42e9-b43b-6305951a33c0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c18c66e9-0260-466d-a789-21800f123458",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "207d4858-1ebb-4d7b-a8be-a18029fb8b36", "AQAAAAIAAYagAAAAEOCDSozQ10SK8Q2P3IiDA1pYihTEMOV5MlmO//Pj3ay33Yt2NGQWAncIqxuRT5vptg==", "7136cebd-521e-48f8-acac-31ce4e82188e" });

            migrationBuilder.UpdateData(
                table: "Psicologos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "WhatsAppUrl" },
                values: new object[] { new DateTime(2025, 8, 18, 19, 11, 13, 228, DateTimeKind.Local).AddTicks(1389), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhatsAppUrl",
                table: "Psicologos");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18c66e9-0260-466d-a789-21800f123456",
                column: "ConcurrencyStamp",
                value: "371b6373-e235-4602-b8cd-e01f4f1cf67d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b18c66e9-0260-466d-a789-21800f123457",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "df557392-d000-40bd-a107-2330993ea892", "AQAAAAIAAYagAAAAEOKwVncl2bJtF5X6988F7ORxqqkg19qr99Up9TvP+QfVva/rkGElS+8pqB2qzpEz6w==", "890ceba7-f780-420f-8b29-535e8e37a36a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c18c66e9-0260-466d-a789-21800f123458",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c805f7a5-17a2-4eb3-a707-be468e4478c5", "AQAAAAIAAYagAAAAEJWvI7iuc4cFakfqX0plZVLks5wRvUkiRzbBZjnQ5tJNR06WLF6ceOjL4jItZH1SLw==", "11e152a7-0700-4b34-be07-7751b3bcdae4" });

            migrationBuilder.UpdateData(
                table: "Psicologos",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataCadastro",
                value: new DateTime(2025, 8, 18, 18, 31, 31, 383, DateTimeKind.Local).AddTicks(9293));
        }
    }
}
