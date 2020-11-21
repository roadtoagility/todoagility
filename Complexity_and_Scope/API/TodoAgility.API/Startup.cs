using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TodoAgility.Agile.CQRS.QueryHandlers.Activity;
using TodoAgility.Agile.CQRS.QueryHandlers.Counter;
using TodoAgility.Agile.CQRS.QueryHandlers.Project;
using TodoAgility.Agile.Persistence.Framework;
using TodoAgility.Agile.Persistence.Model;
using TodoAgility.Agile.Persistence.Repositories;

namespace TodoAgility.API
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
            services.AddMediatR(typeof(Startup));
            services.AddSwaggerGen();

            services.AddDbContext<ActivityDbContext>();

            services.AddScoped<IRequestHandler<ActivityByProjectFilter, ActivityByProjectResponse>, ActivityByProjectQueryHandler>();
            services.AddScoped<IRequestHandler<ActivityDailyCounterFilter, ActivityDailyCounterFilterResponse>, ActivityDailyCounterFilterQueryHandler>();
            services.AddScoped<IRequestHandler<ActivityFinishedCounterFilter, ActivityFinishedCounterResponse>, ActivityFinishedCounterQueryHandler>();
            services.AddScoped<IRequestHandler<ProjectFinishedCounterFilter, ProjectFinishedCounterResponse>, ProjectFinishedCounterQueryHandler>();
            services.AddScoped<IRequestHandler<FeaturedProjectsFilter, FeaturedProjectsResponse>, FeaturedProjectsQueryHandler>();
            services.AddScoped<IRequestHandler<LastProjectsFilter, LastProjectsResponse>, LastProjectsQueryHandler>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IDbSession<IActivityRepository>, ActivityDbSession<IActivityRepository>>();

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy
                          .AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseCors("default");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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
