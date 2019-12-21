using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using NGA.Data.Helper;
using NGA.Data.Service;
using NGA.Data.SubStructure;
using NGA.Domain;
using NGA.MonolithAPI.Config;
using NGA.MonolithAPI.Fillter;
using NGA.MonolithAPI.Helper;
using NGA.MonolithAPI.SignalR;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace NGA.MonolithAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            StaticValues.HostAddress = (IPAddress.Loopback.ToString() + ":" + Configuration.GetValue<int>("Host:Port")).ToString();
            StaticValues.HostSSLAddress = (IPAddress.Loopback.ToString() + ":" + Configuration.GetValue<int>("Host:PortSSL")).ToString();

            string elasticUri = Configuration["ElasticConfiguration:Uri"].Replace("{HostMachineIpAddress}", GetHostMachineIP.Get()); 
            StaticValues.DBConnectionString = Configuration.GetConnectionString("DefaultConnection").Replace("{HostMachineIpAddress}", GetHostMachineIP.Get());

            Serilog.Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                })
                .CreateLogger();
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

            #region Swagger 

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Next Generation API", Version = "v1" });
            });

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

               //services.AddSwaggerGen(c =>
               //{
               //    c.SwaggerDoc("v1", new Info { Title = "Values Api", Version = "v1" });
               //    c.AddSecurityDefinition("Bearer",
               //           new ApiKeyScheme
               //           {
               //               In = "header",
               //               Name = "Authorization",
               //               Type = "apiKey"
               //           });
               //    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
               //     { "Bearer", Enumerable.Empty<string>() },
               //     });

               //});

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
        public void Configure(IApplicationBuilder app, NGADbContext dbContext, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            ParameterHelperLoder.LoadStaticValues(dbContext);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            loggerFactory.AddSerilog();

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

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/help/v1/swagger.json", "NGA V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
