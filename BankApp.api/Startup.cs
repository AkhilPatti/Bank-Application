using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using BankApp.Services;
using BankApp.Models;
using BankApp.api.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

namespace BankApp.api
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

            services.AddDbContext<BankDbContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("DefaultCOnnection"), x => x.MigrationsAssembly("BankApp.Services.Migrations")));

            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddAutoMapper(typeof(AccountMapper).Assembly);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankApp.api", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme { 
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            /*services.AddDbContext<BankDbContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("DefaultCOnnection"), x => x.MigrationsAssembly("BankApp.Services.Migrations")));

            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IAccountService, AccountService>();
            
            services.AddAutoMapper(typeof(AccountMapper).Assembly);*/
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankApp.api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
