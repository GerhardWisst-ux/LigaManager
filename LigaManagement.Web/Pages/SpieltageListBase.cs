using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class SpieltagListBase : ComponentBase
    {
        [Parameter]
        public object SpieltagNr { get; set; }
        public bool VisibleBtnNew { get; set; }

        public bool VisibleVorZurueck { get; set; }

        public List<DisplaySpieltag> SpieltagList;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }
        public IEnumerable<Spieltag> Spieltage { get; set; }
        public IEnumerable<Verein> Vereine { get; set; }
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {

            try
            {
                var authenticationState = await authenticationStateTask;
                if (!authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode($"/spieltage/1");
                    NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
                }

                SpieltagList = new List<DisplaySpieltag>();

                for (int i = 1; i <= Globals.maxSpieltag; i++)
                {
                    SpieltagList.Add(new DisplaySpieltag(i.ToString(), i.ToString() + ".Spieltag"));
                }

                Vereine = await VereineService.GetVereine();

                Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString()).Where(st => st.Saison == Globals.currentSaison).ToList();
                Spieltage = Spieltage.OrderBy(o => o.Datum);

                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);
                    columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname1;
                    columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr)).Vereinsname2;
                }

                if (Globals.currentSaison == "1963/64" || Globals.currentSaison == "1964/65")
                {
                    if (Spieltage.Count() >= 8)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else if (Globals.currentSaison == "1991/92")
                {
                    if (Spieltage.Count() >= 10)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else
                {
                    if (Spieltage.Count() >= 9)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }                

                if (Spieltage.Count() == 0)
                    VisibleVorZurueck = false;
                else
                    VisibleVorZurueck = true;

            }
            catch (Exception ex)
            {

                Debug.Print(ex.Message);
            }
        }

        public async Task SpieltagChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                SpieltagNr = Convert.ToInt32(e.Value);

                int SpieltagNr2 = Convert.ToInt32(e.Value);
                Globals.Spieltag = SpieltagNr2;
                Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString()).Where(st => st.Saison == Ligamanager.Components.Globals.currentSaison).ToList();
                for (int i = 0; i < Spieltage.Count(); i++)
                {
                    var columns = Spieltage.ElementAt(i);
                    columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname1;
                    columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr)).Vereinsname2;
                }

                // ToDo
                if (Spieltage.Count() == 0)
                {

                }

                if (Globals.currentSaison == "1963/64" || Globals.currentSaison == "1964/65")
                {
                    if (Spieltage.Count() >= 8)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else if (Globals.currentSaison == "1991/92")
                {
                    if (Spieltage.Count() >= 10)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }
                else
                {
                    if (Spieltage.Count() >= 9)
                        VisibleBtnNew = false;
                    else
                        VisibleBtnNew = true;
                }


                if (Spieltage.Count() == 0)
                    VisibleVorZurueck = false;
                else
                    VisibleVorZurueck = true;
                
            }
        }
        public async Task SpieltagZurueck()
        {
            if (Convert.ToInt32(SpieltagNr) > 1)
                SpieltagNr = Convert.ToInt32(SpieltagNr) - 1;
            else
                return;


            Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString()).Where(st => st.Saison == Globals.currentSaison).ToList();
            for (int i = 0; i < Spieltage.Count(); i++)
            {
                var columns = Spieltage.ElementAt(i);
                columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname1;
                columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr)).Vereinsname2;
            }

            int SpieltagNr2 = Convert.ToInt32(SpieltagNr);
            Globals.Spieltag = SpieltagNr2;

            if (Globals.currentSaison == "1963/64" || Globals.currentSaison == "1964/65")
            {
                if (Spieltage.Count() >= 8)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }
            else if (Globals.currentSaison == "1991/92")
            {
                if (Spieltage.Count() >= 10)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }
            else
            {
                if (Spieltage.Count() >= 9)
                    VisibleBtnNew = false;
                else
                    VisibleBtnNew = true;
            }

            if (Spieltage.Count() == 0)
                VisibleVorZurueck = false;
            else
                VisibleVorZurueck = true;
          
        }

        public async Task SpieltagVor()
        {
            if (Convert.ToInt32(SpieltagNr) < Globals.maxSpieltag)
                SpieltagNr = Convert.ToInt32(SpieltagNr) + 1;
            else
                return;

            Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.SpieltagNr == SpieltagNr.ToString()).Where(st => st.Saison == Globals.currentSaison).ToList();
            for (int i = 0; i < Spieltage.Count(); i++)
            {
                var columns = Spieltage.ElementAt(i);
                columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname1;
                columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr)).Vereinsname2;
            }

            int SpieltagNr2 = Convert.ToInt32(SpieltagNr);
            Globals.Spieltag = SpieltagNr2;

            if (Spieltage.Count() >= 9)
                VisibleBtnNew = false;
            else
                VisibleBtnNew = true;


            if (Spieltage.Count() == 0)
                VisibleVorZurueck = false;
            else
                VisibleVorZurueck = true;

            StateHasChanged();
        }
        public class DisplaySpieltag
        {
            public DisplaySpieltag(string nummer, string name)
            {
                Nummer = nummer;
                Name = name;
            }
            public string Nummer { get; set; }
            public string Name { get; set; }

        }
    }
}
