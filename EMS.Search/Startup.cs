using EMS.Search.Repositories.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace EMS.Search
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private IConfiguration Configuration { get; }
        readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc()
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            //    .AddJsonOptions(options =>
            //    {
            //        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            //        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            //        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
            //        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //    });
            //services.AddRazorPages();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                  options.SerializerSettings.ContractResolver =
                     new DefaultContractResolver());
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string[] corsList = Configuration.GetSection("CORSes").Value.Split(";").Where(c => !string.IsNullOrEmpty(c)).ToArray();
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                builder =>
                {
                    if (corsList.Count() == 1 && corsList[0] == "*")
                        builder.AllowAnyOrigin().WithMethods("POST").AllowAnyHeader().AllowCredentials();
                    else
                        builder.WithOrigins(corsList.ToArray()).WithMethods("POST").AllowAnyHeader().AllowCredentials();
                });
            });
            
            services.AddDbContext<EMSContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("EMSContext")));

            services.Scan(scan => scan
             .FromAssemblyOf<IServiceScoped>()
                 .AddClasses(classes => classes.AssignableTo<IServiceScoped>())
                     .AsImplementedInterfaces()
                     .WithScopedLifetime());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Test API",
                    Description = "ASP.NET Core Web API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.ApplicationName == Environments.Development)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();
            app.UseCors(AllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/TF/swagger/{documentname}/swagger.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/TF/swagger/v1/swagger.json", "TF API");
                c.RoutePrefix = "api/TF/swagger";
            });
            
        }
    }
}
