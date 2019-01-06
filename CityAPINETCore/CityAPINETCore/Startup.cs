using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityAPINETCore.Entities;
using CityAPINETCore.Models;
using CityAPINETCore.Services;
using CityAPINETCore.Services.Definicion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace CityAPINETCore
{
    public class Startup
    {
        public static IConfiguration Configuracion { get; private set; }
        public Startup(IConfiguration configuracion)
        {
            Configuracion = configuracion;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
              services.AddTransient<IMailService,CloudMailService>();

#endif
            var connectionString = Startup.Configuracion["connectionStrings:cityInfoDBConnectionString"];
            services.AddDbContext<CityInfoContext>(x => x.UseSqlServer(connectionString));

            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CityInfoContext cityInfoContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            cityInfoContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<City, CityWhithoutPointsOfInterestDTO>();
                cfg.CreateMap<City, CityDTO>();
                cfg.CreateMap<PointOfInterest, PointsOfInteresDTO>();
                cfg.CreateMap<PointsOfInteresDTO, PointOfInterest>();
            });

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
