using Microsoft.Extensions.DependencyInjection;
using NGA.Core.Enum;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGA.Data
{
    public class DbInitializer
    {
        public static void Initialize(NGADbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Parameters.Any(a => a.Code == "SYS01001")) { context.Parameters.Add(new Parameter() { Id = new Guid(), CreateBy = Guid.Empty, CreateDT = DateTime.Now, IsDeleted = false, Name = "LogSystem", Code = "SYS01001", GroupCode = "SYS", OrderIndex = 1, Value = "1" }); }

            if (!context.Groups.Any(a => a.IsMain)) { context.Groups.Add(new Group() { Id = new Guid(), CreateBy = Guid.Empty, CreateDT = DateTime.Now, IsDeleted = false, Name = "Main", Description = "The main chat group of application", IsMain = true, IsPrivate = false }); }

            if (!context.Groups.Any(a => a.Name == "Programming")) { context.Groups.Add(new Group() { Id = new Guid(), CreateBy = Guid.Empty, CreateDT = DateTime.Now, IsDeleted = false, Name = "Programming", Description = "For programmers", IsMain = false, IsPrivate = false }); }

            if (!context.Groups.Any(a => a.Name == "Off Topic")) { context.Groups.Add(new Group() { Id = new Guid(), CreateBy = Guid.Empty, CreateDT = DateTime.Now, IsDeleted = false, Name = "Off Topic", Description = "...", IsMain = false, IsPrivate = false }); }

            if (!context.Groups.Any(a => a.Name == "Gaming")) { context.Groups.Add(new Group() { Id = new Guid(), CreateBy = Guid.Empty, CreateDT = DateTime.Now, IsDeleted = false, Name = "Gaming", Description = "...", IsMain = false, IsPrivate = false }); }

            context.SaveChanges();
        }
    }
}
