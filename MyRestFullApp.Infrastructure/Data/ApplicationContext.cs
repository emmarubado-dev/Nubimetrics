using Microsoft.EntityFrameworkCore;
using MyRestFullApp.Core.Entities;
using MyRestFullApp.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestFullApp.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connectionString;
        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!String.IsNullOrWhiteSpace(_connectionString))
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
