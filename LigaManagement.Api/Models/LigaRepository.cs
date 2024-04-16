using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LigaManagerManagement.Api.Models
{
    public class LigaRepository : ILigaRepository
    {       

        public async Task<Liga> AddLiga(Liga liga)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [Ligen] (Liganame, Verband, Erstaustragung, Absteiger, Aktiv)" +
                    " VALUES(@Liganame,@Verband,@Erstaustragung,@Absteiger,@Aktiv)";

                cmd.Parameters.AddWithValue("@Liganame", liga.Liganame);
                cmd.Parameters.AddWithValue("@Verband", liga.Verband);
                cmd.Parameters.AddWithValue("@Erstaustragung", liga.Erstaustragung);
                cmd.Parameters.AddWithValue("@Absteiger", liga.Absteiger);
                cmd.Parameters.AddWithValue("@Aktiv", liga.Aktiv);

                cmd.ExecuteNonQuery();

                conn.Close();

                return liga;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            //var result = await appDbContext.Ligen.AddAsync(Liga);
            //await appDbContext.SaveChangesAsync();
            //return result.Entity;
        }

        public Task<Liga> DeleteLiga(int LigaId)
        {

            SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
            conn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM [dbo].[Ligen] Where ID= @LigaId";

            cmd.Parameters.AddWithValue("@LigaId", LigaId);

            cmd.ExecuteNonQuery();

            conn.Close();

            return null;

            //var result = await appDbContext.Ligen
            //   .FirstOrDefaultAsync(e => e.Id == LigaId);
            //if (result != null)
            //{
            //    appDbContext.Ligen.Remove(result);
            //    await appDbContext.SaveChangesAsync();
            //    return result;
            //}

            //return null;
        }

        public async Task<Liga> GetLiga(int LigaId)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Ligen] WHERE ID =" + LigaId, conn);
                Liga liga = new Liga();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        liga = new Liga();

                        liga.Id = (int)reader["Id"];
                        liga.Liganame = reader["Liganame"].ToString();
                        liga.Verband = reader["Verband"].ToString();
                        liga.Erstaustragung = (DateTime)reader["Erstaustragung"];
                        liga.Liganame = reader["Liganame"].ToString();
                        liga.Absteiger = (int)reader["Absteiger"];
                        liga.Aktiv = bool.Parse(reader["Aktiv"].ToString());
                    }
                }
                conn.Close();
                return liga;
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }

            //return await appDbContext.Ligen
            //    .FirstOrDefaultAsync(d => d.Id == LigaId);
        }

        public async Task<IEnumerable<Liga>> GetLigen()
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [Ligen]", conn);
                Liga liga = null;
                List<Liga> peList = new List<Liga>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        liga = new Liga();

                        liga.Id = int.Parse(reader["Id"].ToString());
                        liga.Liganame = reader["Liganame"].ToString();
                        liga.Verband = reader["Verband"].ToString();
                        liga.Erstaustragung = DateTime.Parse(reader["Erstaustragung"].ToString());
                        liga.Liganame = reader["Liganame"].ToString();
                        liga.Absteiger = int.Parse(reader["Absteiger"].ToString());
                        liga.Aktiv = bool.Parse(reader["Aktiv"].ToString());

                        peList.Add(liga);
                    }
                }
                conn.Close();
                return peList;
            }
            catch (System.Exception ex)
            {

                Debug.Print(ex.Message);

                return null;
            }
           // return await appDbContext.Ligen.ToListAsync();
        }

        public async Task<Liga> UpdateLiga(Liga liga)
        {
            int bAktiv;
            
            try
            {
                SqlConnection conn = new SqlConnection("Data Source=PC-WISST\\SQLEXPRESS;Database=LigaDB;Integrated Security=True;TrustServerCertificate=true");
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
               // cmd.CommandText = "UPDATE Ligen(Liganame,Verband,Erstaustragung,Absteiger,Aktiv)" +
               //" VALUES(@Liganame,@Verband,@Erstaustragung,@Absteiger,@Aktiv)";

               // cmd.Parameters.AddWithValue("@Liganame", liga.Liganame);
               // cmd.Parameters.AddWithValue("@Verband", liga.Verband);
               // cmd.Parameters.AddWithValue("@Erstaustragung", liga.Erstaustragung);
               // cmd.Parameters.AddWithValue("@Absteiger", liga.Absteiger);
               // cmd.Parameters.AddWithValue("@Aktiv", liga.Aktiv);

                if (liga.Aktiv == false)
                    bAktiv = 0;
                else
                    bAktiv = 1;

                cmd.CommandText = "UPDATE [dbo].[Ligen] SET " +                    
                      "[Liganame] = '" + liga.Liganame + "'" +
                      ",[Verband] = '" + liga.Verband + "'" +                     
                      ",[Erstaustragung] = '" + liga.Erstaustragung + "'" +                      
                      ",[Absteiger] =" + liga.Absteiger +
                      ",[Aktiv] =" + bAktiv +                      
                      " WHERE  [Id] = " + liga.Id;

                cmd.ExecuteNonQuery();

                conn.Close();

                return liga;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }


            //try
            //{
            //    var result = await appDbContext.Ligen
            //    .FirstOrDefaultAsync(e => e.Id == Liga.Id);

            //    if (result != null)
            //    {
            //        result.Liganame = Liga.Liganame;
            //        result.Verband = Liga.Verband;
            //        result.Absteiger = Liga.Absteiger;
            //        result.Aktiv = Liga.Aktiv;
            //        result.Erstaustragung = Liga.Erstaustragung;

            //        await appDbContext.SaveChangesAsync();

            //        return result;
            //    }
            //    return null;
            //}
            //catch (Exception ex)
            //{
            //    Debug.Print(ex.StackTrace);
            //    return null;
            //}
        }
    }
}

