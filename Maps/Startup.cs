using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Abstract.Repositories;
using Database;
using DAL.Impl.Repositories;
using ADO.DAL.Impl.Repositories;
using Newtonsoft.Json;
using System.Data.SqlClient;
using ADO.DAL.Impl.Infrastructure;
using Common.BL.Abstract;
using WorkWithEF.BL.Impl;
using ADO.BL.Impl;

namespace Maps
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
            services.AddControllers();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection));


            if (Boolean.Parse(Configuration["UseEF"]))
            {
                services.AddScoped<ICountryRepository, DAL.Impl.Repositories.CountryRepository>();
                services.AddScoped<ICityRepository, DAL.Impl.Repositories.CityRepository>();
                services.AddScoped<IStreetRepository, DAL.Impl.Repositories.StreetRepository>();
                services.AddScoped<IHouseRepository, DAL.Impl.Repositories.HouseRepository>();
                services.AddScoped<IAppartmentRepository, DAL.Impl.Repositories.AppartmentRepository>();
                services.AddScoped<IPersonRepository, DAL.Impl.Repositories.PersonRepository>();

                services.AddScoped<IPersonBusinessLogic, WorkWithEF.BL.Impl.PersonBusinessLogic>();
                services.AddScoped<IHouseBusinessLogic, WorkWithEF.BL.Impl.HouseBusinessLogic>();
            }
            else if (Boolean.Parse(Configuration["UseADO"]))
            {

                services.AddScoped<ICountryRepository, ADO.DAL.Impl.Repositories.CountryRepository>();
                services.AddScoped<ICityRepository, ADO.DAL.Impl.Repositories.CityRepository>();
                services.AddScoped<IStreetRepository, ADO.DAL.Impl.Repositories.StreetRepository>();
                services.AddScoped<IHouseRepository, ADO.DAL.Impl.Repositories.HouseRepository>();
                services.AddScoped<IAppartmentRepository, ADO.DAL.Impl.Repositories.AppartmentRepository>();
                services.AddScoped<IPersonRepository, ADO.DAL.Impl.Repositories.PersonRepository>();

                services.AddScoped<IPersonBusinessLogic, ADO.BL.Impl.PersonBusinessLogic>();
                services.AddScoped<IHouseBusinessLogic, ADO.BL.Impl.HouseBusinessLogic>();

                services.AddScoped<SqlConnection>();
                services.AddScoped<SqlParameter>();
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
