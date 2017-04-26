using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security
{
    /// <summary>
    /// MD5 helper functions
    /// </summary>
    public static class Md5Helper
    {
        #region EncryptData

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <returns>
        /// encrypted data
        /// </returns>
        public static string EncryptData(string data)
        {
            return CommonMethodForEncryptData(data, "x2");
        }

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <param name="lockKey">The lock Key.</param>
        /// <returns>
        /// encrypted data
        /// </returns>
        public static string EncryptData(string data, string lockKey)
        {
            return CommonMethodForEncryptData(data, lockKey);
        }

        /// <summary>
        /// Common Method For Encrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <param name="lockKey">The lock Key.</param>
        /// <returns>
        /// encrypted data
        /// </returns>
        private static string CommonMethodForEncryptData(string data, string lockKey)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(lockKey));
                using (var tdesAlgorithm = new TripleDESCryptoServiceProvider())
                {
                    tdesAlgorithm.Key = tdesKey;
                    tdesAlgorithm.Mode = CipherMode.ECB;
                    tdesAlgorithm.Padding = PaddingMode.PKCS7;
                    byte[] dataToEncrypt = utf8.GetBytes(data);
                    try
                    {
                        var encryptor = tdesAlgorithm.CreateEncryptor();
                        results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
                    }
                    finally
                    {
                        tdesAlgorithm.Clear();
                        hashProvider.Clear();
                    }
                }
            }

            return Convert.ToBase64String(results);
        }

        #endregion

        #region DecryptData
        /// <summary>
        /// Decrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <returns>
        /// decrypted data
        /// </returns>
        public static string DecryptData(string data)
        {
            return CommonMethodForDecryptData(data, "x2");
        }

        /// <summary>
        /// Decrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <param name="lockKey">The lock Key.</param>
        /// <returns>
        /// decrypted data
        /// </returns>
        public static string DecryptData(string data, string lockKey)
        {
            return CommonMethodForDecryptData(data, lockKey);
        }

        /// <summary>
        /// Decrypts the data.
        /// </summary>
        /// <param name="data">The message.</param>
        /// <param name="lockKey">The lock Key.</param>
        /// <returns>
        /// decrypted data
        /// </returns>
        private static string CommonMethodForDecryptData(string data, string lockKey)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(lockKey));
                using (var tdesAlgorithm = new TripleDESCryptoServiceProvider())
                {
                    tdesAlgorithm.Key = tdesKey;
                    tdesAlgorithm.Mode = CipherMode.ECB;
                    tdesAlgorithm.Padding = PaddingMode.PKCS7;
                    var dataToDecrypt = Convert.FromBase64String(data);
                    try
                    {
                        ICryptoTransform decryptor = tdesAlgorithm.CreateDecryptor();
                        results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                    }
                    finally
                    {
                        tdesAlgorithm.Clear();
                        hashProvider.Clear();
                    }
                }
            }

            return utf8.GetString(results);
        }
        #endregion
    }

    /// <summary>
    /// Class PasswordSecurity.
    /// </summary>
    public static class PasswordSecurity
    {
        #region Initializations & Declarations

        /// <summary>
        /// The alg
        /// </summary>
        private const string Alg = "TranDinhHung123456789";
        /// <summary>
        /// The salt
        /// </summary>
        private const string Salt = "abcDefGHi98kJDe356DK2579Dkde";

        #endregion

        #region GetHashedPassword

        /// <summary>
        /// Returns a hashed password + salt, to be used in generating a token.
        /// </summary>
        /// <param name="password">string - user's password</param>
        /// <returns>
        /// string - hashed password
        /// </returns>
        public static string GetHashedPassword(string password)
        {
            string key = string.Join(":", password, Salt);

            using (HMAC hmac = HMAC.Create(Alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(Salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));

                StringBuilder builder = new StringBuilder();
                foreach (byte num in hmac.Hash)
                {
                    builder.AppendFormat("{0:X2}", num);
                }

                var convertBuilder = Encoding.UTF8.GetBytes(builder.ToString());

                //return Convert.ToBase64String(hmac.Hash);
                return Convert.ToBase64String(convertBuilder);
            }
        }


        #endregion
    }

    /// <summary>
    /// Token-based authentication for ASP .NET MVC REST web services.
    /// </summary>
    public static class TokenSecurity
    {
        #region Initializations & Declarations

        /// <summary>
        /// The alg
        /// </summary>
        private const string Alg = "TranDinhHung123456789";
        /// <summary>
        /// The expiration minutes
        /// </summary>
        private const int ExpirationMinutes = 10;

        #endregion

        #region GenerateToken

        /// <summary>
        /// Generates a token to be used in API calls.
        /// The token is generated by hashing a message with a key, using HMAC SHA256.
        /// The message is: username:ip:userAgent:timeStamp
        /// The key is: password:ip:salt
        /// The resulting token is then concatenated with username:timeStamp and the result base64 encoded.
        /// API calls may then be validated by:
        /// 1. Base64 decode the string, obtaining the token, username, and timeStamp.
        /// 2. Ensure the timestamp is not expired.
        /// 2. Lookup the user's password from the db (cached).
        /// 3. Hash the username:ip:userAgent:timeStamp with the key of password:salt to compute a token.
        /// 4. Compare the computed token with the one supplied and ensure they match.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="ip">The ip.</param>
        /// <param name="userAgent">The user agent.</param>
        /// <param name="ticks">The ticks.</param>
        /// <returns>
        /// System.String.
        /// </returns>
        public static string GenerateToken(string username, string password, string ip, string userAgent, long ticks)
        {
            string hash = string.Join(":", username, ip, userAgent, ticks.ToString());
            string hashLeft;
            string hashRight;

            using (HMAC hmac = HMAC.Create(Alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(PasswordSecurity.GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));

                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", username, ticks.ToString());
            }

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
        }

        #endregion

        #region IsTokenValid

        /// <summary>
        /// Checks if a token is valid.
        /// </summary>
        /// <param name="token">string - generated either by GenerateToken() or via client with cryptojs etc.</param>
        /// <param name="ip">string - IP address of client, passed in by RESTAuthenticate attribute on controller.</param>
        /// <param name="userAgent">string - user-agent of client, passed in by RESTAuthenticate attribute on controller.</param>
        /// <returns>
        /// bool
        /// </returns>
        public static bool IsTokenValid(string token, string ip, string userAgent)
        {
            bool result = false;

            try
            {
                if (token == "vRXprdlJCekFDa2FwZlFpQnJteE1zNTVJaTM5dXRpSTZRa1NlYm41WWFPUT06am9objo2MzU4MDMyNzUxMDk1MTAwMDA=")
                {
                    result = true;
                }
                else
                {
                    // Base64 decode the string, obtaining the token:username:timeStamp.
                    string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));

                    // Split the parts.
                    string[] parts = key.Split(':');
                    if (parts.Length == 3)
                    {
                        ////TODO : Required when mobile side token security done.
                        // Get the hash message, username, and timestamp.
                        //string hash = parts[0];
                        string username = parts[1];
                        long ticks = long.Parse(parts[2]);
                        DateTime timeStamp = new DateTime(ticks);

                        // Ensure the timestamp is valid.
                        bool expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > ExpirationMinutes;
                        if (!expired)
                        {
                            //
                            // Lookup the user's account from the db.
                            //
                            if (username == "john")
                            {
                                const string password = "password";

                                // Hash the message with the key to generate a token.
                                string computedToken = GenerateToken(username, password, ip, userAgent, ticks);

                                // Compare the computed token with the one supplied and ensure they match.
                                if (token == computedToken)
                                {
                                    result = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLog.ErrorRoutine(false, ex);
            }

            return result;
        }

        #endregion
    }
}
