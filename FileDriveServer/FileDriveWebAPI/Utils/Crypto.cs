using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileDriveWebAPI.Utils
{
    public class Crypto
    {
        public static string Encrypt(string value = "", string salt = "") 
        {
            byte[] valueBytes = System.Text.UnicodeEncoding.Unicode.GetBytes(value);
            byte[] saltBytes = System.Text.UnicodeEncoding.Unicode.GetBytes(salt);

            byte[] combinedBytes = new byte[valueBytes.Length + saltBytes.Length];

            System.Buffer.BlockCopy(valueBytes, 0, combinedBytes, 0, valueBytes.Length);
            System.Buffer.BlockCopy(saltBytes, 0, combinedBytes, valueBytes.Length, saltBytes.Length);

            System.Security.Cryptography.HashAlgorithm hashAlgo = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = hashAlgo.ComputeHash(combinedBytes);

            byte[] hashPlusSalt = new byte[hash.Length + salt.Length];
            System.Buffer.BlockCopy(hash, 0, hashPlusSalt, 0, hash.Length);
            System.Buffer.BlockCopy(saltBytes, 0, hashPlusSalt, hash.Length, salt.Length);

            string result = System.Text.Encoding.UTF8.GetString(hashPlusSalt);

            return result;
        }
    }
}
