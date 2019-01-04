﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using nwEventoMVCa.Core.Mapper;
using nwEventoMVCa.Core.Repositories;
using nwEventoMVCa.Core.Services;
using nwEventoMVCa.Infrastructure.Repositories;

namespace nwEventoMVCa.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IDataInitializer, DataInitializer>();
            services.AddSingleton(AutoMapperConfig.GetMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            SeedData(app);
            app.UseMvcWithDefaultRoute();
        }

        private void SeedData(IApplicationBuilder app)
        {
            var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
            dataInitializer.Seed();
        }
    }
}
