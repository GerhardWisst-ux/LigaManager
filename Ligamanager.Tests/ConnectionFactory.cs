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

public class VereineRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

        public VereineRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Verein> GetVerein(int vereinnr)
    {
        try
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            await conn.OpenAsync();

            SqlCommand command = new SqlCommand("SELECT * FROM [Vereine] Where VereinNr =" + vereinnr, conn);
            Verein verein = null;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    verein = new Verein();

                    verein.Id = int.Parse(reader["Id"].ToString());
                    verein.VereinNr = int.Parse(reader["VereinNr"].ToString());
                    verein.Vereinsname1 = reader["Vereinsname1"].ToString();
                    verein.Vereinsname2 = reader["Vereinsname2"].ToString();
                    verein.Fassungsvermoegen = int.Parse(reader["Fassungsvermoegen"].ToString());
                    verein.Erfolge = reader["Erfolge"].ToString();
                    verein.Stadion = reader["Stadion"].ToString();
                    verein.Gegruendet = int.Parse(reader["Gegruendet"].ToString());
                    verein.Bundesliga = bool.Parse(reader["Bundesliga"].ToString());
                    verein.Pokal = bool.Parse(reader["Pokal"].ToString());
                    verein.Hyperlink = reader["Hyperlink"].ToString();
                    verein.Ort = reader["Ort"].ToString();
                    verein.Strasse = reader["Strasse"].ToString();
                    verein.EMail = reader["EMail"].ToString();
                    verein.Fax = reader["Fax"].ToString();
                    verein.Telefon = reader["Telefon"].ToString();
                    if (!string.IsNullOrEmpty(reader["Latitude"].ToString()))
                        verein.Latitude = decimal.Parse(reader["Latitude"].ToString());
                    else
                        verein.Latitude = 0;
                    if (!string.IsNullOrEmpty(reader["Longitude"].ToString()))
                        verein.Longitude = decimal.Parse(reader["Longitude"].ToString());
                    else
                        verein.Longitude = 0;
                }
            }
            conn.Close();
            return verein;
        }
        catch (Exception ex)
        {

            ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            return null;
        }
    }
}
    public class SpieltageRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public SpieltageRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Spieltag>> GetAllSpieltageL3()
    {
        try
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            await conn.OpenAsync();

            SqlCommand command = new SqlCommand("SELECT * FROM [SpieltageL3] ", conn);
            Spieltag spieltag = null;
            List<Spieltag> Spieltaglist = new List<Spieltag>();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (await reader.ReadAsync())
                {
                    spieltag = new Spieltag();

                    spieltag.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                    spieltag.SaisonID = int.Parse(reader["SaisonID"].ToString());
                    try
                    {
                        spieltag.StadionID = int.Parse(reader["StadionID"].ToString());
                    }
                    catch (Exception)
                    {

                        spieltag.StadionID = 0;
                    }
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
                    spieltag.Schiedsrichter = reader["Schiedrichter"].ToString();
                    spieltag.Abgeschlossen = bool.Parse(reader["Abgeschlossen"].ToString());
                    spieltag.Zuschauer = int.Parse(reader["Zuschauer"].ToString());
                    spieltag.TeamIconUrl1 = reader["TeamIconUrl1"].ToString();
                    spieltag.TeamIconUrl2 = reader["TeamIconUrl2"].ToString();

                    Spieltaglist.Add(spieltag);
                }
            }
            conn.Close();
            return Spieltaglist;
        }
        catch (Exception ex)
        {
            ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            return null;
        }
    }
    public async Task<IEnumerable<Spieltag>?> GetAllSpieltage()
    {
        try
        {
            using (var conn = new SqlConnection(Globals.connstring))
            {
                await conn.OpenAsync();

                var command = new SqlCommand("sp_spieltage", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                var spieltagList = new List<Spieltag>();

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var spieltag = new Spieltag
                        {
                            SpieltagId = reader.GetInt32(reader.GetOrdinal("SpieltagId")),
                            SaisonID = reader.GetInt32(reader.GetOrdinal("SaisonID")),
                            StadionID = reader.GetInt32(reader.GetOrdinal("StadionID")),
                            LigaID = reader.GetInt32(reader.GetOrdinal("LigaID")),
                            SpieltagNr = reader.GetString(reader.GetOrdinal("SpieltagNr")),
                            Saison = reader.GetString(reader.GetOrdinal("Saison")),
                            Verein1 = reader.GetString(reader.GetOrdinal("Verein1")),
                            Verein2 = reader.GetString(reader.GetOrdinal("Verein2")),
                            Verein1_Nr = reader.GetString(reader.GetOrdinal("Verein1_Nr")),
                            Verein2_Nr = reader.GetString(reader.GetOrdinal("Verein2_Nr")),
                            Tore1_Nr = reader.GetInt32(reader.GetOrdinal("Tore1_Nr")),
                            Doppelpunkt = ":",
                            Tore2_Nr = reader.GetInt32(reader.GetOrdinal("Tore2_Nr")),
                            Datum = reader.GetDateTime(reader.GetOrdinal("Datum")),
                            Ort = reader.GetString(reader.GetOrdinal("Ort")),
                            Schiedsrichter = reader.GetString(reader.GetOrdinal("Schiedrichter")),
                            Abgeschlossen = reader.GetBoolean(reader.GetOrdinal("Abgeschlossen")),
                            Zuschauer = reader.GetInt32(reader.GetOrdinal("Zuschauer")),
                            TeamIconUrl1 = "", //reader.GetString(reader.GetOrdinal("TeamIconUrl1")),
                            TeamIconUrl2 = ""  //reader.GetString(reader.GetOrdinal("TeamIconUrl2"))
                        };  // 

                        spieltagList.Add(spieltag);
                    }
                }

                return spieltagList;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            return null;
        }

    }

    public Spieltag AddSpieltag(Spieltag spieltag)
    {
        try
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.OpenAsync();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO Spieltage ([SpieltagNr],[Saison],[SaisonID],[LigaID],[Verein1_Nr],[Verein1],[Verein2_Nr],[Verein2],[Tore1_Nr],[Tore2_Nr],[Datum],[Ort],[Schiedrichter],[Abgeschlossen],[Zuschauer],[StadionID])" +
                " VALUES(@SpieltagNr,@Saison,@SaisonID,@LigaID,@Verein1_Nr,@Verein1,@Verein2_Nr,@Verein2,@Tore1_Nr,@Tore2_Nr,@Datum,@Ort,@Schiedrichter,@Abgeschlossen,@Zuschauer,@StadionID)";

            cmd.Parameters.AddWithValue("@SpieltagNr", spieltag.SpieltagNr);
            cmd.Parameters.AddWithValue("@Saison", spieltag.Saison);
            cmd.Parameters.AddWithValue("@SaisonID", spieltag.SaisonID);
            cmd.Parameters.AddWithValue("@StadionID", spieltag.StadionID);
            cmd.Parameters.AddWithValue("@LigaID", spieltag.LigaID);
            cmd.Parameters.AddWithValue("@Verein1_Nr", spieltag.Verein1_Nr);
            cmd.Parameters.AddWithValue("@Verein2_Nr", spieltag.Verein2_Nr);
            cmd.Parameters.AddWithValue("@Verein1", spieltag.Verein1);
            cmd.Parameters.AddWithValue("@Verein2", spieltag.Verein2);
            cmd.Parameters.AddWithValue("@Tore1_Nr", spieltag.Tore1_Nr);
            cmd.Parameters.AddWithValue("@Tore2_Nr", spieltag.Tore2_Nr);
            cmd.Parameters.AddWithValue("@Datum", spieltag.Datum);
            cmd.Parameters.AddWithValue("@Ort", spieltag.Ort);
            cmd.Parameters.AddWithValue("@Schiedrichter", spieltag.Schiedsrichter);
            cmd.Parameters.AddWithValue("@Abgeschlossen", spieltag.Abgeschlossen);
            cmd.Parameters.AddWithValue("@Zuschauer", spieltag.Zuschauer);

            cmd.ExecuteNonQuery();

            conn.Close();

            return spieltag;
        }
        catch (Exception ex)
        {
            ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
            return null;
        }

        //var result = await appDbContext.Spieltage.AddAsync(spieltag);
        //await appDbContext.SaveChangesAsync();
        //return result.Entity;
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
