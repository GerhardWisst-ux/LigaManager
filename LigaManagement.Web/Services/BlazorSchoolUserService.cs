using BootstrapBlazor.Components;
using LigaManagement.Api.Models;
using Ligamanager.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LigaManagerManagement.Web.Services
{
    public class BlazorSchoolUserService
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly string _blazorSchoolStorageKey = "blazorSchoolIdentity";


        public BlazorSchoolUserService(ProtectedLocalStorage protectedLocalStorage)
        {
            _protectedLocalStorage = protectedLocalStorage;
        }

        public User? LookupUserInDatabase(string username, string password)
        {


            var usersFromDatabase = GetUsers();

            var foundUser = usersFromDatabase.SingleOrDefault(u => u.Username == username && u.Password == password);

            return foundUser;
        }
        public IEnumerable<User> GetUsers()
        {
            try
            {
                SqlConnection conn = new SqlConnection(Globals.connstring);
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT NormalizedName,[FirstName],[LastName],[Username],[Password],[Location],[Mail]  FROM [dbo].[UserRoles] inner join [User] on UserRoles.UserId = [User].Id inner join Roles on UserRoles.RoleId = Roles.Id", conn);
                List<User> User = new List<User>();
                User user;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new User();
                        user.Password = Decrypt(reader["Password"].ToString());
                        user.Username = reader["Username"].ToString();
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Location = reader["Location"].ToString();
                        user.Role = reader["NormalizedName"].ToString();
                        user.Mail = reader["Mail"].ToString();

                        //string password =user.Password;

                        //Debug.Print("Password:  " + user.Password);

                        //string strDecrypted = (Decrypt(user.Password));
                        //user.Password = strDecrypted;
                        //Debug.Print("Decrypted: " + strDecrypted);

                        //string strEncrypted = (Encrypt(strDecrypted));
                        //Debug.Print("Encrypted: " + strEncrypted);


                        User.Add(user);
                    }
                }
                conn.Close();
                return User;
            }
            catch (System.Exception ex)
            {
                return null;

            }
        }

        public const String strPermutation = "ouiveyxaqtd";
        public const Int32 bytePermutation1 = 0x19;
        public const Int32 bytePermutation2 = 0x59;
        public const Int32 bytePermutation3 = 0x17;
        public const Int32 bytePermutation4 = 0x41;
        public static string Encrypt(string strData)
        {

            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(strData)));
            // reference https://msdn.microsoft.com/en-us/library/ds4kkd55(v=vs.110).aspx

        }


        // decoding
        public static string Decrypt(string strData)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(strData)));
            // reference https://msdn.microsoft.com/en-us/library/system.convert.frombase64string(v=vs.110).aspx

        }

        // encrypt
        public static byte[] Encrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(strPermutation,
            new byte[] { bytePermutation1,
                         bytePermutation2,
                         bytePermutation3,
                         bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateEncryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }

        // decrypt
        public static byte[] Decrypt(byte[] strData)
        {
            PasswordDeriveBytes passbytes =
            new PasswordDeriveBytes(strPermutation,
            new byte[] { bytePermutation1,
                         bytePermutation2,
                         bytePermutation3,
                         bytePermutation4
            });

            MemoryStream memstream = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = passbytes.GetBytes(aes.KeySize / 8);
            aes.IV = passbytes.GetBytes(aes.BlockSize / 8);

            CryptoStream cryptostream = new CryptoStream(memstream,
            aes.CreateDecryptor(), CryptoStreamMode.Write);
            cryptostream.Write(strData, 0, strData.Length);
            cryptostream.Close();
            return memstream.ToArray();
        }
        // reference
        // https://msdn.microsoft.com/en-us/library/system.security.cryptography(v=vs.110).aspx
        // https://msdn.microsoft.com/en-us/library/system.security.cryptography.cryptostream%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
        // https://msdn.microsoft.com/en-us/library/system.security.cryptography.rfc2898derivebytes(v=vs.110).aspx
        // https://msdn.microsoft.com/en-us/library/system.security.cryptography.aesmanaged%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396



        public async Task PersistUserToBrowserAsync(User user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            await _protectedLocalStorage.SetAsync(_blazorSchoolStorageKey, userJson);
        }

        public async Task<User?> FetchUserFromBrowserAsync()
        {
            try
            {
                var storedUserResult = await _protectedLocalStorage.GetAsync<string>(_blazorSchoolStorageKey);

                if (storedUserResult.Success && !string.IsNullOrEmpty(storedUserResult.Value))
                {
                    var user = JsonConvert.DeserializeObject<User>(storedUserResult.Value);

                    return user;
                }
            }
            catch (InvalidOperationException)
            {
            }

            return null;
        }

        public async Task ClearBrowserUserDataAsync() => await _protectedLocalStorage.DeleteAsync(_blazorSchoolStorageKey);

    }

}
