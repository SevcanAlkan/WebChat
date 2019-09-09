using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NGA.Core;
using NGA.Data;
using NGA.Data.Helper;
using NGA.Data.SubStructure;
using NGA.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGA.API.Config
{
    public static class LoadStaticValues
    {
        public static void Load()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();

            StaticValues.DBConnectionString = configuration.GetConnectionString("DefaultConnection");

            ParameterHelperLoder.LoadStaticValues();
        }
    }
}
