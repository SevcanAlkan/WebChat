using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using NGA.API.Config;
using NGA.API.Filter;
using NGA.API.SignalR;
using NGA.Core;
using NGA.Data;
using NGA.Data.Service;
using NGA.Data.SubStructure;
using NGA.Domain;

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

               options.Events = new JwtBearerEvents
               {
                   OnMessageReceived = context =>
                   {
                       var accessToken = context.Request.Query["access_token"];

                       var path = context.HttpContext.Request.Path;
                       if (!string.IsNullOrEmpty(accessToken) &&
                           (path.StartsWithSegments("/chatHub")))
                       {
                           context.Token = accessToken;
                       }
                       return Task.CompletedTask;
                   }
               };
           });
            #endregion
          
            #region MVC Configration
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidatorActionFilter));
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

            #region Add SignalR
            services.AddSignalR().AddHubOptions<ChatHub>(options =>
            {
                options.EnableDetailedErrors = true;
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            //app.UseCookiePolicy();

            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chathub");
            });

            app.UseAuthentication();

            app.UseMvc(options =>
            {
                options.MapRoute(name: "DefaultApi",
                template: "api/{controller}/{id?}");
            });
        }
    }
}
