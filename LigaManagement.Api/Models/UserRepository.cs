using LigaManagement.Api.Models;
using LigaManagement.Web.Classes;
using Ligamanager.Components;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using static Ligamanager.Components.Globals;


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
                cmd.CommandText = "INSERT INTO [User] (Username,Password,[Mail],Location,Firstname,Lastname)" +
                    " VALUES(@Username,@Password,@Mail,@Location,@Firstname,@Lastname)SELECT SCOPE_IDENTITY()";

                cmd.Parameters.AddWithValue("@Username", User.Username);
                cmd.Parameters.AddWithValue("@Password", User.Password);
                cmd.Parameters.AddWithValue("@Mail", User.Mail);
                cmd.Parameters.AddWithValue("@FirstName", User.FirstName);
                cmd.Parameters.AddWithValue("@LastName", User.LastName);
                cmd.Parameters.AddWithValue("@Location", User.Location);
                var newId = cmd.ExecuteScalar();


                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [UserRoles] (UserID,RoleID)" +
                    " VALUES(@UserID,@RoleID)";

                cmd.Parameters.AddWithValue("@UserID", newId);
                cmd.Parameters.AddWithValue("@RoleID", UserGroup.Gast);                
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

                SqlCommand command = new SqlCommand("[user].ID, NormalizedName,[FirstName],[LastName],[Username],[Password],[Location],[Mail]\r\n  FROM [dbo].[UserRoles] inner join [User] on UserRoles.UserId = [User].Id inner join Roles on UserRoles.RoleId = Roles.Id where ID =" + UserId, conn);
                User user = null;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user = new User();
                        user.Password = reader["Password"].ToString();
                        user.Username = reader["Username"].ToString();
                        user.FirstName = reader["Firstname"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Location = reader["Location"].ToString();
                        user.Role = reader["Role"].ToString();
                        user.Mail = reader["NormalizedName"].ToString();
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

                SqlCommand command = new SqlCommand("SELECT [user].ID, NormalizedName,[FirstName],[LastName],[Username],[Password],[Location],[Mail] FROM [dbo].[UserRoles] inner join [User] on UserRoles.UserId = [User].Id inner join Roles on UserRoles.RoleId = Roles.Id", conn);
                List<User> User = new List<User>();
                User user;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {                        
                        user = new User();
                        user.Password = reader["Password"].ToString();
                        user.Username = reader["Username"].ToString();
                        user.FirstName = reader["Firstname"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Location = reader["Location"].ToString();
                        user.Mail = reader["Mail"].ToString();
                        user.Role = reader["NormalizedName"].ToString();

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

