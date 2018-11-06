using System;
using BookStoreWebService.Models.BookDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication2.Models;

namespace BookStoreWebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:64675")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddSession(c =>
            {
                c.IdleTimeout = TimeSpan.FromMinutes(5);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<BookStoreDBContext>(opts => opts.UseSqlServer(Configuration.GetConnectionString("myDatabase")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseSession();
            app.UseCors("MyPolicy");
            app.UseMvc();
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc(routes =>
           {
               routes.MapRoute(
                   name: "default",
                   template: "{controller=Admin}/{action=HomeView}/{id?}");
           });
        }
       
    }
}
