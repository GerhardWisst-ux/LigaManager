using LigaManagement.Api.Models;
using LigaManagement.Web.Classes;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System;
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
                services.AddScoped<ISpieltagRepository, SpieltageRepository>();

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

                services.AddScoped<IVereinRepository, VereinRepository>();

                services.AddScoped<IVereineITRepository, VereinITRepository>();
                services.AddScoped<IVereinePLRepository, VereinPLRepository>();
                services.AddScoped<IVereineFRRepository, VereinFRRepository>();
                services.AddScoped<IVereineESRepository, VereinESRepository>();
                services.AddScoped<IVereineNLRepository, VereinNLRepository>();

                services.AddScoped<IVereinePTRepository, VereinPTRepository>();
                services.AddScoped<IVereineTURepository, VereinTURepository>();

                services.AddScoped<IVereinePTRepository, VereinPTRepository>();
                services.AddScoped<IVereineTURepository, VereinTURepository>();
                services.AddScoped<IVereineBERepository, VereinBERepository>();
                services.AddScoped<IVereinRepository, VereinRepository>();
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

                services.AddControllers();

                services.AddCors(options =>
                {
                    options.AddPolicy("NewPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
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

                app.UseRouting();

                app.UseCors("NewPolicy");

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
