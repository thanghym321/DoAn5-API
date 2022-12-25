using DoAn5.Application.BLL.Interfaces;
using DoAn5.Application.BLL;
using DoAn5.DataContext.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using DoAn5.DataContext.Entities;
using WebApi.Services;
using DoAn5_API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Diagnostics;

namespace DoAn5_API
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
            //db
            services.AddDbContext<DoAn5DbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DoAn5Db")));

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //services
            services.AddTransient<IManageAccount, ManageAccount>();
            services.AddTransient<IManageCategory, ManageCategory>();
            services.AddTransient<IManageCustomer, ManageCustomer>();
            services.AddTransient<IManageExport_Invoice, ManageExport_Invoice>();
            services.AddTransient<IManageExport_Invoice_Detail, ManageExport_Invoice_Detail>();
            services.AddTransient<IManageImport_Invoice,ManageImport_Invoice>();
            services.AddTransient<IManageImport_Invoice_Detail, ManageImport_Invoice_Detail>();
            services.AddTransient<IManageProducer, ManageProducer>();
            services.AddTransient<IManageProduct, ManageProduct>();
            services.AddTransient<IManageProduct_Image, ManageProduct_Image>();
            services.AddTransient<IManageProduct_Price, ManageProduct_Price>();
            services.AddTransient<IManageProvider, ManageProvider>();
            services.AddTransient<IManageSlide, ManageSlide>();
            services.AddTransient<IManageUnit, ManageUnit>();
            services.AddTransient<IManageUser, ManageUser>();


            services.AddHttpClient();
            services.AddControllers();

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //cors them dau tien
            app.UseCors(builder => builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //swagger
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "/swagger/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
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
