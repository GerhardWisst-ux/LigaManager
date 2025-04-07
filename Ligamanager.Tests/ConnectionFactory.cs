using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString = Globals.connstring;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}

public class SpieltageRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SpieltageRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Spieltag>?> GetAllSpieltage()
    {
        try
        {
            using (var conn = new SqlConnection(Globals.connstring))
            {
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("sp_spieltage", conn);
                command.CommandType = CommandType.StoredProcedure;
                Spieltag spieltag = null;
                List<Spieltag> Spieltaglist = new List<Spieltag>();
                await using (SqlDataReader reader = command.ExecuteReader())

                ////SqlCommand command = new SqlCommand("SELECT * FROM [Spieltage] ", conn);
                ////Spieltag spieltag = null;
                ////List<Spieltag> Spieltaglist = new List<Spieltag>();
                ////using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        spieltag = new Spieltag();

                        spieltag.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        spieltag.SaisonID = int.Parse(reader["SaisonID"].ToString());
                        spieltag.StadionID = int.Parse(reader["StadionID"].ToString());
                        spieltag.LigaID = int.Parse(reader["LigaID"].ToString());
                        spieltag.SpieltagNr = reader["SpieltagNr"].ToString();
                        spieltag.Saison = reader["Saison"].ToString();
                        spieltag.Verein1 = reader["Verein1"].ToString();
                        spieltag.Verein2 = reader["Verein2"].ToString();
                        spieltag.Verein1_Nr = reader["Verein1_Nr"].ToString();
                        spieltag.Verein2_Nr = reader["Verein2_Nr"].ToString();
                        spieltag.Tore1_Nr = int.Parse(reader["Tore1_Nr"].ToString());
                        spieltag.Tore2_Nr = int.Parse(reader["Tore2_Nr"].ToString());
                        spieltag.Datum = DateTime.Parse(reader["Datum"].ToString());
                        spieltag.Ort = reader["Ort"].ToString();
                        spieltag.Schiedrichter = reader["Schiedrichter"].ToString();
                        spieltag.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                        spieltag.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                        spieltag.TeamIconUrl1 = reader["TeamIconUrl1"].ToString();
                        spieltag.TeamIconUrl2 = reader["TeamIconUrl2"].ToString();

                        Spieltaglist.Add(spieltag);
                    }
                }
                return Spieltaglist;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            return null;
        }
    }

    public async Task<IEnumerable<VereineSaison>> GetVereineSaison()
    {
        try
        {
            List<VereineSaison> vereineSaison = new List<VereineSaison>();

            SqlConnection conn = new SqlConnection(Globals.connstring);
            await conn.OpenAsync();

            SqlCommand command = new SqlCommand("SELECT [Id],[VereinNr],[SaisonID],[LigaID] FROM [dbo].[VereineSaison]", conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    VereineSaison verein = new VereineSaison();
                    verein.VereinNr = (int)reader["VereinNr"];
                    verein.SaisonID = (int)reader["SaisonID"];
                    verein.LigaID = (int)reader["LigaID"];

                    vereineSaison.Add(verein);
                }
            }

            conn.Close();
            return vereineSaison;
        }
        catch (Exception ex)
        {

            ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            return null;
        }
    }

    public async Task<IEnumerable<Saison>> GetSaisonen()
    {
        try
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            await conn.OpenAsync();

            SqlCommand command = new SqlCommand("SELECT * FROM [Saisonen] Order by LigaID, Saisonname DESC ", conn);
            Saison saison = null;
            List<Saison> saisonenList = new List<Saison>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    saison = new Saison();

                    saison.SaisonID = (int)reader["saisonID"];
                    saison.LigaID = (int)reader["LigaID"];
                    saison.LandID = (int)reader["landID"];
                    saison.Saisonname = reader["Saisonname"].ToString();
                    saison.Liganame = reader["Liganame"].ToString();
                    saison.Ligahoehe = (int)reader["Ligahoehe"];
                    saison.AnzahlVereine = (int)reader["AnzahlVereine"];
                    saison.Aufsteiger = (int)reader["AnzahlAufsteiger"];
                    saison.Absteiger = (int)reader["AnzahlAbsteiger"];
                    saison.CL_League = (int)reader["AnzahlCL_Plaetze"];
                    saison.CF_League = (int)reader["AnzahlCF_Plaetze"];
                    saison.EL_League = (int)reader["AnzahlEL_Plaetze"];
                    saison.Relegation = (int)reader["Anzahl_Relegation"];
                    saison.Abgeschlossen = (bool)reader["Abgeschlossen"];
                    saison.Aktuell = (bool)reader["Aktuell"];

                    saisonenList.Add(saison);
                }
            }
            conn.Close();
            return saisonenList;
        }
        catch (Exception ex)
        {

            ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            return null;
        }
    }
}
