using LouvOgRathApp.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LouvOgRathApp.ServerSide.DataAccess
{
    public class UserCredentialsRepository : RepositoryBase
    {
        #region contructer

        public UserCredentialsRepository(string nameOfConfigFileConnectionString) : base(nameOfConfigFileConnectionString)
        {
        }
        #endregion


        #region methods

        /// <summary>
        /// attempt to login the usercredentials
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        public (bool, RoleKind?) UserCredentialsLoginAttemp(UserCredentials userCredentials)
        {
            DataSet ds = executor.Execute($"EXECUTE GetUserCredentials @username =  '{userCredentials.Username}', @password = '{Encrypt(GetHashKey(userCredentials.Password), userCredentials.Password)}'");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                return (true, (RoleKind)Enum.Parse(typeof(RoleKind), dr.Field<string>("RolesName")));
            }
            return (false, null);
        }
        /// <summary>
        /// creating a new user in the database
        /// </summary>
        /// <param name="user"></param>
        public void CreateNewUser(UserCredentials user)
        {
            DataSet ds = Executor.Execute($"EXECUTE InsertCredentials @Username = {user.Username}, @Password = '{Encrypt(GetHashKey(user.Password), user.Password)}', @Fullname = '{user.Person.Fullname}', @Email = '{user.Person.Email}', @PhoneNumber = '{user.Person.Email}', @RolesId = {(int)user.RoleKind_}");
        }
        /// <summary>
        /// used for hash keys 
        /// </summary>
        /// <param name="hashKey"></param>
        /// <returns></returns>
        public static byte[] GetHashKey(string hashKey)
        {
            // Initialize
            UTF8Encoding encoder = new UTF8Encoding();
            // Get the salt
            string salt = !string.IsNullOrEmpty(hashKey) ? hashKey : "I am a nice little salt";
            byte[] saltBytes = encoder.GetBytes(salt);
            // Setup the hasher
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(hashKey, saltBytes);
            // Return the key
            return rfc.GetBytes(16);
        }
        /// <summary>
        /// used for encrypt data to database
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dataToEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(byte[] key, string dataToEncrypt)
        {
            // Initialize
            AesManaged encryptor = new AesManaged();
            // Set the key
            encryptor.Key = key;
            encryptor.IV = key;
            // create a memory stream
            using (MemoryStream encryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream encrypt = new CryptoStream(encryptionStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    byte[] utfD1 = UTF8Encoding.UTF8.GetBytes(dataToEncrypt);
                    encrypt.Write(utfD1, 0, utfD1.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();
                    // Return the encrypted data
                    return Convert.ToBase64String(encryptionStream.ToArray());
                }
            }
        }
        /// <summary>
        /// used to decrypt from database
        /// </summary>
        /// <param name="key"></param>
        /// <param name="encryptedString"></param>
        /// <returns></returns>
        public static string Decrypt(byte[] key, string encryptedString)
        {
            // Initialize
            AesManaged decryptor = new AesManaged();
            byte[] encryptedData = Convert.FromBase64String(encryptedString);
            // Set the key
            decryptor.Key = key;
            decryptor.IV = key;
            // create a memory stream
            using (MemoryStream decryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream decrypt = new CryptoStream(decryptionStream, decryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    decrypt.Write(encryptedData, 0, encryptedData.Length);
                    decrypt.Flush();
                    decrypt.Close();
                    // Return the unencrypted data
                    byte[] decryptedData = decryptionStream.ToArray();
                    return UTF8Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);
                }
            }
        }
        #endregion
    }
}
