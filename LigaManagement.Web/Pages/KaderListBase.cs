﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LigaManagement.Models;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
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

        
        protected override async Task OnInitializedAsync()
        {
            if (Id != null)
            {
                Kader = await KaderService.GetSpieler(Convert.ToInt32(Id));
                
                //VereinChange(new ChangeEventArgs { Value = Verein1_Nr });
            }

            var verein = await VereineService.GetVerein(Globals.currentVereinID);
            Vereinsname1 = verein.Vereinsname1;
            Verein1_Nr = verein.VereinNr.ToString();

            var spiele = await SpieltagService.GetSpieltage();
            List<Spieltag> spiele2 = spiele.Where(x => x.Saison == Globals.currentSaison && x.SpieltagNr == "1").Take(9).ToList();

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

                if (Position == "Torhüter")
                    Kader.PositionsNr = 1;
                else if (Position == "Abwehr")
                    Kader.PositionsNr = 2;
                else if (Position == "Mittelfeld")
                    Kader.PositionsNr = 3;
                else if (Position == "Sturm")
                    Kader.PositionsNr = 4;
                else 
                    Kader.PositionsNr = -999;

                Kader.Position = Position;
                Kader.VereinID = Convert.ToInt32( Verein1_Nr);
            }
                        
            StateHasChanged();
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