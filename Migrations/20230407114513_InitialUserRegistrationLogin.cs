using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialUserRegistrationLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoorInformation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STARTBYTE = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    IMEI = table.Column<long>(type: "bigint", nullable: false),
                    DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADMISSONLEVEL = table.Column<short>(name: "ADMISSON_LEVEL", type: "smallint", nullable: true),
                    PING = table.Column<short>(type: "smallint", nullable: true),
                    DOORSTATUS = table.Column<short>(name: "DOOR_STATUS", type: "smallint", nullable: true),
                    DOORCLOSE = table.Column<short>(name: "DOOR_CLOSE", type: "smallint", nullable: true),
                    DOOROPEN = table.Column<short>(name: "DOOR_OPEN", type: "smallint", nullable: true),
                    RIGHTDOOR = table.Column<short>(name: "RIGHT_DOOR", type: "smallint", nullable: true),
                    LEFTDOOR = table.Column<short>(name: "LEFT_DOOR", type: "smallint", nullable: true),
                    RECORDINGTIME = table.Column<DateTime>(name: "RECORDING_TIME", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorInformation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DoorInformationUPDATED",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STARTBYTE = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    IMEI = table.Column<long>(type: "bigint", nullable: false),
                    DATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADMISSONLEVEL = table.Column<short>(name: "ADMISSON_LEVEL", type: "smallint", nullable: true),
                    PING = table.Column<short>(type: "smallint", nullable: true),
                    DOORSTATUS = table.Column<short>(name: "DOOR_STATUS", type: "smallint", nullable: true),
                    DOORCLOSE = table.Column<short>(name: "DOOR_CLOSE", type: "smallint", nullable: true),
                    DOOROPEN = table.Column<short>(name: "DOOR_OPEN", type: "smallint", nullable: true),
                    RIGHTDOOR = table.Column<short>(name: "RIGHT_DOOR", type: "smallint", nullable: true),
                    LEFTDOOR = table.Column<short>(name: "LEFT_DOOR", type: "smallint", nullable: true),
                    RECORDINGTIME = table.Column<DateTime>(name: "RECORDING_TIME", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorInformationUPDATED", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Imei = table.Column<long>(type: "bigint", nullable: false),
                    OrderType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InSent = table.Column<short>(type: "smallint", nullable: false),
                    OrderResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    OrderType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoorInformation");

            migrationBuilder.DropTable(
                name: "DoorInformationUPDATED");

            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
