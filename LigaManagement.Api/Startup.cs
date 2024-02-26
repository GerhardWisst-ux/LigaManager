using LigaManagement.Api.Models;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));
                       
            services.AddScoped<ISpieltagRepository, SpieltagRepository>();
            services.AddScoped<IVereinRepository, VereinRepository>();
            services.AddScoped<ITabelleRepository, TabelleRepository>();
            services.AddScoped<ISaisonRepository, SaisonRepository>();
            services.AddScoped<ILigaRepository, LigaRepository>();
            services.AddScoped<IKaderRepository, KaderRepository>();
            services.AddScoped<ISpielerSpieltagRepository, SpielerSpieltagRepository>();

            services.AddControllers();
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
