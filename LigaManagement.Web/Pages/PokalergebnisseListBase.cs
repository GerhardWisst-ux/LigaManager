using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigaManagement.Web.Pages
{
    public class PokalergebnisseListBase : ComponentBase
    {
        public RadzenDataGrid<PokalergebnisSpieltag> grid;
        public Density Density = Density.Default;
        public bool allowVirtualization;

        protected string DisplayErrorRunde = "none";
        protected string DisplayErrorSaison = "none";
        public IEnumerable<int> values = new int[] { 1, 2, 3, 4, 5};
        public List<int> TabellenList;
        public int SaisonChoosed = 0;
        public string RundeChoosed;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [BindProperty]
        public string ImportCSV { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        NotificationService NotificationService;

        public List<DisplaySaison> SaisonenList;

        public List<DisplayLiga> LigenList;

        private readonly AppDbContext appDbContext;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }
             
       
        [Inject]
        public IPokalergebnisseService PokalergebnisseService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }
             
        public IEnumerable<PokalergebnisSpieltag> PokalergebnisseSpieltage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            SaisonenList = new List<DisplaySaison>();
            Saisonen = (await SaisonenService.GetSaisonen()).ToList();

            for (int i = 0; i < Saisonen.Count(); i++)
            {
                var columns = Saisonen.ElementAt(i);
                SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
            }
                             
            DisplayErrorRunde = "none";
            DisplayErrorSaison = "none";
        }

        public void ValidateItems(IEnumerable args)
        {
            TabellenList = (List<int>)args;
                                
            
        }
        public void SaisonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                SaisonChoosed = Convert.ToInt32(e.Value);

            }
        }
        public async void RundeChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                RundeChoosed = e.Value.ToString();               

            }
        }
        public void OnSaisonChange(object value)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            Globals.currentSaison = str.ToString();
            Globals.SaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).SaisonID;

            //Console.WriteLine($"Value changed to {str}");
        }

        public void OnProgress(UploadProgressArgs args, string name)
        {
            Console.WriteLine($"{args.Progress}% '{name}' / {args.Loaded} of {args.Total} bytes.");

            if (args.Progress == 100)
            {
                foreach (var file in args.Files)
                {
                    Console.WriteLine($"Uploaded: {file.Name} / {file.Size} bytes");
                }
            }
        }

        public void LigaChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Globals.currentLiga = e.Value.ToString();
            }
        }

        public class DisplaySaison
        {
            public DisplaySaison(int saisonID, string saisonname)
            {
                SaisonID = saisonID;
                Saisonname = saisonname;
            }
            public int SaisonID { get; set; }
            public string Saisonname { get; set; }
        }

        public class DisplayLiga
        {
            public DisplayLiga(int ligaID, string liganame)
            {
                LigaID = ligaID;
                Liganame = liganame;
            }
            public int LigaID { get; set; }
            public string Liganame { get; set; }
        }

        public async void OnClickHandler()
        {
            if (SaisonChoosed == null && RundeChoosed == null)
            {
                DisplayErrorSaison = "block";
                DisplayErrorRunde = "block";
                return;
            }

            if (SaisonChoosed == null)
            {
                DisplayErrorSaison = "block";
                DisplayErrorRunde = "none";
                return;
            }

            if (RundeChoosed == null)
            {
                DisplayErrorRunde = "block";
                DisplayErrorSaison = "none";
                return;
            }
            
            DisplayErrorSaison = "none";
            DisplayErrorRunde = "none";

            PokalergebnisseSpieltage = await PokalergebnisseService.GetPokalergebnisseSpieltag();

            if (PokalergebnisseSpieltage == null)
                return;

            PokalergebnisseSpieltage = PokalergebnisseSpieltage.ToList();
            PokalergebnisseSpieltage = PokalergebnisseSpieltage.Where(x => x.SaisonID == SaisonChoosed && x.Runde == RundeChoosed).OrderBy(x => x.Datum);
                                    
            Globals.bVisibleNavMenuElements = true;
            StateHasChanged();
        }
       
    }
}



