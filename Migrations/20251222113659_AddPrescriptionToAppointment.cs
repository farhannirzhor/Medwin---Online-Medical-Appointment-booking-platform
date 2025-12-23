using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicalAppointmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPrescriptionToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrescriptionFilePath",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrescriptionFilePath",
                table: "Appointments");
        }
    }
}
