using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LigaManagement.Models;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using LigaManagement.Web.Shared;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using LigaManagerManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Radzen;

namespace LigaManagerManagement.Web.Pages
{
    public class EditKaderSpielerBase : ComponentBase
    {
        [Inject]
        public IKaderService KaderService { get; set; }
        public IEnumerable<Kader> KaderList { get; set; }
        public Kader Kader { get; set; } = new Kader();

        public string DisplayElements = "none";
        public string DisplayErrorVerein = "none";
        public string DisplayErrorSaison = "none";

        public string Vereinsname1;

        public int? PositionsNr;

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }
        [Inject]
        public ISpieltagService SpieltagService { get; set; }
        public List<DisplayKaderVerein> VereineList = new List<DisplayKaderVerein>();

        public string Verein1_Nr;

        public string Position;
        public IEnumerable<Verein> Vereine { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<KaderList> Localizer { get; set; }
        protected override async Task OnInitializedAsync()
        {

            var authenticationState = await authenticationStateTask;

            if (authenticationState.User.Identity == null)
            {
                return;
            }

            if (!authenticationState.User.Identity.IsAuthenticated)
            {
                string returnUrl = WebUtility.UrlEncode($"/Ligamanager");
                NavigationManager.NavigateTo($"/identity/account/login?returnUrl={returnUrl}");
            }

            var verein = await VereineService.GetVerein(Globals.KaderVereinNr);
            Vereinsname1 = verein.Vereinsname1;
            Verein1_Nr = verein.VereinNr.ToString();
                        
            Vereine = (await VereineService.GetVereine()).ToList();
            var vereineSaison = await VereineService.GetVereineSaison();

            List<VereinAktSaison> verList = vereineSaison.ToList();

            for (int i = 0; i < verList.Count(); i++)
            {
                if (verList[i].SaisonID == Globals.SaisonID)
                    VereineList.Add(new DisplayKaderVerein(verList[i].VereinNr.ToString(), verList[i].Vereinsname1, verList[i].Stadion));
            }

            DisplayElements = "none";
            DisplayErrorSaison = "none";   
            Globals.bVisibleNavMenuElements = true;
        }

        public void VereinChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Verein1_Nr = e.Value.ToString();
            }
            else
            {
                DisplayErrorVerein = "Block";
                return;
            }

            DisplayElements = "block";
            StateHasChanged();
        }

        public void PositionChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Position = e.Value.ToString();

                if (Position == "1")
                {
                    Kader.PositionsNr = 1;
                    Kader.Position = "Torhüter";
                }                    
                else if (Position == "2")
                {
                    Kader.PositionsNr = 2;
                    Kader.Position = "Abwehr";
                }
                else if (Position == "3")
                {
                    Kader.PositionsNr = 2;
                    Kader.Position = "Abwehr";
                }
                else if (Position == "4")
                {
                    Kader.PositionsNr = 4;
                    Kader.Position = "Sturm";
                }
                else
                {
                    Kader.PositionsNr =null;
                    Kader.Position = "";
                }
                
                Kader.VereinID = Convert.ToInt32(Verein1_Nr);

                StateHasChanged();
            }
        }
    }    
}

public class DisplayKaderVerein
{
    public DisplayKaderVerein(string vereinID, string vereinname, string ort)
    {
        VereinID = vereinID;
        Vereinname1 = vereinname;
        Ort = ort;
    }
    public string VereinID { get; set; }
    public string Vereinname1 { get; set; }
    public string Ort { get; set; }
}
