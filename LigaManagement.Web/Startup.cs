using AutoMapper;
using LigaManagement.Web.Services.Contracts;
using LigaManagerManagement.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using Radzen.Blazor;
using Radzen.Blazor.Rendering;
using System;

namespace LigaManagement.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Identity.Application")
                .AddCookie();

            services.AddRazorPages();
            //services.AddBlazorBootstrap(); // Add this line                        
            
            services.AddServerSideBlazor();
          

            services.AddAutoMapper(typeof(Startup));          
           
            services.AddHttpClient<IVereineService, VereineService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44355/");
            });
            services.AddHttpClient<ISaisonenService, SaisonenService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44355/");
            });
            services.AddHttpClient<ILigaService, LigaService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44355/");
            });
            services.AddHttpClient<ISpieltagService, SpieltagService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44355/");
            });
            services.AddHttpClient<ITabelleService, TabelleService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44355/");
            });
            services.AddHttpClient<IKaderService, KaderService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44355/");
            });
            services.AddHttpClient<ISpielerSpieltagService, SpielerSpieltagService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:44355/");
            });

            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ChartTooltip>();
            services.AddScoped<RadzenChart>();
            services.AddScoped<ContextMenuService>();                        

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

           
        }
      
    }
}
