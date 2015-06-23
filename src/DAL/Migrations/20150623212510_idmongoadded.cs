using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace DAL.Migrations
{
    public partial class idmongoadded : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.AddColumn(
                name: "IdMongo",
                table: "Libro",
                type: "nvarchar(max)",
                nullable: true);
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropColumn(name: "IdMongo", table: "Libro");
        }
    }
}
