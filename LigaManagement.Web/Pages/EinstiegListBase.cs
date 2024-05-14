using LigaManagement.Models;
using LigaManagement.Web.Classes;
using LigaManagement.Web.Services.Contracts;
using Ligamanager.Components;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Radzen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LigaManagement.Web.Pages
{
    public class EinstiegListBase : ComponentBase
    {
        protected string sFilename;
        protected string DisplayErrorLiga = "none";
        protected string DisplayErrorSaison = "none";
        protected string DisplayErrorLand = "none";
        protected bool isDropdownDisabledLiga = true;
        protected bool isDropdownDisabledSaison = true;

        public IEnumerable<int> values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public List<int> TabellenList;

        public int LandID;
        public int LigaID;
        public int SaisonID;

        [CascadingParameter]
        public Task<AuthenticationState> authenticationStateTask { get; set; }

        [BindProperty]
        public string ImportCSV { get; set; }

        [Inject]
        public ISaisonenService SaisonenService { get; set; }

        NotificationService NotificationService;

        public List<DisplaySaison> SaisonenList;

        public List<DisplayLiga> LigenList;

        public List<DisplayLaender> LaenderList;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

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
        public IVereineFRService VereineServiceES { get; set; }

        [Inject]
        public IVereineNLService VereineServiceNL { get; set; }

        [Inject]
        public IVereinePTService VereineServicePT { get; set; }

        [Inject]
        public IVereineTUService VereineServiceTU { get; set; }

        [Inject]
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        [Inject]
        public ILandService LaenderService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        public IEnumerable<Liga> Ligen { get; set; }

        public IEnumerable<Land> Laender { get; set; }

        public IEnumerable<Spieltag> Spieltage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {

                LaenderList = new List<DisplayLaender>();
                Laender = (await LaenderService.GetLaender()).ToList();

                for (int i = 0; i < Laender.Count(); i++)
                {
                    var columns = Laender.ElementAt(i);
                    LaenderList.Add(new DisplayLaender(columns.Aktiv, columns.Id, columns.Laendername));
                }

             
                if (Globals.SaisonID == 0)
                    Globals.bVisibleNavMenuElements = false;
                else
                    Globals.bVisibleNavMenuElements = true;

                Globals.currentLiga = "";
                Globals.LandID = 0;
                Globals.LigaID = 0;
                isDropdownDisabledLiga = true;
                isDropdownDisabledLiga = true;
                DisplayErrorLiga = "none";
                DisplayErrorSaison = "none";
                DisplayErrorLand = "none";
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

                if (Saisonen == null || Globals.currentSaison == null)
                    throw new Exception("Saisonen null oder Globals.currentSaison");

                if (Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison && x.LigaID == Globals.LigaID) != null)
                {
                    Globals.SaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison && x.LigaID == Globals.LigaID).SaisonID;
                    Globals.PokalSaisonID = Globals.SaisonID;
                    Globals.KaderSaisonID = Globals.SaisonID;
                }
                else
                {
                    Globals.SaisonID = 1;
                    Globals.PokalSaisonID = 1;
                    Globals.KaderSaisonID = 1;
                }

            }
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

        public async Task LandChangeAsync(ChangeEventArgs e)
        {
            string sCurrentliga = "";
            if (e.Value != null)
            {
                Globals.LandID = Convert.ToInt32(e.Value);

                Globals.currentLand = LaenderService.GetLand(Globals.LandID).ToString();
                LigenList = new List<DisplayLiga>();
                Ligen = (await LigaService.GetLigen()).ToList().Where(x => x.LandID == Globals.LandID);

                for (int i = 0; i < Ligen.Count(); i++)
                {
                    var columns = Ligen.ElementAt(i);
                    LigenList.Add(new DisplayLiga(columns.Aktiv, columns.Id, columns.LandID, columns.Liganame));
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

                    if (Globals.LigaID == 1)
                        sCurrentliga = "Bundesliga";
                    else if (Globals.LigaID == 2)
                        sCurrentliga = "2-Bundesliga";
                    else if (Globals.LigaID == 3)
                        sCurrentliga = "3-Liga";
                    else if (Globals.LigaID == 4)
                        sCurrentliga = "premier-league";
                    else if (Globals.LigaID == 6)
                        sCurrentliga = "serie-a";
                    else if (Globals.LigaID == 7)
                        sCurrentliga = "ligue-1";
                    else if (Globals.LigaID == 8)
                        sCurrentliga = "la-liga";
                    else if (Globals.LigaID == 8)
                        sCurrentliga = "la-liga";
                    else if (Globals.LigaID == 9)
                        sCurrentliga = "Eredivisie";
                    else if (Globals.LigaID == 10)
                        sCurrentliga = "primeira-liga";
                    else if (Globals.LigaID == 11)
                        sCurrentliga = "sueper-lig";

                    if (Globals.LigaID == 0)
                    {
                        SaisonenList.Clear();
                        isDropdownDisabledLiga = false;
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

                        isDropdownDisabledLiga = false;
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
            string sFilename = @"C:\Users\gwiss\source\repos\Ligamanager\Data\2022.csv";
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
                if (Globals.LigaID == 1 || Globals.LigaID == 2 || Globals.LigaID == 3)
                {
                    Vereine = await VereineService.GetVereine();
                }
                else if (Globals.LigaID == 4)
                {
                    VereineAUS = await VereineServicePL.GetVereine();
                }
                else if (Globals.LigaID == 6)
                {
                    VereineAUS = await VereineServiceIT.GetVereine();
                }
                else if (Globals.LigaID == 7)
                {
                    VereineAUS = await VereineServiceFR.GetVereine();
                }
                else if (Globals.LigaID == 8)
                {
                    VereineAUS = await VereineServiceES.GetVereine();
                }
                else if (Globals.LigaID == 9)
                {
                    VereineAUS = await VereineServiceNL.GetVereine();
                }
                else if (Globals.LigaID == 10)
                {
                    VereineAUS = await VereineServicePT.GetVereine();
                }
                else if (Globals.LigaID == 11)
                {
                    VereineAUS = await VereineServiceTU.GetVereine();
                }


                using (SqlConnection conn = new SqlConnection(Globals.connstring))
                {
                    int i = 1;
                    int spieltag = 1;
                    conn.Open();
                    foreach (DataRow importRow in imported_data.Rows)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO spieltage(Saison,SpieltagNr,Verein1,Verein2,Verein1_Nr,Verein2_Nr, Tore1_Nr, Tore2_Nr, Ort,Datum,Abgeschlossen,SaisonID,LigaID,Zuschauer,Schiedrichter) " +
                                                          "VALUES (@Saison,@SpieltagNr,@Verein1,@Verein2,@Verein1_Nr,@Verein2_Nr,@Tore1_Nr,@Tore2_Nr,@Ort,@Datum,@Abgeschlossen,@SaisonID,@LigaID,@Zuschauer,@Schiedrichter)", conn);
                        cmd.Parameters.AddWithValue("@Saison", "2022/23");
                        cmd.Parameters.AddWithValue("@SpieltagNr", spieltag);
                        cmd.Parameters.AddWithValue("@Verein1", importRow["Hometeam"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@Verein2", importRow["AwayTeam"].ToString().Trim());

                        int iVerein1 = 0;
                        int iVerein2 = 0;
                        int iFassungsvermoegen = 0;
                        string sStadion = "";
                        for (int j = 0; j < Vereine.Count(); j++)
                        {
                            var columns = Vereine.ElementAt(j);

                            iVerein1 = Vereine.FirstOrDefault(a => a.Vereinsname1 == (importRow["Hometeam"].ToString().Trim())).VereinNr;
                            iVerein2 = Vereine.FirstOrDefault(a => a.Vereinsname1 == (importRow["AwayTeam"].ToString().Trim())).VereinNr;
                            sStadion = Vereine.FirstOrDefault(a => a.Vereinsname1 == (importRow["Hometeam"].ToString().Trim())).Stadion;
                            iFassungsvermoegen = Convert.ToInt32(Vereine.FirstOrDefault(a => a.Vereinsname1 == (importRow["Hometeam"].ToString().Trim())).Fassungsvermoegen);
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

                        cmd.Parameters.AddWithValue("@SaisonID", 2);
                        cmd.Parameters.AddWithValue("@LigaID", 1);

                        // string time = importRow["Time"].ToString();

                        //DateTime dt = new DateTime(Convert.ToInt32(importRow["Date"].ToString().Substring(6, 4)),
                        //                        Convert.ToInt32(importRow["Date"].ToString().Substring(3, 2)),
                        //                        Convert.ToInt32(importRow["Date"].ToString().Substring(0, 2)), 15, 30, 0);

                        string time = importRow["Time"].ToString();

                        DateTime dt = new DateTime(Convert.ToInt32(importRow["Date"].ToString().Substring(6, 4)),
                                                Convert.ToInt32(importRow["Date"].ToString().Substring(3, 2)),
                                                Convert.ToInt32(importRow["Date"].ToString().Substring(0, 2)), Convert.ToInt32(importRow["Time"].ToString().Substring(0, 2)), Convert.ToInt32(importRow["Time"].ToString().Substring(3, 2)), 0);

                        cmd.Parameters.AddWithValue("@Datum", dt);
                        cmd.Parameters.AddWithValue("@Abgeschlossen", true);
                        cmd.ExecuteNonQuery();

                        int mod = i % 9;

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


        public async void OnClickHandler()
        {
            if (Globals.LandID == 0)
                DisplayErrorLand = "block";
            else
                DisplayErrorLand = "none";

            if (Globals.currentLiga == "")
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
            Spieltage = await SpieltagService.GetSpieltage();

            Spieltage = Spieltage.Where(st => st.Saison == Globals.currentSaison && st.LigaID == Globals.LigaID).ToList();
            for (int j = 0; j < Spieltage.Count(); j++)
            {
                var columns = Spieltage.ElementAt(j);
                columns.Verein1 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein1_Nr)).Vereinsname1;
                columns.Verein2 = Vereine.FirstOrDefault(a => a.VereinNr == Convert.ToInt32(columns.Verein2_Nr)).Vereinsname2;
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

            if (Globals.LigaID < 4)
            {
                if (Globals.currentSaison.StartsWith("1963") || Globals.currentSaison.StartsWith("1964"))
                    Globals.maxSpieltag = 30;
                else if (Globals.currentSaison.StartsWith("1991"))
                    Globals.maxSpieltag = 38;
                else
                    Globals.maxSpieltag = 34;

            }
            else if (Globals.LigaID == 4)
            {
                if (Globals.currentSaison.StartsWith("1993") || Globals.currentSaison.StartsWith("1994"))
                    Globals.maxSpieltag = 42;
                else
                    Globals.maxSpieltag = 38;
            }
            else if (Globals.LigaID == 6)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2003)
                    Globals.maxSpieltag = 38;
                else
                    Globals.maxSpieltag = 34;
            }
            else if (Globals.LigaID == 7)
            {
                if (Globals.currentSaison.StartsWith("1993") || Globals.currentSaison.StartsWith("1994"))
                    Globals.maxSpieltag = 38;
                else
                    Globals.maxSpieltag = 34;
            }
            else if (Globals.LigaID == 8)
            {
                if (Globals.currentSaison.StartsWith("1995") || Globals.currentSaison.StartsWith("1996"))
                    Globals.maxSpieltag = 42;
                else
                    Globals.maxSpieltag = 38;
            }
            else if (Globals.LigaID == 9)
            {
                Globals.maxSpieltag = 34;
            }
            else if (Globals.LigaID == 10)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2013)
                    Globals.maxSpieltag = 34;
                else
                    Globals.maxSpieltag = 30;
            }
            else if (Globals.LigaID == 11)
            {
                if (Convert.ToInt32(Globals.currentSaison.Substring(0, 4)) > 2019)
                    Globals.maxSpieltag = 38;
                else
                    Globals.maxSpieltag = 34;
            }

            SpieltageRepository rep = new SpieltageRepository();

            bool bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

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

            NavigationManager.NavigateTo($"Ligamanager/{Globals.currentLigaUrl}/spieltage/{iAktSpieltag}", true);
        }

        public void CreateDatabaseIfNotExists(string connectionString, string dbName)
        {
            SqlCommand cmd = null;
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
            catch (System.Exception ex)
            {
                Debug.Print(ex.Message);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        [Bind]
        public class DisplayLiga
        {
            public DisplayLiga(bool aktiv, int ligaID, int landid, string liganame)
            {
                Aktiv = aktiv;
                LigaID = ligaID;
                LandID = landid;
                Liganame = liganame;
            }          
            public int LandID { get; set; }
            public int LigaID { get; set; }
            public string Liganame { get; set; }
            public bool Aktiv { get; set; }
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



