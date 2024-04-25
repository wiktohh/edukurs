using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Repositories_RepositoryId1",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_RepositoryId1",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RepositoryId1",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "SubmittedReports",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_RepositoryId",
                table: "Tasks",
                column: "RepositoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Repositories_RepositoryId",
                table: "Tasks",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Repositories_RepositoryId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_RepositoryId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SubmittedReports",
                newName: "Guid");

            migrationBuilder.AddColumn<Guid>(
                name: "RepositoryId1",
                table: "Tasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_RepositoryId1",
                table: "Tasks",
                column: "RepositoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Repositories_RepositoryId1",
                table: "Tasks",
                column: "RepositoryId1",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
