using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System;
using Ligamanager.Components;
using LigaManagerManagement.Models;

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
            chartDataList = new List<ChartData>();

            SqlConnection conn = new SqlConnection(Globals.connstring);

            string selectSQL = "SELECT [ChartDataId],[Spiele],[Punkte] FROM [dbo].[CHARTDATA] where saisonID = " + Globals.SaisonID 
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

        public class ChartPlaetze
        {
            public List<int?> plaetze = new List<int?>();
        }

    }
}
