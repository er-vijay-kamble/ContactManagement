namespace ContactManagement.Web.Api
{
    using AutoMapper;
    using ContactManagement.Domain.Interfaces;
    using ContactManagement.Engine.Engines;
    using ContactManagement.Storage.DbConfiguration;
    using ContactManagement.Storage.Interfaces;
    using ContactManagement.Storage.Repositories;
    using ContactManagement.Web.Api.Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        private const string ConnectionStringsKey = "ConnectionStrings";
        private const string DefaultConnectionKey = "DefaultConnection";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc(options =>
            {
                options.Filters.AddService<CustomExceptionFilterAttribute>();
            });
            services.AddSingleton<ILogger>(x => Log.Logger);
            services.AddScoped<CustomExceptionFilterAttribute>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactEngine, ContactEngine>();
            services.AddEntityFrameworkSqlServer().
                AddDbContext<ManagementContext>(option => option.UseSqlServer(Configuration[$"{ConnectionStringsKey}:{DefaultConnectionKey}"]));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info()
                {
                    Title = "Contact Management",
                    Contact = new Contact()
                    {
                        Email = "er.vijaykamble@gmail.com",
                        Name = "Vijay Kamble"
                    }
                });
            });
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Contact Management V1");
                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
