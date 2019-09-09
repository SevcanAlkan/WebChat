using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using NGA.API.Config;
using NGA.API.Filter;
using NGA.API.Middleware;
using NGA.API.SignalR;
using NGA.Core;
using NGA.Core.EntityFramework;
using NGA.Core.Model;
using NGA.Data;
using NGA.Data.Service;
using NGA.Data.SubStructure;
using NGA.Data.ViewModel;
using NGA.Domain;
using StackExchange.Redis;

namespace NGA.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            StaticValues.HostAddress = (IPAddress.Loopback.ToString() + ":" + Configuration.GetValue<int>("Host:Port")).ToString();
            StaticValues.HostSSLAddress = (IPAddress.Loopback.ToString() + ":" + Configuration.GetValue<int>("Host:PortSSL")).ToString();
            LoadStaticValues.Load();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                 ));

            #region MVC Configration
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidatorActionFilter));//MVC kendisi attributelara gore zaten validation yapiyor. 
                options.Filters.Add(typeof(LoggerFilter));
            }).AddJsonOptions(options =>
            {
                var settings = options.SerializerSettings;
                options.SerializerSettings.Converters.Add(new StringEnumConverter
                {
                    CamelCaseText = true
                });
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #endregion

            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddAutoMapper(typeof(Startup).Assembly);
            #endregion

            #region Dependency Injection 

            services.AddSingleton(mapper);
            services.AddDbContext<NGADbContext>(ServiceLifetime.Transient);
            services.AddSingleton<UnitOfWork>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

            services.AddSingleton<IParameterService, ParameterService>();
            services.AddSingleton<IGroupService, GroupService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddScoped(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));

            services.AddSingleton<ChatHub>();

            #endregion

            services.AddSignalR();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<AuthMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseHttpsRedirection(); //for diseable SSL

            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });

            app.UseMvc(options =>
            {
                options.MapRoute(name: "DefaultApi",
                template: "api/{controller}/{id?}");
            });
        }
    }
}
