using System;
using System.Web.Security;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace MCAS.Web.Objects.CommonHelper
{
   public class EnDecryption
    {
        static string gStrkey = "PR0$^CTB^IL$ER";

        public EnDecryption()
        {

        }
        public static string EncryptMessage(string plainMessage)
        {
            plainMessage = System.Web.HttpUtility.UrlEncode(plainMessage);

            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.IV = new byte[8];
            byte[] encryptedBytes = null;

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(gStrkey, new byte[0]);

            try
            {
                // Hash Algorithms can be MD5 , SHA , SHA1
                //des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);
                des.Key = pdb.CryptDeriveKey("RC2", "SHA1", 128, new byte[8]);

                MemoryStream ms = new MemoryStream(plainMessage.Length * 2);
                CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainMessage);
                encStream.Write(plainBytes, 0, plainBytes.Length);
                encStream.FlushFinalBlock();
                encryptedBytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(encryptedBytes, 0, (int)ms.Length);
                encStream.Close();
            }
            finally
            {
                des.Clear();
                des = null;
                pdb = null;
            }

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string DecryptMessage(string encryptedBase64)
        {
            string plainMessage = null;

            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.IV = new byte[8];
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(gStrkey, new byte[0]);
            // Hash Algorithms can be MD5 , SHA , SHA1
            des.Key = pdb.CryptDeriveKey("RC2", "SHA1", 128, new byte[8]);
            try
            {

                byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
                MemoryStream ms = new MemoryStream(encryptedBase64.Length);
                CryptoStream decStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                decStream.FlushFinalBlock();
                byte[] plainBytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(plainBytes, 0, (int)ms.Length);
                decStream.Close();
                plainMessage = Encoding.UTF8.GetString(plainBytes);

            }
            finally
            {
                des.Clear();
                des = null;
            }
            return System.Web.HttpUtility.UrlDecode(plainMessage);

        }
    }
}
