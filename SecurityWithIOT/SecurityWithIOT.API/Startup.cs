using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Helpers;
using SecurityWithIOT.API.Model.Interfaces;
using SecurityWithIOT.API.Services;

namespace SecurityWithIOT.API
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var key = System.Text.Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            services.AddDbContext<DataContext>(x => x.UseSqlServer(_configuration.GetConnectionString("MSSQLConnection")));
            //services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("SQLiteConnection")));
            services.AddTransient<Seed>();
            services.AddCors();
            services.Configure<ClaudinarySettings>(_configuration.GetSection("CloudinarySettings"));
            services.AddAutoMapper();
            services.AddScoped<IAuthRepository,AuthRepository>(); 
            services.AddScoped<IUser,UserService>();       
            services.AddScoped<IDepartment,DepartmentService>();
            services.AddScoped<ICompany,CompanyService>();
            services.AddScoped<IAddress, AddressService>();
            services.AddScoped<IPhoto, PhotoService>();
            services.AddScoped<ICountry, CountryService>();
            services.AddScoped<ICity, CityService>();
            services.AddScoped<IDistrict, DistrictService>();
            
            services.AddMvc().AddJsonOptions(opt => {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddScoped<LogUserActivity>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder) 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else {

                app.UseExceptionHandler(builder => {

                    builder.Run(async context => {

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null){
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });

                });

            }

            var isSeedEnabled = Convert.ToBoolean(_configuration.GetSection("Seed:Enabled").Value);

            if (isSeedEnabled)
            {
            seeder.SeedCountry();
            seeder.SeedCity();
            seeder.SeedUsers(); 
            }
            
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}
