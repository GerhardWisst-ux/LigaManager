using LigaManagement.Api.Models;
using LigaManagement.Models;
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
        public IEnumerable<int> values = new int[] { 1, 2, 3, 4, 5};
        public List<int> TabellenList;
        public int SaisonID;
        public int LigaID;

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
        public ISpieltagService SpieltagService { get; set; }

        [Inject]
        public ILigaService LigaService { get; set; }

        public IEnumerable<Saison> Saisonen { get; set; }

        public IEnumerable<Liga> Ligen { get; set; }

        public IEnumerable<Spieltag> Spieltage { get; set; }
        protected override async Task OnInitializedAsync()
        {
            SaisonenList = new List<DisplaySaison>();
            Saisonen = (await SaisonenService.GetSaisonen()).ToList();

            for (int i = 0; i < Saisonen.Count(); i++)
            {
                var columns = Saisonen.ElementAt(i);
                SaisonenList.Add(new DisplaySaison(columns.SaisonID, columns.Saisonname));
            }

            LigenList = new List<DisplayLiga>();
            Ligen = (await LigaService.GetLigen()).ToList();

            for (int i = 0; i < Ligen.Count(); i++)
            {
                var columns = Ligen.ElementAt(i);
                LigenList.Add(new DisplayLiga(columns.Id, columns.Liganame));
            }

            if (Globals.SaisonID == 0)
                Globals.bVisibleNavMenuElements = false;
            else
                Globals.bVisibleNavMenuElements = true;

            DisplayErrorLiga = "none";
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
                Globals.currentSaison = e.Value.ToString();
                Globals.currentPokalSaison = Globals.currentSaison;
                Globals.SaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).SaisonID;
                Globals.PokalSaisonID = Globals.SaisonID;
                Globals.KaderSaisonID = Globals.SaisonID;
            }
        }
        public void OnSaisonChange(object value)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            Globals.currentSaison = str.ToString();
            Globals.SaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).SaisonID;
            Globals.KaderSaisonID = Globals.SaisonID;

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
            //sFilename = "C:\\Users\\gwiss\\source\\repos\\Ligamanager\\Data\\2023.csv";

            string sFilename = @"C:\Users\gwiss\source\repos\Ligamanager\Data\1997.csv";
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

                        for (int i = 0; i < 12; i++)
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
                Console.WriteLine("Datei zum Einlesen nicht gefunden:");
                Console.WriteLine(ex.Message);
            }

            return importedData;
        }

        private async void SaveImportDataToDatabase(DataTable imported_data)
        {

            try
            {
                Vereine = await VereineService.GetVereine();

                using (SqlConnection conn = new SqlConnection(Globals.connstring))
                {
                    int i = 1;
                    int spieltag = 1;
                    conn.Open();
                    foreach (DataRow importRow in imported_data.Rows)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO spieltage(Saison,SpieltagNr,Verein1,Verein2,Verein1_Nr,Verein2_Nr, Tore1_Nr, Tore2_Nr, Ort,Datum,Abgeschlossen,SaisonID,LigaID) " +
                                                          "VALUES (@Saison,@SpieltagNr, @Verein1,@Verein2,@Verein1_Nr,@Verein2_Nr,@Tore1_Nr, @Tore2_Nr,@Ort,@Datum, @Abgeschlossen,@SaisonID,@LigaID)", conn);
                        cmd.Parameters.AddWithValue("@Saison", "1997/98"); /*String.Concat(sFilename.Substring(0, 4), "/", (Convert.ToInt32(sFilename.Substring(0, 4)) + 1).ToString())); ;*/
                        cmd.Parameters.AddWithValue("@SpieltagNr", spieltag);
                        cmd.Parameters.AddWithValue("@Verein1", importRow["Hometeam"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@Verein2", importRow["AwayTeam"].ToString().Trim());

                        int iVerein1 = 0;
                        int iVerein2 = 0;
                        string sStadion = "";
                        for (int j = 0; j < Vereine.Count(); j++)
                        {
                            var columns = Vereine.ElementAt(j);

                            iVerein1 = Vereine.FirstOrDefault(a => a.Vereinsname1 == (importRow["Hometeam"].ToString().Trim())).VereinNr;
                            iVerein2 = Vereine.FirstOrDefault(a => a.Vereinsname1 == (importRow["AwayTeam"].ToString().Trim())).VereinNr;
                            sStadion = Vereine.FirstOrDefault(a => a.Vereinsname1 == (importRow["Hometeam"].ToString().Trim())).Stadion;
                            break;
                        }

                        cmd.Parameters.AddWithValue("@Verein1_Nr", iVerein1);
                        cmd.Parameters.AddWithValue("@Verein2_Nr", iVerein2);

                        cmd.Parameters.AddWithValue("@Tore1_Nr", Convert.ToInt32(importRow["FTHG"]));
                        cmd.Parameters.AddWithValue("@Tore2_Nr", Convert.ToInt32(importRow["FTAG"]));
                        cmd.Parameters.AddWithValue("@Ort", sStadion);
                        //cmd.Parameters.AddWithValue("@Zuschauer", Convert.ToInt32(importRow["Attendance"].ToString()));
                        //cmd.Parameters.AddWithValue("@Schiedrichter", importRow["Referee"].ToString());
                        cmd.Parameters.AddWithValue("@SaisonID", 25);
                        cmd.Parameters.AddWithValue("@LigaID", 1);


                        //DateTime time = Convert.ToDateTime(importRow["Time"]);
                        DateTime dt = new DateTime(Convert.ToInt32(importRow["Date"].ToString().Substring(6, 4)),
                                                Convert.ToInt32(importRow["Date"].ToString().Substring(3, 2)),
                                                Convert.ToInt32(importRow["Date"].ToString().Substring(0, 2)), 15, 30, 0);

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
            if (Globals.currentSaison == null & Globals.currentLiga == null)
            {
                DisplayErrorLiga = "block";
                DisplayErrorSaison = "block";
                return;
            }

            if (Globals.currentSaison == null)
            {
                DisplayErrorSaison = "block";
                DisplayErrorLiga = "none";
                return;
            }

            if (Globals.currentLiga == null)
            {
                DisplayErrorLiga = "block";
                DisplayErrorSaison = "none";
                return;
            }

            DisplayErrorLiga = "none";
            DisplayErrorSaison = "none";

            Vereine = await VereineService.GetVereine();
            Spieltage = (await SpieltagService.GetSpieltage());

            Spieltage = (await SpieltagService.GetSpieltage()).Where(st => st.Saison == Globals.currentSaison).ToList();
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

            if (Globals.currentSaison.StartsWith("1963") || Globals.currentSaison.StartsWith("1964"))
                Globals.maxSpieltag = 30;
            else if (Globals.currentSaison.StartsWith("1991"))
                Globals.maxSpieltag = 38;
            else
                Globals.maxSpieltag = 34;

            SpieltageRepository rep = new SpieltageRepository(appDbContext);

            Globals.SaisonID = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).SaisonID;

            bool bAbgeschlossen = Saisonen.FirstOrDefault(x => x.Saisonname == Globals.currentSaison).Abgeschlossen;

            int iAktSpieltag;
            if (bAbgeschlossen)
            {
                iAktSpieltag = Globals.maxSpieltag;
                Globals.Spieltag = iAktSpieltag;
            }
            else
            {
                iAktSpieltag = rep.AktSpieltag(Globals.SaisonID);
                Globals.Spieltag = iAktSpieltag;
            }

            Globals.bVisibleNavMenuElements = true;
            NavigationManager.NavigateTo($"spieltage/{iAktSpieltag}", true);
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
                    else if (i == 9 && TabellenList.Contains(10))
                        SQLScript = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Pokalergebnisse.sql");

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
                    //NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Info, Summary = "Anlage Datenbank und Erzeugen Tabellen", Detail = "Angelegt" });
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

        public void GenerateDataBaseTables()
        {
            string script = string.Empty;

            for (int i = 1; i <= 4; i++)
            {
                if (i == 1)
                    script = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Delete.sql");
                else if (i == 2)
                    script = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\Spieler.sql");
                else if (i == 3)
                    script = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\VereineSaison.sql");
                else if (i == 4)
                    script = File.ReadAllText(@"C:\Users\gwiss\source\repos\Ligamanager\LigaManagement.Models\SQL\SpielerSpieltag.sql");

                // split script on GO command
                IEnumerable<string> commandStrings = Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

                using (SqlConnection conn = new SqlConnection(Globals.connstring))
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
    }
}



