using LigaManagement.Api.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;


namespace LigamanagerManagement.Api.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> CreateUser(User User)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [User] (Username,Password)" +
                    " VALUES(@Username,@Password)";

                cmd.Parameters.AddWithValue("@Username", User.Username);
                cmd.Parameters.AddWithValue("@Password", User.Password);

                cmd.ExecuteNonQuery();

                conn.Close();

                return User;
            }
            catch (System.Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }
        }

        public async Task<User> DeleteUser(int UserId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM [dbo].[User]  where ID = @ID";

                cmd.Parameters.AddWithValue("@Id", UserId);

                cmd.ExecuteNonQuery();

                conn.Close();

                return null;
            }
            catch (System.Exception ex)
            {

                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
            }

        }

        public async Task<User> GetUser(int UserId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [User] where ID =" + UserId, conn);
                User user = null;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user.Password = reader["Password"].ToString();
                        user.Username = reader["Username"].ToString();
                    }
                }
                conn.Close();
                return user;
            }
            catch (System.Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;

            }
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM [User]", conn);
                List<User> User = new List<User>();
                User user;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User();                        
                        user.Password = reader["Password"].ToString();
                        user.Username = reader["Username"].ToString();                      

                        User.Add(user);
                    }
                }
                conn.Close();
                return User;
            }
            catch (System.Exception ex)
            {
                ErrorLogger.WriteToErrorLog(ex.Message, ex.StackTrace, Assembly.GetExecutingAssembly().FullName);
                return null;
                
            }
        }

        public async Task<User> UpdateUser(User User)
        {
            SqlConnection conn = new SqlConnection(Globals.connstring);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE [User] (Username,Password)" +
                " VALUES(@Username,@Password)";

            cmd.Parameters.AddWithValue("@Username", User.Username);
            cmd.Parameters.AddWithValue("@Password", User.Password);
            

            cmd.ExecuteNonQuery();

            conn.Close();

            return User;
        }
    }
}

