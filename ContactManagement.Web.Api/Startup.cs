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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc(options =>
            {
                //Register filters
                options.Filters.AddService<CustomExceptionFilterAttribute>();
            });
            services.AddSingleton<ILogger>(x => Log.Logger);
            services.AddScoped<CustomExceptionFilterAttribute>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactEngine, ContactEngine>();
            services.AddEntityFrameworkSqlServer().
                AddDbContext<ManagementContext>(option => option.UseSqlServer(Configuration[$"{ConnectionStringsKey}:{DefaultConnectionKey}"]));

            // Register the Swagger generator, defining 1 or more Swagger documents
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
