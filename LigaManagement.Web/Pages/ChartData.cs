using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using Ligamanager.Components;
using LigaManagerManagement.Models;
using LigaManagement.Models;
using System.Reflection;
using LigaManagement.Web.Classes;

namespace LigaManagement.Web.Pages
{
    public class ChartData
    {
        public List<ChartData> chartDataList = new List<ChartData>();

        public int ChartDataId { get; set; }
        public int ChartSpiele { get; set; }
        public int ChartValue { get; set; }
        public List<ChartData> GetChartData(int vereinsnr)
        {
            try
            {
                chartDataList = new List<ChartData>();

                SqlConnection conn = new SqlConnection(Globals.connstring);

                string selectSQL = "SELECT [ChartDataId],[Spiele],[Punkte] FROM [dbo].[CHARTDATA] where saisonID = " + Globals.SaisonID + " AND LigaID = " + Globals.LigaID
                                    + " and VereinNr = " + vereinsnr;

                conn.Open(); SqlCommand cmd = new SqlCommand(selectSQL, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        ChartData chartData = new ChartData();
                        chartData.ChartDataId = Convert.ToInt32(dr["ChartDataId"]);
                        chartData.ChartSpiele = Convert.ToInt32(dr["Spiele"]);
                        chartData.ChartValue = Convert.ToInt32(dr["Punkte"]);
                        chartData.chartDataList.Add(chartData);

                        chartDataList.Add(chartData);
                    }
                }

                return chartDataList;
            }
            catch (System.Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public bool InsertChartDataPunkte(List<int?> chartarray, int vereinsnr)
        {
            try
            {
                int punkte = 0;
                SqlConnection conn = new SqlConnection(Globals.connstring);
                SqlCommand cmd = new SqlCommand();
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM [dbo].[CHARTDATA]";

                cmd.Parameters.AddWithValue("@VereinNr", vereinsnr);

                cmd.ExecuteNonQuery();                

                for (int i = 0; i < chartarray.Count - 1; i++)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO [CHARTDATA] (SaisonID,LigaID,VereinNr,Spiele,Punkte)" +
                    " VALUES(@SaisonID,@LigaID,@VereinNr,@Spiele,@Punkte)";

                    cmd.Parameters.AddWithValue("@SaisonID", Globals.SaisonID);
                    cmd.Parameters.AddWithValue("@LigaID", Globals.LigaID);
                    cmd.Parameters.AddWithValue("@VereinNr", vereinsnr);
                    cmd.Parameters.AddWithValue("@Spiele", i + 1);                    
                    cmd.Parameters.AddWithValue("@Punkte", chartarray[i + 1]);

                    cmd.ExecuteNonQuery();
                }

                conn.Close();

                return true;
            }
            catch (System.Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return true;
            }
        }

        public class ChartPlaetze
        {
            public List<int?> punkte = new List<int?>();
            public List<int?> plaetze = new List<int?>();
        }

    }
}
