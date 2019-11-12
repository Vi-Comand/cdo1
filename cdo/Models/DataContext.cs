using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace cdo.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }
        public DataContext(DbContextOptionsBuilder optionsBuilder)
        {
            OnConfiguring(optionsBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            optionsBuilder.UseMySql(configuration["ConnectionStrings:DefaultConnection"]);

        }

        public DbSet<user> User { get; set; }
        public DbSet<Mo> Mo { get; set; }
        public DbSet<uo> Uo { get; set; }
        public DbSet<main> Main { get; set; }
        public DbSet<ist> Ist { get; set; }
        public DbSet<to> To { get; set; }
        public DbSet<rem> Rem { get; set; }
        public DbSet<inter> Inter { get; set; }
        public DbSet<kurs> Kurs { get; set; }
        public DbSet<bvp> Bvp { get; set; }
        public DbSet<dop_dog> Dop_Dog { get; set; }
        public DbSet<auth_date> auth_date { get; set; }
        public DbSet<sklad_to> Sklad_to { get; set; }
        public DbSet<ist_kompl> Ist_Kompl { get; set; }
        public CompositeModel Composite { get; set; }

    }
}
