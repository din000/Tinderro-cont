using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tinderro.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Tinderro.API.Helpers;

namespace Tinderro.API
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
            services.AddControllers().AddNewtonsoftJson(o => {
                o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // to igoruje petle w petli przy bazie danych jak 1 odwoluje sie do 2 i 2 do 1 i tak w nieskonczonosc
            });
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MojePolaczenie")));
            services.AddCors(); // to pozwoli na uzywanie API we front-endzie
            services.Configure<ClaudinarySettings>(Configuration.GetSection("ClaudinarySettings")); // konfiguracja chmury ze zdjeciami
            services.AddAutoMapper(typeof(Startup)); // rozwiazanie z overflow XD
            services.AddTransient<Seed>(); //laduje przykladowe dane
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>(); // tam gdzie bedzie wylowywany inteerface to wskoczy Authrepository
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>{
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                                ValidateIssuer = false,
                                ValidateAudience = false
                            };
                        });
            services.AddScoped<LogUserActivity>(); // z helpersow
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seeder) // dodajemy tutaj Seed
        {
            if (env.IsDevelopment())
            {
                 app.UseDeveloperExceptionPage();
            }
            else
            {
                            // tryb produkcyjny
            // obsluga bledow
                // context to chodzi o http context
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if(error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message); // to sprawi ze w f12 beda dostepne bledy
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }
            
            seeder.SeedUsers(); // tutaj odpalamy metode z Seed

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); // to pozwoli na uzywanie API we front-endzie

            app.UseAuthentication(); // uzywa tokenu do autoryzacji

            app.UseHttpsRedirection();
          
            app.UseRouting();
            
            app.UseAuthorization();

            // te 2 linijki + app.UseEndpoints pozwalaja odpalic to co sie zrobilo w angularze bez odpalania angulara
            // wczesniej w angularze za pomoca ng build i odpowiednies sciezki w angular.json przenieslismy pliki to wwwroot w API
            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=NaszaNazwa_Fallback}/{action=Index}/{id?}");

                endpoints.MapFallbackToController("Index", "NaszaNazwa_Fallback");
            });
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapControllers();
            // });
            // app.UseEndpoints(endpoints =>
            // {
            //     endpoints.MapDefaultControllerRoute(); // MapControllers();
            // });
        }
    }
}
