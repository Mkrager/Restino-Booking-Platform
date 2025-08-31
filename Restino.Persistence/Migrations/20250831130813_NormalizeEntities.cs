using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restino.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Accommodations_AccommodationsId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Accommodations");

            migrationBuilder.RenameColumn(
                name: "ReservationCreated",
                table: "Reservation",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "AccommodationsId",
                table: "Reservation",
                newName: "AccommodationId");

            migrationBuilder.RenameColumn(
                name: "ReservationId",
                table: "Reservation",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_AccommodationsId",
                table: "Reservation",
                newName: "IX_Reservation_AccommodationId");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AccommodationsId",
                table: "Accommodations",
                newName: "Id");

            migrationBuilder.AlterColumn<decimal>(
                name: "ReservationPrice",
                table: "Reservation",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Reservation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Accommodations",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("0ff2a035-d55d-422c-a7c0-6a3eb4a2b51a"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Local), 1900m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("1a491f3d-94ac-4df8-a244-0ef6c4a5c5b2"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 5000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("3abf0f37-8079-4900-bd6b-8679585a5607"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 3500m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("400c5bff-99c3-4493-a618-6653241bc6b5"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 6000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("47fc830b-751c-4b54-88e3-3281d746f3fd"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { "7678105a-3d95-4c43-a3ac-a717d241a8f1", new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 12000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("48f6d005-6237-465c-a413-e2924e9773e8"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Local), 16000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("4cbafdc3-446b-4ab3-bf2c-2c0b3c744492"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 4000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("6b7e2693-aa97-4f54-97cc-8cc3bdd5d130"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Local), 50000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("70adcfec-32f1-4ae7-a67a-a1690c5ffaa9"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Local), 21000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("826dace9-c0ba-40b2-bd92-41f9d6519a08"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 18, 0, 0, 0, 0, DateTimeKind.Local), 75000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("9015220b-2e8d-4067-a5cb-9a556260e1da"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Local), 2400m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("90bea0a5-4dc5-4461-a6ca-ec2633ce4acf"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Local), 37500m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("965155a6-8b18-45e9-9a60-b92b7876fa69"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 14, 0, 0, 0, 0, DateTimeKind.Local), 2800m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("97b5de46-c8fa-42ff-b752-e1baaf09a844"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Local), 35000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("9db960f8-38e7-4fb4-8db5-6c82f65a654c"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Local), 22000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("9ebd806d-08e7-40d6-9dc8-408c590c119d"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 2000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("a4ab6df6-66b8-46f7-8198-c94332964006"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 24, 0, 0, 0, 0, DateTimeKind.Local), 4500m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("a4e54b1f-3149-4d9b-8d88-e8b4a174a619"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 5500m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("b3b8f3d5-9efe-497a-9454-d2d16965cfa0"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Local), 18000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("c119661c-1d5a-42c1-8819-6b0885af4d4a"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Local), 5000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("cdd387fe-0834-494b-9a63-2a622375d36c"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Local), 50000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("cf0e7c96-b60b-4c6b-8374-57f827fa17a4"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 4500m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("dcbbfc78-9191-4ced-97c6-da3c51b1adae"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 18000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("e3132a60-2e38-4366-b0ac-59d653e3400f"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Local), 55000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("e7136862-7c13-4a57-a05f-e09b9ae98d06"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Local), 50000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("eac0f796-b80d-43ff-800e-dfbe0a5dbb60"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 30000m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("f0aefde0-bc3d-4cfc-b477-00b3ddec847c"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 9, 6, 0, 0, 0, 0, DateTimeKind.Local), 2200m });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: new Guid("ff38ccf6-d319-40d1-8869-c7ae8a415f5e"),
                columns: new[] { "CreatedBy", "CreatedDate", "Price" },
                values: new object[] { null, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Local), 75000m });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("418ade5f-d0b7-4545-b9de-beafbefffa66"),
                columns: new[] { "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6f80d6e0-54b3-495d-847b-16a092c8b626"),
                columns: new[] { "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8f67819c-0d09-43e8-b64f-17c9123b6040"),
                columns: new[] { "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9fbd8bb8-1a85-4305-87fd-4b3d011b1a5a"),
                columns: new[] { "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c119661c-1d5a-42c1-8819-6b0885af4d4a"),
                columns: new[] { "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("cf0976f2-83c1-4708-afea-3e4785db6d66"),
                columns: new[] { "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d1e68fb1-9cd9-4018-9ba5-cfb9bd315b0d"),
                columns: new[] { "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate" },
                values: new object[] { null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "Id",
                keyValue: new Guid("c4046135-7ef7-4a85-a125-feeea203d4de"),
                columns: new[] { "CheckInDate", "CheckOutDate", "CreatedBy", "LastModifiedBy", "LastModifiedDate", "ReservationPrice" },
                values: new object[] { new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 9, 7, 0, 0, 0, 0, DateTimeKind.Local), "TestFirstUserId", null, null, 0m });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Accommodations_AccommodationId",
                table: "Reservation",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Accommodations_AccommodationId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Accommodations");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Reservation",
                newName: "ReservationCreated");

            migrationBuilder.RenameColumn(
                name: "AccommodationId",
                table: "Reservation",
                newName: "AccommodationsId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reservation",
                newName: "ReservationId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_AccommodationId",
                table: "Reservation",
                newName: "IX_Reservation_AccommodationsId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoriesId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Accommodations",
                newName: "AccommodationsId");

            migrationBuilder.AlterColumn<double>(
                name: "ReservationPrice",
                table: "Reservation",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Accommodations",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Accommodations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("0ff2a035-d55d-422c-a7c0-6a3eb4a2b51a"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Local), 1900, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("1a491f3d-94ac-4df8-a244-0ef6c4a5c5b2"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 5000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("3abf0f37-8079-4900-bd6b-8679585a5607"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 3500, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("400c5bff-99c3-4493-a618-6653241bc6b5"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 6000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("47fc830b-751c-4b54-88e3-3281d746f3fd"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 12000, "7678105a-3d95-4c43-a3ac-a717d241a8f1" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("48f6d005-6237-465c-a413-e2924e9773e8"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), 16000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("4cbafdc3-446b-4ab3-bf2c-2c0b3c744492"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 4000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("6b7e2693-aa97-4f54-97cc-8cc3bdd5d130"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), 50000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("70adcfec-32f1-4ae7-a67a-a1690c5ffaa9"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Local), 21000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("826dace9-c0ba-40b2-bd92-41f9d6519a08"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Local), 75000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("9015220b-2e8d-4067-a5cb-9a556260e1da"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), 2400, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("90bea0a5-4dc5-4461-a6ca-ec2633ce4acf"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Local), 37500, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("965155a6-8b18-45e9-9a60-b92b7876fa69"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Local), 2800, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("97b5de46-c8fa-42ff-b752-e1baaf09a844"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Local), 35000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("9db960f8-38e7-4fb4-8db5-6c82f65a654c"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Local), 22000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("9ebd806d-08e7-40d6-9dc8-408c590c119d"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 2000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("a4ab6df6-66b8-46f7-8198-c94332964006"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Local), 4500, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("a4e54b1f-3149-4d9b-8d88-e8b4a174a619"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 5500, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("b3b8f3d5-9efe-497a-9454-d2d16965cfa0"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Local), 18000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("c119661c-1d5a-42c1-8819-6b0885af4d4a"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Local), 5000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("cdd387fe-0834-494b-9a63-2a622375d36c"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Local), 50000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("cf0e7c96-b60b-4c6b-8374-57f827fa17a4"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 4500, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("dcbbfc78-9191-4ced-97c6-da3c51b1adae"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 18000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("e3132a60-2e38-4366-b0ac-59d653e3400f"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Local), 55000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("e7136862-7c13-4a57-a05f-e09b9ae98d06"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 21, 0, 0, 0, 0, DateTimeKind.Local), 50000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("eac0f796-b80d-43ff-800e-dfbe0a5dbb60"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 30000, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("f0aefde0-bc3d-4cfc-b477-00b3ddec847c"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Local), 2200, "" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "AccommodationsId",
                keyValue: new Guid("ff38ccf6-d319-40d1-8869-c7ae8a415f5e"),
                columns: new[] { "CreatedDate", "Price", "UserId" },
                values: new object[] { new DateTime(2025, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), 75000, "" });

            migrationBuilder.UpdateData(
                table: "Reservation",
                keyColumn: "ReservationId",
                keyValue: new Guid("c4046135-7ef7-4a85-a125-feeea203d4de"),
                columns: new[] { "CheckInDate", "CheckOutDate", "ReservationPrice", "UserId" },
                values: new object[] { new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Local), 0.0, "TestFirstUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Accommodations_AccommodationsId",
                table: "Reservation",
                column: "AccommodationsId",
                principalTable: "Accommodations",
                principalColumn: "AccommodationsId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
