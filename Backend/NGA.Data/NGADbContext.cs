using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using NGA.Core.EntityFramework;
using NGA.Data.Mapping;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGA.Data
{
    public class NGADbContext : DbContext
    {
        public NGADbContext() : base()
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException("modelBuilder");            

            modelBuilder.ApplyConfiguration(new AnimalMap());
            modelBuilder.ApplyConfiguration(new AnimalTypeMap());
            modelBuilder.ApplyConfiguration(new NestMap());
            modelBuilder.ApplyConfiguration(new NestAnimalMap());
            modelBuilder.ApplyConfiguration(new ParameterMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }

        public virtual void Commit()
        {
            try
            {
                base.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw dbEx;
            }
        }

        #region Tables

        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<Nest> Nests { get; set; }
        public DbSet<NestAnimal> NestAnimals { get; set; }
    
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogError> LogErrors { get; set; }

        public DbSet<User> Users { get; set; }

        #endregion
    }
}
