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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<GroupUserMapping> GroupUserMappings { get; set; }
        public DbSet<StudentLessonsMarks> StudentLessonsMarks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=stud-mssql.sttec.yar.ru,38325;Database=user301_db;User Id=user301_db; Password=user301;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Teacher" },
                new Role { Id = 3, Name = "Student" },
                new Role { Id = 4, Name = "User" }
            );

            modelBuilder.Entity<Mark>().HasData(
                new Mark { Id = 1, MarkText = "1", DecodingMark = "" },
                new Mark { Id = 2, MarkText = "2", DecodingMark = "Не удовлетворительно" },
                new Mark { Id = 3, MarkText = "3", DecodingMark = "Удовлетворительно" },
                new Mark { Id = 4, MarkText = "4", DecodingMark = "Хорошо" },
                new Mark { Id = 5, MarkText = "5", DecodingMark = "Отлично" },
                new Mark { Id = 6, MarkText = "Н", DecodingMark = "Отсутствует" }
            );
        }

    }
}
