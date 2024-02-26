using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Pages
{
    public class EditKaderSpielerBase : ComponentBase
    {
        [Inject]
        public IKaderService KaderService { get; set; }
        public IEnumerable<Kader> KaderList { get; set; }
        public Kader Kader { get; set; } = new Kader();

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }
        [Inject]
        public ISpieltagService SpieltagService { get; set; }
        public List<DisplayVerein> VereineList = new List<DisplayVerein>();

        public string Verein1_Nr;
        public IEnumerable<Verein> Vereine { get; set; }

        protected string DisplayErrorVerein = "none";

        protected override async Task OnInitializedAsync()
        {
            if (Id != null)
            {
                Kader = await KaderService.GetSpieler(Convert.ToInt32(Id));
                Verein1_Nr = Kader.VereinID.ToString();
                //VereinChange(new ChangeEventArgs { Value = Verein1_Nr });
            }

            var spiele = await SpieltagService.GetSpieltage();
            List<Spieltag> spiele2 = spiele.Where(x => x.Saison == Globals.currentSaison && x.SpieltagNr == "1").Take(9).ToList();

            Vereine = (await VereineService.GetVereine()).ToList();
            VereineList = new List<DisplayVerein>();

            int iAnzahl = spiele2.Count() * 2;

            for (int i = 0; i < spiele2.Count(); i++)
            {
                VereineList.Add(new DisplayVerein(spiele2[i].Verein1_Nr, spiele2[i].Verein1));
                VereineList.Add(new DisplayVerein(spiele2[i].Verein2_Nr, spiele2[i].Verein2));
            }            
        }

        public void VereinChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Verein1_Nr = e.Value.ToString();              
            }
        }
        
    }
}

