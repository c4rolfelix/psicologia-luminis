using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Luminis.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetupCompletoFinalV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Biografia",
                table: "Psicologos",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Psicologos",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a18c66e9-0260-466d-a789-21800f123456",
                column: "ConcurrencyStamp",
                value: "9c32b6d7-b90b-490a-8728-e26e551407f4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b18c66e9-0260-466d-a789-21800f123457",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "418d9b3e-9c2c-40a0-9867-acaf3fcf8c6d", "AQAAAAIAAYagAAAAEPlZQPVirtX6rrTvpjOvzlpfWFDA1HQAlJrfzrNpKeq2w1zPy1zzQrYc7TpMM+52rQ==", "668e6936-3d19-4e48-869f-9778b867ffbf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c18c66e9-0260-466d-a789-21800f123458",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b7687da-ac63-4ba8-82d5-92088355780b", "AQAAAAIAAYagAAAAEHw1LJ0uelZ2db8eyBE+jdcLGQvjjo0O7ahJaiawEPtY09CCsSsm6kHwum0PNbrzHg==", "db30cb6b-8ed1-4cce-9d82-8239c55442a4" });

            migrationBuilder.UpdateData(
                table: "Psicologos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CPF", "DataCadastro", "WhatsApp", "WhatsAppUrl" },
                values: new object[] { "000.000.000-00", new DateTime(2025, 9, 17, 20, 41, 44, 710, DateTimeKind.Local).AddTicks(1608), "5599999999999", "https://wa.me/5599999999999" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Psicologos");

            migrationBuilder.AlterColumn<string>(
                name: "Biografia",
                table: "Psicologos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(700)",
                oldMaxLength: 700,
                oldNullable: true);

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
                columns: new[] { "DataCadastro", "WhatsApp", "WhatsAppUrl" },
                values: new object[] { new DateTime(2025, 8, 18, 19, 11, 13, 228, DateTimeKind.Local).AddTicks(1389), "(99) 99999-9999", null });
        }
    }
}
