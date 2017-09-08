using System;
using System.Collections.Generic;
using System.Text;

namespace CheckListConsole
{
    using Microsoft.EntityFrameworkCore;
    using System.IO;
    using MySql.Data.EntityFrameworkCore;
    using MySQL.Data.EntityFrameworkCore;
    using MySQL.Data.EntityFrameworkCore.Extensions;

    public class LinksDb : DbContext
    {
        public DbSet<LinkCheckResult> Links { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // MSSQL:
            //var connection = @"Server=localhost;Database=Links;User Id=sa;Password=whatever12!";
            //optionsBuilder.UseSqlServer(connection);

            // MySQL (Official):
            //var connection = "server=localhost;userid=root;pwd=password;database=Links;sslmode=none;";
            //optionsBuilder.UseMySQL(connection);
             
            // MySQL (Pomelo):
            //var connection = "server=localhost;userid=root;pwd=password;database=Links;sslmode=none;";
            //optionsBuilder.UseMySql(connection);

            // PostgreSQL (Npgsql):
            var connection = "Host=localhost;Database=Links;Username=postgres;Password=password";
            optionsBuilder.UseNpgsql(connection);

            // SQLite:
            //var databaseLocation = Path.Combine(Directory.GetCurrentDirectory(), "links.db");
            //optionsBuilder.UseSqlite($"Filename={databaseLocation}");

        }
    }
}
