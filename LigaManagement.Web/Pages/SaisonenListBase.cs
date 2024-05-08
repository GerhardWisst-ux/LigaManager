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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static LigaManagement.Web.Pages.EinstiegListBase;

namespace LigaManagerManagement.Web.Pages
{
    public class SaisonenListBase : ComponentBase
    {
        protected string DisplayErrorLiga = "none";
        public string Liganame = "Bundesliga";
        public bool value = true;
        public Density Density = Density.Default;

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }
        public IEnumerable<Saison> SaisonenList { get; set; }
        public Saison Saison { get; set; } = new Saison();

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        public List<DisplayLiga> LigenList;

        public List<Verein> Vereine { get; set; }

        public IEnumerable<Liga> Ligen { get; set; }

        public Verein Verein { get; set; }

        public List<DisplayVerein> VereineList = new List<DisplayVerein>();
        public List<DisplayVerein> VereineSaisonList = new List<DisplayVerein>();

        public RadzenDataGrid<Saison> grid;
        IList<Tuple<Saison, RadzenDataGridColumn<Saison>>> selectedCellData = new List<Tuple<Saison, RadzenDataGridColumn<Saison>>>();

        public List<Verein> vereinesaison = new List<Verein>();

        public List<Verein> vereinesaisonSelected = new List<Verein>();

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        public NavigationManager NavigationManager { get; set; }

        string type = "Click";
        bool multiple = true;


        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;

            if (authenticationState.User.Identity == null)
            {
                return;
            }

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/saisonen");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            LigenList = new List<DisplayLiga>();
            Ligen = (await LigaService.GetLigen()).ToList();

            for (int i = 0; i < Ligen.Count(); i++)
            {
                var columns = Ligen.ElementAt(i);
                LigenList.Add(new DisplayLiga(columns.Aktiv, columns.Id, columns.Liganame));
            }

            var liga = (await LigaService.GetLiga(Globals.LigaID));
            Liganame = liga.Liganame;

            SaisonenList = (await SaisonenService.GetSaisonen()).ToList().OrderByDescending(x => x.Saisonname);

            VereineList = new List<DisplayVerein>();

            Vereine = (await VereineService.GetVereine()).ToList();

            var VereineSaison = (await VereineService.GetVereineSaison()).Where(x => x.SaisonID == Convert.ToInt32(Id)).ToList();

            for (int i = 0; i < Vereine.Count(); i++)
            {
                var result = VereineSaison.FindIndex(s => s.VereinNr == Vereine[i].VereinNr);

                if (Id != null)
                {
                    if (result == -1)
                        VereineList.Add(new DisplayVerein(Vereine[i].VereinNr.ToString(), Vereine[i].Vereinsname1, false));
                    else
                        VereineList.Add(new DisplayVerein(Vereine[i].VereinNr.ToString(), Vereine[i].Vereinsname1, true));
                }
                else                   
                    VereineList.Add(new DisplayVerein(Vereine[i].VereinNr.ToString(), Vereine[i].Vereinsname1, false));
            }         

            DisplayErrorLiga = "none";

            Globals.bVisibleNavMenuElements = true;
        }
      
        [Bind]
        public class DisplayVerein
        {
            public DisplayVerein(string vereinID, string vereinname, bool vereinchecked)
            {
                VereinID = vereinID;
                Vereinname1 = vereinname;
                VereinChecked = vereinchecked;

            }
            public string VereinID { get; set; }
            public string Vereinname1 { get; set; }

            public bool VereinChecked { get; set; }
        }

        public async void CheckboxClicked(string aSelectedId, object aChecked)
        {
            Verein = await VereineService.GetVerein(Convert.ToInt32(aSelectedId));

            if ((bool)aChecked)
            {
                if (!vereinesaisonSelected.Contains(Verein))
                    vereinesaisonSelected.Add(Verein);
            }
            else
            {
                if (vereinesaisonSelected.Contains(Verein))
                    vereinesaisonSelected.Remove(Verein);
            }          

            StateHasChanged();
        }
       

        public void LigaChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Globals.currentLiga = e.Value.ToString();
            }
        }
    }
}

