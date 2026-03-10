using InfoTextManagerManagement.Api.Models;
using LigaManagement.Api.Models;
using LigaManagement.Web.Classes;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using StadionManagerManagement.Api.Models;
using System;
using System.Reflection;
using ToremanagerManagement.Api.Models.Repository;
using ToreManagerManagement.Api.Models;
namespace LigaManagement.Api
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
            //services.AddDbContext<AppDbContext>(options =>
            //            options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            try
            {
                services.AddScoped<ISpieltageRepository, SpieltageRepository>();

                services.AddScoped<ISpieltageBERepository, SpieltageBERepository>();

                services.AddScoped<ISpieltageITRepository, SpieltageITRepository>();
                services.AddScoped<ISpieltageFRRepository, SpieltageFRRepository>();
                services.AddScoped<ISpieltageESRepository, SpieltageESRepository>();
                services.AddScoped<ISpieltageENRepository, SpieltageENRepository>();
                services.AddScoped<ISpieltageNLRepository, SpieltageNLRepository>();
                services.AddScoped<ISpieltagePTRepository, SpieltagePTRepository>();
                services.AddScoped<ISpieltageTURepository, SpieltageTURepository>();
                services.AddScoped<ISpieltageBERepository, SpieltageBERepository>();
                services.AddScoped<ISpieltageCLRepository, SpieltageCLRepository>();
                services.AddScoped<ISpieltageEMWMRepository, SpieltageEMWMRepository>();
                services.AddScoped<ISpieltagRepositoryLE, SpieltageRepositoryLE>();

                services.AddScoped<IVereinRepository, VereinRepository>();

                services.AddScoped<IVereineITRepository, VereinITRepository>();
                services.AddScoped<IVereinePLRepository, VereinPLRepository>();
                services.AddScoped<IVereineFRRepository, VereinFRRepository>();
                services.AddScoped<IVereineESRepository, VereinESRepository>();
                services.AddScoped<IVereineNLRepository, VereinNLRepository>();

                services.AddScoped<IVereinePTRepository, VereinPTRepository>();
                services.AddScoped<IVereineTURepository, VereinTURepository>();
                services.AddScoped<IVereineBERepository, VereinBERepository>();                                

                services.AddScoped<ISaisonenRepository, SaisonenRepository>();
                services.AddScoped<ISaisonenCLRepository, SaisonenCLRepository>();
                services.AddScoped<ILigenRepository, LigaRepository>();
                services.AddScoped<IKaderRepository, KaderRepository>();
                services.AddScoped<IToreRepository, ToreRepository>();
                services.AddScoped<ISpielerSpieltagRepository, SpielerSpieltagRepository>();
                services.AddScoped<IVereineSaisonRepository, VereineSaisonRepository>();
                services.AddScoped<IVereineSaisonAusRepository, VereineSaisonAusRepository>();
                services.AddScoped<IPokalergebnisseRepository, PokalergebnisseRepository>();
                services.AddScoped<ILaenderRepository, LandRepository>();
                services.AddScoped<IEinstellungenRepository, EinstellungenRepository>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IStadionRepository, StadionRepository>();
                services.AddScoped<IInfoTexteRepository, InfoTexteRepository>();
                services.AddScoped<ISpielplaeneRepository, SpielplaeneRepository>();

                services.AddControllers();
                
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Ligamanager API",
                        Description = "Ligamanager API",                        
                        Contact = new OpenApiContact
                        {
                            Name = "Gerhard Wißt",
                            Email = "g.wisst@web.de",
                            Url = new Uri("https://twitter.com/gwisst"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "GeWi Open License",
                            Url = new Uri("https://google.de"),
                        }
                    });
                });

                services.AddCors(options => {
                    options.AddPolicy("AllowAll",
                        b => b.AllowAnyMethod()
                        .AllowAnyHeader()                        
                        .AllowAnyOrigin());
                });
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseHttpsRedirection();

                app.UseSwagger();
                // This middleware serves the Swagger documentation UI
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
                });

                app.UseRouting();

                app.UseCors("AllowAll");

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }
    }
}
