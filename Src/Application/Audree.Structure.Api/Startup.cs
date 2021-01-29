using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Audree.Structure.Core.Contracts.IRespositories;
using Audree.Structure.Core.Contracts.IUnitOfWork;
using Audree.Structure.Infrastructure.Data;
using Audree.Structure.Infrastructure.Respositories;
using Audree.Structure.Infrastructure.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Audree.Structure.Api.Helpers;

namespace Audree.Structure.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            OptionsBuilderConfiguration = configuration;
        }

        public IConfiguration OptionsBuilderConfiguration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Global cors policy
            // global cors policy
            services.AddCors(options =>
            {
                options.AddPolicy("m",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    // builder.WithOrigins(Configuration.GetValue<string[]>("CorsConfig:AllowedOrigins"));
});
            });
            #endregion
            services.AddControllers();

            #region Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRUD ", Version = "v1" });

            });
            #endregion

            //#region Auto Mapper
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new AutoMapper());
            //});
            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);
            //#endregion

            #region Auto Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperClass());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<UsernewIRespositories, UserNewRepositories>();
            services.AddScoped<IRespositoriesEmp, EmpRepositories>();

            services.AddDbContext<Context>(item => item.UseSqlServer(OptionsBuilderConfiguration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Enable middleware to serve generated Swagger as a JSON endpoint.
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            #endregion



            #region Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),specifying the Swagger JSON endpoint.
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audree V1");
                //c.RoutePrefix = string.Empty;
            });
            #endregion
      


            app.UseCors("m");
            app.UseCookiePolicy();

            app.UseAuthentication();

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