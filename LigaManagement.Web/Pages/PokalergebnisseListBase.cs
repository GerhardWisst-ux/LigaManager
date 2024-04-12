using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagement.Web.Pages
{
    public class PokalergebnisseListBase : ComponentBase
    {
        public RadzenDataGrid<PokalergebnisSpieltag> grid;
        public Density Density = Density.Default;
        public bool allowVirtualization;
        public string Titel { get; set; }

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

        private readonly AppDbContext appDbContext;

        public bool VisibleBtnNew { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<Verein> Vereine { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }             
       
        [Inject]
        public IPokalergebnisseService PokalergebnisseService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }
             
        public IEnumerable<PokalergebnisSpieltag> PokalergebnisseSpieltage { get; set; }

        public IEnumerable<PokalergebnisSpieltag>  PokalergebnisseSpieltageAlle { get; set; }
        protected override async Task OnInitializedAsync()
        {
            SaisonenList = new List<DisplaySaison>();
            Saisonen = (await SaisonenService.GetSaisonen()).ToList();

            for (int i = 0; i < Saisonen.Count(); i++)
            {
                var columns = Saisonen.ElementAt(i);
                SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
            }

            PokalergebnisseSpieltageAlle = await PokalergebnisseService.GetPokalergebnisseSpieltag();

            if (PokalergebnisseSpieltageAlle == null)
                return;

            PokalergebnisseSpieltageAlle = PokalergebnisseSpieltageAlle.ToList().Where(x => x.Runde == "F").OrderByDescending(x => x.Datum);
            
            DisplayErrorRunde = "none";
            DisplayErrorSaison = "none";

            VisibleBtnNew = false;

        }

        public void ValidateItems(IEnumerable args)
        {
            TabellenList = (List<int>)args;                                
            
        }

        public async void SaisonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                SaisonChoosed = Convert.ToInt32(e.Value);                             
                
                Globals.PokalSaisonID = SaisonChoosed;
                
                var saison = await SaisonenService.GetSaison(Convert.ToInt32(SaisonChoosed));

                Globals.currentPokalSaison = saison.Saisonname;
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

            Globals.currentPokalSaison = str.ToString();
            Globals.PokalSaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentPokalSaison).SaisonID;

            //Console.WriteLine($"Value changed to {str}");
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

        public async void OnClickHandler()
        {
            if (SaisonChoosed == 0 && RundeChoosed == null)
            {
                DisplayErrorSaison = "block";
                DisplayErrorRunde = "block";
                return;
            }

            if (SaisonChoosed == 0)
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

            if (RundeChoosed == "HF" && PokalergebnisseSpieltage.Count() == 2)
                VisibleBtnNew = false;
            else if (RundeChoosed == "VF" && PokalergebnisseSpieltage.Count() == 4)
                VisibleBtnNew = false;
            else if (RundeChoosed == "AF" && PokalergebnisseSpieltage.Count() == 8)
                VisibleBtnNew = false;
            else if (RundeChoosed == "2" && PokalergebnisseSpieltage.Count() == 16)
                VisibleBtnNew = false;
            else
                VisibleBtnNew = true;

            Globals.bVisibleNavMenuElements = true;
            StateHasChanged();
        }       
    }
}



