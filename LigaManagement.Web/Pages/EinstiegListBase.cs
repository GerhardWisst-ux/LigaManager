using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using LigaManagerManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static LigaManagement.Web.Pages.EM_WMListbase;

namespace LigaManagement.Web.Pages
{
    [EnableCors]
    public class EinstiegListBase : ComponentBase
    {
        static HttpClient client = new HttpClient();
        protected string sFilename;
        protected string DisplayErrorLiga = "none";
        protected string DisplayErrorSaison = "none";
        protected string DisplayErrorSaisonEMWM = "none";
        protected string DisplayErrorLand = "none";

        protected bool ImportVisible = false;
        protected bool TabellenAnlegenVisible = false;
        protected bool isDropdownDisabledLiga = true;
        protected bool isDropdownDisabledSaison = true;

        public RadzenDataGrid<Spieltag> spieltageGrid;
        public RadzenDataGrid<Spieltag> grid;
        IList<Tuple<Spieltag, RadzenDataGridColumn<Spieltag>>> selectedCellData = new List<Tuple<Spieltag, RadzenDataGridColumn<Spieltag>>>();

        public IEnumerable<int> values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public List<int> TabellenList;
        public User CurrentLMUser { get; private set; } = new();
        public int LandID;
        public int LigaID;
        public int SaisonID;

        [BindProperty]
        public string ImportCSV { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        public List<DisplaySaison> SaisonenList;

        public List<DisplaySaison> SaisonenListEMWM;

        public List<DisplayLiga> LigenList;

        public List<DisplayLaender> LaenderList;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISaisonenCLService SaisonenEMWMService { get; set; }
        public IEnumerable<Verein> Vereine { get; set; }

        public IEnumerable<VereinAUS> VereineAUS { get; set; }

        [Inject]
        public IVereineService VereineService { get; set; }

        [Inject]
        public IVereinePLService VereineServicePL { get; set; }

        [Inject]
        public IVereineITService VereineServiceIT { get; set; }

        [Inject]
        public IVereineFRService VereineServiceFR { get; set; }

        [Inject]
        public IVereineESService VereineServiceES { get; set; }

        [Inject]
        public IVereineNLService VereineServiceNL { get; set; }

        [Inject]
        public IVereinePTService VereineServicePT { get; set; }

        [Inject]
        public IVereineTUService VereineServiceTU { get; set; }

        [Inject]
        public IVereineBEService VereineServiceBE { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public ISpieltagServiceLE SpieltagServiceLE { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        [Inject]
        public ILandService LaenderService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        public IEnumerable<Liga> Ligen { get; set; }

        public IEnumerable<Land> Laender { get; set; }

        public IEnumerable<Spieltag> Spieltage { get; set; }

        public IEnumerable<Spieltag> LetzteErgebnisse { get; set; }

        [Inject]
        public IStringLocalizer<EinstiegList> Localizer { get; set; }

        public bool TestSQLServer()
        {
            try
            {
                Console.WriteLine("Connecting to: {0}", Globals.connstring);
                using (var connection = new SqlConnection(Globals.connstring))
                {
                    var query = "select 1";
                    Console.WriteLine("Executing: {0}", query);

                    var command = new SqlCommand(query, connection);

                    connection.Open();
                    Console.WriteLine("SQL Connection successful.");

                    command.ExecuteScalar();
                    Console.WriteLine("SQL Query execution successful.");
                }

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return false;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {

                if (TestSQLServer() == false)
                    return;

                LaenderList = new List<DisplayLaender>();
                Laender = (await LaenderService.GetLaender()).ToList();

                for (int i = 0; i < Laender.Count(); i++)
                {
                    var columns = Laender.ElementAt(i);
                    LaenderList.Add(new DisplayLaender(columns.Aktiv, columns.Id, columns.Laendername));
                }

                LigenList = new List<DisplayLiga>();
                Ligen = (await LigaService.GetLigen()).ToList().Where(x => x.LandID == Globals.LandID);

                for (int i = 0; i < Ligen.Count(); i++)
                {
                    var columns = Ligen.ElementAt(i);
                    LigenList.Add(new DisplayLiga(columns.Aktiv, columns.Id, columns.LandID, columns.Liganame, columns.EMWM));
                }

                if (Globals.SaisonID == 0)
                    Globals.bVisibleNavMenuElements = false;
                else
                    Globals.bVisibleNavMenuElements = true;

                SaisonenList = new List<DisplaySaison>();
                Saisonen = (await SaisonenService.GetSaisonen()).Where(x => x.LigaID == Globals.LigaID && x.LandID == Globals.LandID).ToList();
                if (Globals.LigaNummer == 0)
                {
                    SaisonenList.Clear();
                    isDropdownDisabledSaison = false;
                }
                else
                {
                    for (int i = 0; i < Saisonen.Count(); i++)
                    {
                        var columns = Saisonen.ElementAt(i);
                        Globals.currentLiga = Saisonen.ElementAt(0).Liganame;
                        Globals.currentLigaUrl = Globals.currentLiga;
                        SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                    }

                    isDropdownDisabledLiga = false;
                    isDropdownDisabledSaison = false;
                }


                SaisonenListEMWM = new List<DisplaySaison>();


                Saisonen = (await SaisonenEMWMService.GetSaisonen()).Where(x => x.Saisonname.StartsWith("WM") || x.Saisonname.StartsWith("EM")).ToList().OrderByDescending(x => x.Saisonname.Substring(x.Saisonname.Length - 4));

                for (int i = 0; i < Saisonen.Count(); i++)
                {
                    var columns = Saisonen.ElementAt(i);
                    SaisonenListEMWM.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                }


                if (Globals.SaisonID == 0)
                    isDropdownDisabledSaison = true;
                else
                    isDropdownDisabledSaison = false;

                if (LMSettings.GetImportVisible() == false)
                    ImportVisible = false;
                else if (LMSettings.GetImportVisible() == true)
                    ImportVisible = true;

                if (LMSettings.GetTabellenAnlegenVisible() == false)
                    TabellenAnlegenVisible = false;
                else if (LMSettings.GetTabellenAnlegenVisible() == true)
                    TabellenAnlegenVisible = true;

                LetzteErgebnisse = await SpieltagServiceLE.GetSpieltage();

                DisplayErrorLiga = "none";
                DisplayErrorSaison = "none";
                DisplayErrorSaisonEMWM = "none";
                DisplayErrorLand = "none";
                                
                StateHasChanged();
                //var result = await GetDataFromOpenLgaDB();
            }
            catch (Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        public void ValidateItems(IEnumerable args)
        {
            TabellenList = (List<int>)args;
        }

        public void SaisonChange(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == "0")
                    return;

                Globals.currentSaison = e.Value.ToString();
                Globals.currentPokalSaison = Globals.currentSaison;
                Globals.currentCLSaison = Globals.currentSaison;

                if (Saisonen == null || Globals.currentSaison == null)
                    throw new Exception("Saisonen null oder Globals.currentSaison");

                if (Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison && x.LigaID == Globals.LigaID) != null)
                {
                    Globals.SaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison && x.LigaID == Globals.LigaID).SaisonID;
                    Globals.CLSaisonID = Globals.SaisonID;
                    Globals.CLPokalSaisonID = Globals.SaisonID;
                    Globals.KaderSaisonID = Globals.SaisonID;
                }
                else
                {
                    Globals.SaisonID = 1;
                    Globals.CLPokalSaisonID = 1;
                    Globals.CLSaisonID = 1;
                    Globals.KaderSaisonID = 1;
                }


                StateHasChanged();
            }
        }

        public async void SaisonChangeEMWM(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.ToString() == "0")
                    return;

                Globals.currentEMWMSaison = e.Value.ToString();

                Saisonen = (await SaisonenEMWMService.GetSaisonen()).Where(x => x.Saisonname.StartsWith("WM") || x.Saisonname.StartsWith("EM")).ToList().OrderByDescending(x => x.Saisonname.Substring(x.Saisonname.Length - 4));

                if (Saisonen == null || Globals.currentEMWMSaison == null)
                    throw new Exception("Saisonen is null oder Globals.currentEMWMSaison is null");

                if (Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison && x.LigaID == Globals.LigaID) != null)
                {
                    Globals.EMWMSaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison && x.LigaID == Globals.LigaID).SaisonID;
                }
                else
                {
                    Globals.EMWMSaisonID = 1;
                }

                StateHasChanged();
            }
        }


        public async Task LandChangeAsync(ChangeEventArgs e)
        {
            if (e.Value != null)
            {
                Globals.LandID = Convert.ToInt32(e.Value);
                var land = await LaenderService.GetLand(Globals.LandID);
                Globals.currentLand = land.Laendername;

                LigenList = new List<DisplayLiga>();
                Ligen = (await LigaService.GetLigen()).ToList().Where(x => x.LandID == Globals.LandID && x.EMWM == false);

                for (int i = 0; i < Ligen.Count(); i++)
                {
                    var columns = Ligen.ElementAt(i);
                    LigenList.Add(new DisplayLiga(columns.Aktiv, columns.Id, columns.LandID, columns.Liganame, columns.EMWM));
                }

                isDropdownDisabledLiga = false;

                StateHasChanged();
            }
        }

        public async Task LigaChangeAsync(ChangeEventArgs e)
        {
            try
            {
                string sCurrentliga = "";
                if (e.Value != null)
                {

                    Globals.LigaID = Convert.ToInt32(e.Value);

                    SaisonenList = new List<DisplaySaison>();
                    Saisonen = (await SaisonenService.GetSaisonen()).Where(x => x.LigaID == Globals.LigaID && x.LandID == Globals.LandID).ToList();

                    var liga = await LigaService.GetLiga(Globals.LigaID);

                    Globals.LigaNummer = liga.Liganummer;
                    Globals.currentLiganame = liga.Liganame;

                    sCurrentliga = liga.Liganame;

                    if (Globals.LigaID == 0)
                    {
                        SaisonenList.Clear();
                        isDropdownDisabledSaison = false;
                    }
                    else
                    {
                        for (int i = 0; i < Saisonen.Count(); i++)
                        {
                            var columns = Saisonen.ElementAt(i);
                            Globals.currentLiga = Saisonen.ElementAt(0).Liganame;
                            Globals.currentLigaUrl = sCurrentliga;
                            SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
                        }

                        isDropdownDisabledSaison = false;
                    }

                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {

                Debug.Print(ex.StackTrace);
            }
        }

        public void OnClickHandlerImport()
        {
            try
            {
                //if (ImportCSV == null)
                //    return;

                DataTable imported_data = GetDataFromFile();

                if (imported_data == null)
                    return;

                SaveImportDataToDatabase(imported_data);
            }
            catch (Exception ex)
            {

                Debug.Print(ex.StackTrace);
            }
        }

        private DataTable GetDataFromFile()
        {
            DataTable importedData = new DataTable();

            string sFilename = @"C:\Users\gwiss\source\repos\Ligamanager\Data\2023_IT.csv";
            if (File.Exists(sFilename))
                Console.WriteLine("Datei existiert");
            else
                Console.WriteLine("Datei existiert nicht");

            try
            {
                using (StreamReader sr = new StreamReader(sFilename, Encoding.GetEncoding("iso-8859-1")))
                {
                    string header = sr.ReadLine();
                    if (string.IsNullOrEmpty(header))
                    {

                        return null;
                    }

                    string[] headerColumns = header.Split(',');
                    foreach (string headerColumn in headerColumns)
                    {
                        importedData.Columns.Add(headerColumn);
                    }

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line)) continue;
                        string[] fields = line.Split(',');
                        DataRow importedRow = importedData.NewRow();

                        for (int i = 0; i < 7; i++)
                        {
                            if (i == 160)
                                Debug.Print(i + ": " + fields[i]);

                            importedRow[i] = fields[i];

                        }

                        importedData.Rows.Add(importedRow);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Einlesen:" + ex.Message);
            }

            return importedData;
        }

        private async void SaveImportDataToDatabase(DataTable imported_data)
        {

            try
            {
                if (Globals.LigaNummer == 1 || Globals.LigaNummer == 2 || Globals.LigaNummer == 3)
                {
                    Vereine = await VereineService.GetVereine();
                }

                else if (Globals.LigaNummer == 4 | Globals.LigaNummer == 12)
                {
                    VereineAUS = await VereineServicePL.GetVereine();
                }
                else if (Globals.LigaNummer == 5)
                {
                    VereineAUS = await VereineServiceIT.GetVereine();
                }
                else if (Globals.LigaNummer == 6)
                {
                    VereineAUS = await VereineServiceFR.GetVereine();
                }
                else if (Globals.LigaNummer == 7)
                {
                    VereineAUS = await VereineServiceES.GetVereine();
                }
                else if (Globals.LigaNummer == 8)
                {
                    VereineAUS = await VereineServiceNL.GetVereine();
                }
                else if (Globals.LigaNummer == 9)
                {
                    VereineAUS = await VereineServicePT.GetVereine();
                }
                else if (Globals.LigaNummer == 10)
                {
                    VereineAUS = await VereineServiceTU.GetVereine();
                }
                else if (Globals.LigaNummer == 11)
                {
                    VereineAUS = await VereineServiceBE.GetVereine();
                }

                using (SqlConnection conn = new SqlConnection(Globals.connstring))
                {
                    int i = 1;
                    int spieltag = 1;
                    conn.Open();
                    foreach (DataRow importRow in imported_data.Rows)
                    {

                        SqlCommand cmd = new SqlCommand("INSERT INTO spieltageIT(Saison,SpieltagNr,Verein1,Verein2,Verein1_Nr,Verein2_Nr, Tore1_Nr, Tore2_Nr, Ort,Datum,Abgeschlossen,SaisonID,LigaID,Zuschauer,Schiedrichter) " +
                                                          "VALUES (@Saison,@SpieltagNr,@Verein1,@Verein2,@Verein1_Nr,@Verein2_Nr,@Tore1_Nr,@Tore2_Nr,@Ort,@Datum,@Abgeschlossen,@SaisonID,@LigaID,@Zuschauer,@Schiedrichter)", conn);
                        cmd.Parameters.AddWithValue("@Saison", "2023/24");
                        cmd.Parameters.AddWithValue("@SpieltagNr", spieltag);
                        cmd.Parameters.AddWithValue("@Verein1", importRow["Hometeam"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@Verein2", importRow["AwayTeam"].ToString().Trim());

                        int iVerein1 = 0;
                        int iVerein2 = 0;
                        int iFassungsvermoegen = 0;
                        string sStadion = "";
                        for (int j = 0; j < VereineAUS.Count(); j++)
                        {
                            var columns = VereineAUS.ElementAt(j);

                            iVerein1 = VereineAUS.FirstOrDefault(a => a.Vereinsname1 == (importRow["Hometeam"].ToString().Trim())).VereinNr;
                            iVerein2 = VereineAUS.FirstOrDefault(a => a.Vereinsname1 == (importRow["AwayTeam"].ToString().Trim())).VereinNr;
                            sStadion = VereineAUS.FirstOrDefault(a => a.Vereinsname1 == (importRow["Hometeam"].ToString().Trim())).Stadion;
                            iFassungsvermoegen = Convert.ToInt32(VereineAUS.FirstOrDefault(a => a.Vereinsname1 == (importRow["Hometeam"].ToString().Trim())).Fassungsvermoegen);
                            break;
                        }

                        cmd.Parameters.AddWithValue("@Verein1_Nr", iVerein1);
                        cmd.Parameters.AddWithValue("@Verein2_Nr", iVerein2);

                        cmd.Parameters.AddWithValue("@Tore1_Nr", Convert.ToInt32(importRow["FTHG"]));
                        cmd.Parameters.AddWithValue("@Tore2_Nr", Convert.ToInt32(importRow["FTAG"]));
                        cmd.Parameters.AddWithValue("@Ort", sStadion);

                        //if (!string.IsNullOrEmpty(importRow["Attendance"].ToString()))
                        //    cmd.Parameters.AddWithValue("@Zuschauer", int.Parse(importRow["Attendance"].ToString()));
                        //else
                        cmd.Parameters.AddWithValue("@Zuschauer", iFassungsvermoegen);

                        //if (importRow["Referee"] != null)
                        //    cmd.Parameters.AddWithValue("@Schiedrichter", importRow["Referee"].ToString());
                        //else
                        cmd.Parameters.AddWithValue("@Schiedrichter", "SR");

                        //string time = importRow["Time"].ToString();

                        //DateTime dt = new DateTime(Convert.ToInt32(importRow["Date"].ToString().Substring(6, 4)),
                        //                        Convert.ToInt32(importRow["Date"].ToString().Substring(3, 2)),
                        //                        Convert.ToInt32(importRow["Date"].ToString().Substring(0, 2)), 15, 30, 0);

                        string time = importRow["Time"].ToString();

                        cmd.Parameters.AddWithValue("@SaisonID", 138);
                        cmd.Parameters.AddWithValue("@LigaID", 6);

                        //string time = importRow["Time"].ToString();

                        //DateTime dt = new DateTime(Convert.ToInt32(importRow["Date"].ToString().Substring(6, 4)),
                        //                        Convert.ToInt32(importRow["Date"].ToString().Substring(3, 2)),
                        //                        Convert.ToInt32(importRow["Date"].ToString().Substring(0, 2)), Convert.ToInt32(importRow["Time"].ToString().Substring(0, 2)), Convert.ToInt32(importRow["Time"].ToString().Substring(3, 2)), 0);

                        //string time = importRow["Time"].ToString();

                        DateTime dt = new DateTime(Convert.ToInt32(importRow["Date"].ToString().Substring(6, 4)),
                                                Convert.ToInt32(importRow["Date"].ToString().Substring(3, 2)),
                                                Convert.ToInt32(importRow["Date"].ToString().Substring(0, 2)), Convert.ToInt32(importRow["Time"].ToString().Substring(0, 2)), Convert.ToInt32(importRow["Time"].ToString().Substring(3, 2)), 0);

                        cmd.Parameters.AddWithValue("@Datum", dt);
                        cmd.Parameters.AddWithValue("@Abgeschlossen", true);
                        cmd.ExecuteNonQuery();

                        int mod = i % 10;

                        if (mod == 0)
                            spieltag++;

                        i++;
                    }

                }
            }
            catch (Exception ex)
            {

                Debug.Print(ex.StackTrace);
            }
        }

        public async void OnClickHandlerEMWM()
        {
            if (Globals.currentEMWMSaison == null)
                DisplayErrorSaisonEMWM = "block";
            else
                DisplayErrorSaisonEMWM = "none";

            if (Globals.currentEMWMSaison == null)
                return;

            Globals.EMWMSaisonID = 0;
            NavigationManager.NavigateTo($"Ligamanager/em_wm", true);
        }

        public async void OnClickHandler()
        {
            bool bAbgeschlossen = false;

            if (Globals.LandID == 0)
                DisplayErrorLand = "block";
            else
                DisplayErrorLand = "none";

            if (Globals.currentLiga == null)
                DisplayErrorLiga = "block";
            else
                DisplayErrorLiga = "none";

            if (Globals.currentSaison == null)
                DisplayErrorSaison = "block";
            else
                DisplayErrorSaison = "none";

            if (Globals.currentSaison == null || Globals.currentLiga == null || Globals.LandID == 0)
            {
                return;
            }

            Vereine = await VereineService.GetVereine();
            if (Globals.LigaNummer != 3)
                Spieltage = await SpieltagService.GetSpieltage();
            else
                Spieltage = await SpieltagService.GetSpieltageL3();

            Spieltage = Spieltage.Where(st => st.Saison == Globals.currentSaison && st.LigaID == Globals.LigaID).ToList();

            if (Globals.LigaID != 3)
            {
                for (int j = 0; j < Spieltage.Count(); j++)
                {
                    var columns = Spieltage.ElementAt(j);
                    columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname1;
                    columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr)).Vereinsname2;
                }
            }

            int i = 1;
            foreach (var spieltag in Spieltage)
            {

                if (!Globals.VereinAktSaison.ContainsKey(spieltag.Verein1_Nr))
                {
                    Globals.VereinAktSaison.Add(spieltag.Verein1_Nr, spieltag.Verein1);
                }

                var Verein2 = Globals.VereinAktSaison.FirstOrDefault(x => x.Value == spieltag.Verein2_Nr).Key;

                if (!Globals.VereinAktSaison.ContainsKey(spieltag.Verein2_Nr))
                {
                    Globals.VereinAktSaison.Add(spieltag.Verein2_Nr, spieltag.Verein2);
                }

                i++;
            }

            if (Globals.LigaNummer < 3)
            {
                if (Globals.currentSaison.StartsWith("1963") || Globals.currentSaison.StartsWith("1964"))
                    Globals.maxSpieltag = 30;
                else if (Globals.currentSaison.StartsWith("1991"))
                    Globals.maxSpieltag = 38;
                else
                    Globals.maxSpieltag = 34;

            }
            else if (Globals.LigaNummer == 3)
                Globals.maxSpieltag = 38;
            else if (Globals.LigaNummer == 4)
            {
                if (Globals.currentSaison.StartsWith("1993") || Globals.currentSaison.StartsWith("1994"))
                    Globals.maxSpieltag = 42;
                else
                    Globals.maxSpieltag = 38;
            }
            else if (Globals.LigaNummer == 5)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2003)
                    Globals.maxSpieltag = 38;
                else
                    Globals.maxSpieltag = 34;
            }
            else if (Globals.LigaNummer == 6)
            {
                if (Globals.currentSaison.StartsWith("1993") || Globals.currentSaison.StartsWith("1994"))
                    Globals.maxSpieltag = 38;
                else
                    Globals.maxSpieltag = 34;
            }
            else if (Globals.LigaNummer == 7)
            {
                if (Globals.currentSaison.StartsWith("1995") || Globals.currentSaison.StartsWith("1996"))
                    Globals.maxSpieltag = 42;
                else
                    Globals.maxSpieltag = 38;
            }
            else if (Globals.LigaNummer == 8)
            {
                Globals.maxSpieltag = 34;
            }
            else if (Globals.LigaNummer == 9)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2013)
                    Globals.maxSpieltag = 34;
                else
                    Globals.maxSpieltag = 30;
            }
            else if (Globals.LigaNummer == 10)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2019)
                    Globals.maxSpieltag = 38;
                else
                    Globals.maxSpieltag = 34;
            }
            else if (Globals.LigaNummer == 11)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) < 2020 || Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2022)
                    Globals.maxSpieltag = 30;
                else
                    Globals.maxSpieltag = 34;
            }
            else if (Globals.LigaNummer == 12)
                Globals.maxSpieltag = 46;
            else if (Globals.LigaNummer == 20 || Globals.LigaNummer == 21)
                Globals.maxSpieltag = 34;


            SpieltageRepository rep = new SpieltageRepository();


            if (Saisonen != null)
                bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

            int iAktSpieltag;
            if (bAbgeschlossen)
            {
                iAktSpieltag = Globals.maxSpieltag;
                Globals.Spieltag = iAktSpieltag;
            }
            else
            {
                iAktSpieltag = rep.AktSpieltag(Globals.SaisonID, Globals.LigaID);
                Globals.Spieltag = iAktSpieltag;
            }

            Globals.bVisibleNavMenuElements = true;

            Globals.EMWMSaisonID = 0;
            NavigationManager.NavigateTo($"Ligamanager/{Globals.currentLigaUrl}/spieltage/{iAktSpieltag}", true);

        }

        public void CreateDatabaseIfNotExists(string connectionString, string dbName)
        {
            SqlCommand cmd;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (cmd = new SqlCommand($"If(db_id(N'{dbName}') IS NULL) CREATE DATABASE [{dbName}]", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void GenerateDataBase()
        {
            String connstr;
            SqlConnection myConn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Integrated security=SSPI;database=master;TrustServerCertificate=true;");
            string SQLScript = string.Empty;
            string path = "C:\\TEMP\\Ligamanager";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            connstr = "CREATE DATABASE LIGAMANAGER ON PRIMARY " +
             "(NAME = LIGAMANAGER, " +
             "FILENAME = '" + path + "\\LIGAMANAGER.mdf', " +
             "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
             "LOG ON (NAME = LIGAMANAGER_Log, " +
             "FILENAME = '" + path + "\\LIGAMANAGER.ldf', " +
             "SIZE = 1MB, " +
             "MAXSIZE = 5MB, " +
             "FILEGROWTH = 10%)";

            SqlCommand myCommand = new SqlCommand(connstr, myConn);

            try
            {
                myConn.Open();

                if (!File.Exists(path + "\\LIGAMANAGER.mdf"))
                    myCommand.ExecuteNonQuery();

                for (int i = 1; i <= 10; i++)
                {
                    if (i == 1)
                        SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Ligen.sql");
                    else if (i == 2)
                        SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Saisonen.sql");
                    else if (i == 3)
                        SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Vereine.sql");
                    else if (i == 4)
                        SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\VereineSaison.sql");
                    else if (i == 5)
                        SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Spieltage.sql");
                    else if (i == 6 && TabellenList.Contains(6))
                        SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Kader.sql");
                    else if (i == 7 && TabellenList.Contains(7))
                        SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Spieler.sql");
                    else if (i == 8 && TabellenList.Contains(8))
                        SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\SpielerSpieltag.sql");
                    else if (i == 9 && TabellenList.Contains(9))
                        SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Tore.sql");
                    //else if (i == 10 && TabellenList.Contains(10))
                    //    SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Pokalergebnisse.sql");

                    // split script on GO command
                    IEnumerable<string> commandStrings = Regex.Split(SQLScript, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                    using (SqlConnection conn = new SqlConnection(myConn.ConnectionString))
                    {
                        conn.Open();
                        foreach (string commandString in commandStrings)
                        {
                            if (!string.IsNullOrWhiteSpace(commandString.Trim()))
                            {
                                using (var command = new SqlCommand(commandString, conn))
                                {
                                    int j = command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        protected async Task<int> GetDataFromOpenLgaDB()
        {
            int ret = 0;
            int i = 0;
            int spieltag = 0;
            client.BaseAddress = new Uri("https://api.openligadb.de/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            do
            {
                try
                {
                    var matches = await GetMatchesAsync("getmatchdata/rlno/2019");

                    foreach (var match in matches)
                    {
                        Debug.Print(string.Concat(match.MatchDateTime, ": ", match.Team1.TeamName, " : ", match.Team2.TeamName, match.Team2.TeamName));

                        var matchDetail = await GetMatchAsync("getmatchdata/" + match.MatchID + "");

                        //Debug.Print(string.Concat(matchDetail.LeagueName, ": ", matchDetail.matchResults[1].PointsTeam1, " : ", matchDetail.matchResults[1].PointsTeam2));

                        if (matches.Count() <= 380)
                        {
                            int mod = i % 9;

                            if (mod == 0)
                                spieltag++;

                            SaveImportDataToDatabase(match, matchDetail, spieltag);
                        }

                        i++;

                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                    return ret;

                }

            } while (true);
        }
        static async Task<List<LigaManagement.Models.Match>> GetMatchesAsync(string path)
        {
            try
            {
                List<LigaManagement.Models.Match> matches = null;
                List<LigaManagement.Models.Match2> matches2 = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    string matchstring = await response.Content.ReadAsStringAsync();
                    try
                    {
                        matches = JsonConvert.DeserializeObject<List<LigaManagement.Models.Match>>(matchstring);
                    }
                    catch (Exception ex)
                    {
                        matches2 = JsonConvert.DeserializeObject<List<LigaManagement.Models.Match2>>(matchstring);

                        ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                        return null;
                    }
                }
                return matches;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        static async Task<MatchDetail> GetMatchAsync(string path)
        {
            try
            {
                MatchDetail match = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    string matchstring = await response.Content.ReadAsStringAsync();
                    match = JsonConvert.DeserializeObject<MatchDetail>(matchstring);
                }
                return match;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }
        private void SaveImportDataToDatabase(LigaManagement.Models.Match match, MatchDetail matchdetail, int spieltag)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(Globals.connstring))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO spieltageL3(Saison,SaisonID,SpieltagNr,Verein1,Verein2,Verein1_Nr,Verein2_Nr, Tore1_Nr,Tore2_Nr, Ort,Datum,LigaID,Zuschauer,Schiedrichter,Abgeschlossen,TeamIconUrl1,TeamIconUrl2) " +
                                                      "VALUES (@Saison,@SaisonID,@SpieltagNr, @Verein1,@Verein2,@Verein1_Nr,@Verein2_Nr,@Tore1_Nr,@Tore2_Nr,@Ort,@Datum,@LigaID,@Zuschauer,@Schiedrichter,@Abgeschlossen,@TeamIconUrl1,@TeamIconUrl2)", conn);

                    cmd.Parameters.AddWithValue("@Saison", matchdetail.LeagueSeason + +(Convert.ToInt32(matchdetail.LeagueSeason.ToString().Substring(2, 2)) + 1));
                    cmd.Parameters.AddWithValue("@SaisonID", 391);
                    cmd.Parameters.AddWithValue("@LigaID", 23);
                    cmd.Parameters.AddWithValue("@SpieltagNr", spieltag);

                    cmd.Parameters.AddWithValue("@Verein1", match.Team1.TeamName);
                    cmd.Parameters.AddWithValue("@Verein2", match.Team2.TeamName);
                    cmd.Parameters.AddWithValue("@Verein1_Nr", match.Team1.TeamId);
                    cmd.Parameters.AddWithValue("@Verein2_Nr", match.Team2.TeamId);

                    cmd.Parameters.AddWithValue("@Tore1_Nr", matchdetail.MatchResults[1].PointsTeam1);
                    cmd.Parameters.AddWithValue("@Tore2_Nr", matchdetail.MatchResults[1].PointsTeam2);

                    cmd.Parameters.AddWithValue("@Ort", "k.A.");
                    cmd.Parameters.AddWithValue("@Zuschauer", 0);
                    cmd.Parameters.AddWithValue("@TeamIconUrl1", match.Team1.TeamIconUrl);
                    cmd.Parameters.AddWithValue("@TeamIconUrl2", match.Team2.TeamIconUrl);
                    cmd.Parameters.AddWithValue("@Schiedrichter", "k.A.");
                    cmd.Parameters.AddWithValue("@Datum", match.MatchDateTime);
                    cmd.Parameters.AddWithValue("@Abgeschlossen", true);

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            }
        }

        [Bind]
        public class DisplayLiga
        {
            public DisplayLiga(bool aktiv, int ligaID, int landid, string liganame, bool emwm)
            {
                Aktiv = aktiv;
                LigaID = ligaID;
                LandID = landid;
                Liganame = liganame;
                EMWM = emwm;
            }
            public int LandID { get; set; }
            public int LigaID { get; set; }
            public string Liganame { get; set; }
            public bool Aktiv { get; set; }
            public bool EMWM { get; set; }
        }

        [Bind]
        public class DisplayLaender
        {
            public DisplayLaender(bool aktiv, int landID, string laendername)
            {
                Aktiv = aktiv;
                LandID = landID;
                Laendername = laendername;
            }
            public bool Aktiv { get; set; }
            public int LandID { get; set; }
            public string Laendername { get; set; }
        }

        [Bind]
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

    }
}