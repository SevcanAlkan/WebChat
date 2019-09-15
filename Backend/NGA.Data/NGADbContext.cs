using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public class NGADbContext : IdentityDbContext<User,Role, Guid>
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

            modelBuilder.ApplyConfiguration(new GroupMap());
            modelBuilder.ApplyConfiguration(new GroupUserMap());
            modelBuilder.ApplyConfiguration(new MessageMap());
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

        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
    
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogError> LogErrors { get; set; }

        #endregion
    }
}
