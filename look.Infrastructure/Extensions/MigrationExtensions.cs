using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data.Common;
using look.Infrastructure.data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
namespace look.Infrastructure.Extensions
{
    public static class MigrationExtensions
    {
        public static void CreateTableIfNotExists<TColumns>(
             this MigrationBuilder migrationBuilder,
             string name,
             Func<ColumnsBuilder, TColumns> columns,
             Action<CreateTableBuilder<TColumns>> constraints)
        {
            try
            {
                migrationBuilder.CreateTable(
                    name: name,
                    columns: columns,
                    constraints: constraints)
                    .Annotation("MySql:CharSet", "utf8mb4");
            }
            catch (Exception ex) when (ex.Message.Contains($"Table '{name}' already exists"))
            {
                // La tabla ya existe, no hacemos nada
            }
        }

    }
}
