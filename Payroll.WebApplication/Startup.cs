using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Payroll.Domain.Models;
using Payroll.Domain.Repositories;
using Payroll.Tests;
using System.Linq;
using AutoMapper;
using System;
using Payroll.WebApplication.Models;

namespace Payroll.WebApplication
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper(s =>
            {
                s.CreateMap<Person, PersonView>()
                .ReverseMap();
                s.CreateMap<Position, PositionView>()
                .ForMember(x => x.Code, x => x.MapFrom(z => (int)z))
                .ForMember(x => x.Name, x => x.MapFrom(z => z.ToString("F")));
            }, AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton(s => {
                var mock = new Mock<IPersonRepository>();
                mock.Setup(x => x.Save(It.IsAny<Person>())).Returns(10);
                mock.Setup(x => x.GetGraph()).Returns(new Person[] {
                    PersonBuilder.Create(Position.Manager, 2)
                    .AddStaff(PersonBuilder.Create(Position.Manager, 1).AddStaff(Position.Employee, 3))
                    .AddStaff(Position.Employee, 2),
                    PersonBuilder.Create(Position.Sales, 2)
                    .AddStaff(PersonBuilder.Create(Position.Manager, 2).AddStaff(PersonBuilder.Create(Position.Manager, 1).AddStaff(Position.Employee, 3)))
                    .AddStaff(PersonBuilder.Create(Position.Manager, 3).AddStaff(PersonBuilder.Create(Position.Manager, 1).AddStaff(Position.Employee, 3)))
                }.ToList());
                return mock.Object;
            });
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(120);
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
