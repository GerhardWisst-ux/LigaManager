using LigaManagement.Api.Models;
using LigaManagement.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace LigaManagerManagement.Api.Models
{
    public class SpieltageRepositoryLE : ISpieltagRepositoryLE
    {

        public async Task<IEnumerable<Spieltag>> GetSpieltage()
        {

            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT Top 10 * FROM [LetzteErgebnisse] Order by Anlagedatum DESC ", conn);
                Spieltag spieltag = null;
                List<Spieltag> Spieltaglist = new List<Spieltag>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spieltag = new Spieltag();

                        spieltag.SpieltagId = int.Parse(reader["SpieltagId"].ToString());
                        spieltag.SaisonID = int.Parse(reader["SaisonID"].ToString());
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
                        spieltag.Zuschauer = 0;
                        spieltag.TeamIconUrl1 = GetImageFromPath(reader["Verein1"].ToString(), int.Parse(reader["SpieltagNr"].ToString()));
                        spieltag.TeamIconUrl2 = GetImageFromPath(reader["Verein2"].ToString(), int.Parse(reader["SpieltagNr"].ToString()));

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

        public string GetImageFromPath(string sVerein, int SpieltagNr)
        {

            string sVereinImage = string.Empty;
            

            try
            {
                sVereinImage = sVerein + ".webp";

                string rootpath = System.IO.Path.Combine("wwwroot/images", sVereinImage);
                string rootpathNat = System.IO.Path.Combine("wwwroot/images/Nationalmannschaften/", sVereinImage);

                if (Globals.LigaNummer == 20 || Globals.LigaNummer == 21)
                {
                    return "images/NoImage.webp";
                }
                else if (SpieltagNr > 0)
                {
                    return "/images/" + sVereinImage;
                }
                else if (SpieltagNr == 0)
                {
                    return "images/Nationalmannschaften/" + sVereinImage;
                }
                else
                {
                    return "images/NoImage.webp";
                }
            }
            catch (Exception)
            {

                return "/images//NoImage.webp";
            }
        }
    }
}

