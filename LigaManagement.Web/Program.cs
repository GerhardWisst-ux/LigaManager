using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Web;
using System;
using Ligamanager.Components;

namespace LigaManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {                    

                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseWebRoot("wwwroot");                    
                    webBuilder.UseStaticWebAssets();

                    if (LMSettings.GetSprache_LandKZ() == "DE")
                    {
                        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("de-DE");
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("de-DE");
                    }
                    else if (LMSettings.GetSprache_LandKZ() == "EN")
                    {
                        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");
                    }
                    else
                    {
                        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("de-DE");
                        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("de-DE");
                    }


                });

        //static void SetWebsiteLanguage(WebAssemblyHost host)
        //{
        //    var navigationManager = host.Services.GetRequiredService<NavigationManager>();
        //    var uri = new Uri(navigationManager.Uri);
        //    var urlParameters = HttpUtility.ParseQueryString(uri.Query);
        //    var defaultCulture = CultureInfo.GetCultureInfo("fr");
        //    string urlQueryKey = "language";
        //    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(urlParameters[urlQueryKey] ?? defaultCulture.Name);
        //    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(urlParameters[urlQueryKey] ?? defaultCulture.Name);
        //}
    }
}
