using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyRestFullApp.Infrastructure.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 200, nullable: false),
                    Apellido = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    Roles = table.Column<string>(nullable: true),
                    RefreshTokenExpiredTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UserName);
                });

            migrationBuilder.Sql(@"INSERT[dbo].[Usuario] ([UserName], [Nombre], [Apellido], [Password], [RefreshToken], [Roles], [RefreshTokenExpiredTime]) VALUES(N'erubado', N'Emmanuel', N'Rubado', N'123', N'V0fk/3D9SXyUUBxh6EhRHAK9kjbtV8jlylut+2o4exg=', N'1', CAST(N'2022-02-23T06:26:52.6768186' AS DateTime2))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }


    }
}
