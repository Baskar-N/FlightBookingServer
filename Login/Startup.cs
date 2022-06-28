using LoginApiService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApiService
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
            services.AddMvc();
            services.AddScoped<IUserRepository, SqlUserRepository>();
            services.AddDbContext<AppDbContext>(
                options => {
                    options.UseSqlServer(Configuration.GetConnectionString("SqlDbConnection"));
                }
            );

            services.AddSwaggerGen(options => {
                    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                    options.OperationFilter<SecurityRequirementsOperationFilter>();
                    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Login Web Api",
                        Version = "v1",
                        Description = "Login Api Service"
                    });
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Login Web Api");
                });
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => 
                endpoints.MapControllerRoute("default", "{controller:auth/action:login}")    
            );
        }
    }
}
