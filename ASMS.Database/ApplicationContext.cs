using ASMS.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Database
{
    public class ApplicationContext : DbContext
    {
        private static ApplicationContext? instance;
        public static ApplicationContext Instance
        {
            get => instance ?? (instance = new ApplicationContext());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=stud-mssql.sttec.yar.ru,38325;Database=user301_db;User Id=user301_db; Password=user301;TrustServerCertificate=True");
        }
    }
}
