using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MusicSystem.Data;
using MusicSystem.Helpers;
using MusicSystem.Repositories;
using MusicSystem.Repositories.Interfaces;
using MusicSystem.Services;
using MusicSystem.Services.Interfaces;

namespace MusicSystem
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
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient<ISongsService, SongsService>();
            services.AddTransient<ISongsPerformersService, SongsPerformersService>();
            services.AddTransient<IPerformerService, PerformersService>();
            services.AddTransient<IProducersService, ProducersService>();
            services.AddTransient<IAlbumsService, AlbumsService>();
            services.AddTransient<IWritersService, WritersService>();
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddScoped<ApplicationDbContext>();

            services.AddControllers()
        .AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));


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
