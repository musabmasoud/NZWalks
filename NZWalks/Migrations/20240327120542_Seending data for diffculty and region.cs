using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class Seendingdatafordiffcultyandregion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Diffculties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("78ad2ed7-695e-458a-93a4-acf616e7a9e6"), "Easy" },
                    { new Guid("d6cb3e68-ded2-47fe-beb2-116df7c2f63b"), "Medium" },
                    { new Guid("d9a355ae-e2e8-4a28-bd0c-55361063cf7a"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("3ef37571-b117-46c3-a336-70423107e3e2"), "AKL", "Acukland", "Photo by Thirdman from Pexels: https://www.pexels.com/photo/mixing-acrylic-paint-with-a-chisel-6732552/" },
                    { new Guid("4bc0d799-646e-4862-94aa-fa546ae91661"), "STL", "SouthLand", "Photo by Finn Semmer from Pexels: https://www.pexels.com/photo/lonely-20590852/" },
                    { new Guid("8180fd18-496e-4cde-a7bd-68e072cd805d"), "NTL", "NorthLand", "Photo by Nurgül  Kelebek  from Pexels: https://www.pexels.com/photo/white-ceramic-mug-on-white-ceramic-saucer-13708881/" },
                    { new Guid("9e0d1edc-9a01-4c11-9454-b571a52f2280"), "BOP", "Bay Of Plenty", "Photo by igovar igovar from Pexels: https://www.pexels.com/photo/grayscale-photo-of-man-in-polo-shirt-with-birds-5815451/" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Diffculties",
                keyColumn: "Id",
                keyValue: new Guid("78ad2ed7-695e-458a-93a4-acf616e7a9e6"));

            migrationBuilder.DeleteData(
                table: "Diffculties",
                keyColumn: "Id",
                keyValue: new Guid("d6cb3e68-ded2-47fe-beb2-116df7c2f63b"));

            migrationBuilder.DeleteData(
                table: "Diffculties",
                keyColumn: "Id",
                keyValue: new Guid("d9a355ae-e2e8-4a28-bd0c-55361063cf7a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3ef37571-b117-46c3-a336-70423107e3e2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4bc0d799-646e-4862-94aa-fa546ae91661"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8180fd18-496e-4cde-a7bd-68e072cd805d"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("9e0d1edc-9a01-4c11-9454-b571a52f2280"));
        }
    }
}
