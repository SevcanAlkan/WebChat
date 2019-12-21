using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using NGA.Core;
using NGA.Data;
using NGA.Data.Service;
using NGA.Data.SubStructure;
using NGA.Domain;
using NGA.MonolithAPI.Config;
using NGA.MonolithAPI.Fillter;
using NGA.MonolithAPI.SignalR;

namespace NGA.MonolithAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            StaticValues.HostAddress = (IPAddress.Loopback.ToString() + ":" + Configuration.GetValue<int>("Host:Port")).ToString();
            StaticValues.HostSSLAddress = (IPAddress.Loopback.ToString() + ":" + Configuration.GetValue<int>("Host:PortSSL")).ToString();

            string hostMachineIpAddress = "127.0.0.1";

            try
            {
                hostMachineIpAddress = Dns.GetHostAddresses(new Uri("http://docker.for.win.localhost").Host)[0].ToString();
            }
            catch (SocketException es)
            {
            }
            catch (Exception e)
            {
            }
            finally
            {
                StaticValues.DBConnectionString = Configuration.GetConnectionString("DefaultConnection").Replace("{HostMachineIpAddress}", hostMachineIpAddress);
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Add CORS
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200", "http://192.168.0.102:4200")
                 ));
            #endregion

            #region Add Entity Framework and Identity Framework
            services.AddIdentity<User, Role>()
              .AddEntityFrameworkStores<NGADbContext>()
              .AddDefaultTokenProviders();
            #endregion

            #region Add Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<String>("Jwt:Key")));

               options.RequireHttpsMetadata = false;
               options.SaveToken = true;

               options.TokenValidationParameters = new TokenValidationParameters
               {
                   LifetimeValidator = (before, expires, token, param) =>
                   {
                       return expires > DateTime.UtcNow;
                   },
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = signingKey,
                   ValidAudience = Configuration["Jwt:Audience"],
                   ValidIssuer = Configuration["Jwt:Issuer"],
               };

               //options.Events = new JwtBearerEvents
               //{
               //    OnMessageReceived = context =>
               //    {
               //        var accessToken = context.Request.Query["access_token"];

               //        var path = context.HttpContext.Request.Path;
               //        if (!string.IsNullOrEmpty(accessToken) &&
               //            (path.StartsWithSegments("/chatHub")))
               //        {
               //            context.Token = accessToken;
               //        }
               //        return Task.CompletedTask;
               //    }
               //};
           });
            #endregion

            #region MVC Configration
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidatorActionFilter));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            #endregion

            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddAutoMapper(typeof(Startup).Assembly);
            #endregion

            #region Dependency Injection 

            services.AddSingleton(mapper);
            services.AddDbContext<NGADbContext>(db => db.UseSqlServer(StaticValues.DBConnectionString));
            services.AddTransient<UnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IParameterService, ParameterService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupUserService, GroupUserService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));

            //services.AddSingleton<ChatHub>();

            #endregion

            #region Add SignalR
            //services.AddSignalR().AddHubOptions<ChatHub>(options =>
            //{
            //    options.EnableDetailedErrors = true;
            //});
            #endregion

            #region Versioning

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(2, 0);
                o.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(),
                    new HeaderApiVersionReader("api-version"));
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, NGADbContext dbContext, IWebHostEnvironment env)
        {
            Data.Helper.ParameterHelperLoder.LoadStaticValues(dbContext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            //app.UseSignalR(routes =>
            //{
            //    routes.MapHub<ChatHub>("/chathub");  //https://stackoverflow.com/questions/43181561/signalr-in-asp-net-core-1-1
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
