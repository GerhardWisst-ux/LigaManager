using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Pages;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using LigaManagerManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class EditKaderSpielerBase : ComponentBase
    {
        [Inject]
        public IKaderService KaderService { get; set; }
        public IEnumerable<Kader> KaderList { get; set; }
        public Kader Kader { get; set; } = new Kader();
        public bool IsLoading { get; set; } = false;
        public string DisplayElements { get; set; } = "none";
        public string DisplayErrorVerein { get; set; } = "none";
        public string DisplayErrorSaison { get; set; } = "none";
        
        public string Vereinsname1 { get; set; }

        public string Saisonname1 { get; set; }

        public int? PositionsNr { get; set; }

        [Parameter]
        public string Id { get; set; }

        public int LandId { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }
        public List<DisplayKaderVerein> VereineList { get; set; } = new List<DisplayKaderVerein>();

        public List<DisplaySaison> SaisonenList { get; set; } = new List<DisplaySaison>();

        public string Verein1_Nr { get; set; }
        public string Saison_Nr { get; set; }

        public string Position { get; set; }
        public IEnumerable<Verein> Vereine { get; set; }
        public IEnumerable<Saison> Saisonen { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationStateTask { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<KaderBase> Localizer { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsLoading = true;
                var authenticationState = await AuthenticationStateTask;

                if (authenticationState.User.Identity == null || !authenticationState.User.Identity.IsAuthenticated)
                {
                    string returnUrl = WebUtility.UrlEncode($"/Ligamanager");
                    NavigationManager.NavigateTo($"/Ligamanager/account/login?returnUrl={returnUrl}");
                    return;
                }               
                await LoadVereineData();
                await LoadSaisonenData();
                IsLoading = false;
            }
            catch (Exception ex)
            {

                IsLoading = false;
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }

        }

        private async Task LoadVereineData()
        {
            try
            {
                var verein = await VereineService.GetVerein(Globals.KaderVereinNr);
                Vereinsname1 = verein.Vereinsname1;
                Verein1_Nr = verein.VereinNr.ToString();

                Vereine = (await VereineService.GetVereine()).ToList();
                var vereineSaison = await VereineService.GetVereineSaison();

                VereineList = vereineSaison
                    .Where(v => v.SaisonID == Globals.SaisonID)
                    .Select(v => new DisplayKaderVerein(v.VereinNr.ToString(), v.Vereinsname1, v.Stadion))
                    .ToList();

                DisplayElements = "none";
                DisplayErrorSaison = "none";
                Globals.bVisibleNavMenuElements = true;
            }
            catch (Exception ex)
            {

                IsLoading = false;
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        private async Task LoadSaisonenData()
        {

            try
            {
                if (Id is null or "0")
                {
                    var saisonen = (await SaisonenService.GetSaisonen()).ToList();
                    saisonen = saisonen.OrderByDescending(x => x.Saisonname).ToList();

                    SaisonenList.Clear();
                    foreach (Saison item in saisonen)
                    {
                        var saisonname1 = saisonen.Find(v => v.SaisonID == item.SaisonID)?.Saisonname ?? Globals.currentSaison;
                        SaisonenList.Add(new DisplaySaison(item.SaisonID, saisonname1));
                    }

                    SaisonenList = SaisonenList.DistinctBy(i => i.Saisonname).ToList();

                    System.Threading.Thread.Sleep(1000);
                    Saisonname1 = saisonen.Find(v => v.SaisonID == Globals.SaisonID)?.Saisonname ?? Globals.currentSaison;
                }
                else
                {
                    var spieler = await KaderService.GetSpieler(Convert.ToInt32(Id));
                    var kaderSpieler = (await KaderService.GetAllSpieler()).ToList().Where(x => x.SpielerName == spieler.SpielerName);
                    var saisonen = (await SaisonenService.GetSaisonen()).ToList();

                    saisonen = saisonen.OrderBy(x => x.Saisonname).ToList();
                    SaisonenList.Clear();
                    foreach (Kader item in kaderSpieler)
                    {
                        var saisonname1 = saisonen.Find(v => v.SaisonID == item.SaisonId)?.Saisonname ?? Globals.currentSaison;

                        SaisonenList.Add(new DisplaySaison(item.SaisonId, saisonname1));
                    }

                    LandId = saisonen.Find(v => v.SaisonID == Globals.SaisonID)?.LandID ?? Globals.LandID;
                    Saisonname1 = saisonen.Find(v => v.SaisonID == Globals.SaisonID)?.Saisonname ?? Globals.currentSaison;
                }

                DisplayElements = "none";
                DisplayErrorSaison = "none";
                Globals.bVisibleNavMenuElements = true;
            }
            catch (Exception ex)
            {

                IsLoading = false;
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        public void VereinChange(ChangeEventArgs e)
        {
            IsLoading = true;
            Verein1_Nr = e.Value?.ToString() ?? string.Empty;
            DisplayErrorVerein = string.IsNullOrEmpty(Verein1_Nr) ? "block" : "none";
            DisplayElements = "block";
            IsLoading = false;
            StateHasChanged();
        }

        public void SaisonChange(ChangeEventArgs e)
        {
            IsLoading = true;
            Saison_Nr = e.Value?.ToString() ?? string.Empty;
            DisplayErrorSaison = string.IsNullOrEmpty(Saison_Nr) ? "block" : "none";
            DisplayElements = "block";
            IsLoading = false;
            StateHasChanged();
        }

        public void DatumChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                DateTime d = Convert.ToDateTime(e.Value);
            }
        }

        public void PositionChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                IsLoading = true;
                Position = e.Value.ToString();
                UpdateKaderPosition(Position);
                Kader.VereinID = Convert.ToInt32(Verein1_Nr);
                IsLoading = false;
                StateHasChanged();
            }
        }

        private void UpdateKaderPosition(string position)
        {
            switch (position)
            {
                case "1":
                    Kader.PositionsNr = 1;
                    Kader.Position = "Torhüter";
                    break;
                case "2":
                    Kader.PositionsNr = 2;
                    Kader.Position = "Abwehr";
                    break;
                case "3":
                    Kader.PositionsNr = 3;
                    Kader.Position = "Mittelfeld";
                    break;
                case "4":
                    Kader.PositionsNr = 4;
                    Kader.Position = "Sturm";
                    break;
                default:
                    Kader.PositionsNr = null;
                    Kader.Position = string.Empty;
                    break;
            }
        }

        protected async Task<bool> Confirm()
        {
            if (Globals.CurrentRole == "USER" || Globals.CurrentRole == "GUEST")
            {
                await JSRuntime.InvokeVoidAsync("alert", "Sie können diesen Spieler nicht löschen");
                return false;
            }

            var message = Localizer["Möchten Sie diesen Spieler tatsächlich löschen?"].Value;
            var result = await JSRuntime.InvokeAsync<bool>("confirm", new[] { message });

            if (result)
            {
                await KaderService.DeleteSpieler(Convert.ToInt32(Id));
                await JSRuntime.InvokeVoidAsync("alert", "Spieler wurde gelöscht");
            }

            return result;
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
}
