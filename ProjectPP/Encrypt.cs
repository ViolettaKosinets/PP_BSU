using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPP
{
    public class Encrypt
    {
        private static byte[] GetIV(string ivSecret)
        {
            using MD5 md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(ivSecret));
        }

        private static byte[] GetKey(string key)
        {
            using SHA256 sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }

        public static void EncryptFile(string sourceFileName, string outputFileName, string key, string iv)
        {


            using Aes aes = Aes.Create();
            aes.IV = GetIV(iv);
            aes.Key = GetKey(key);

            using FileStream inStream = new FileStream(sourceFileName, FileMode.Open); 
            using FileStream outStream = new FileStream(outputFileName, FileMode.Create);

            CryptoStream encStream = new CryptoStream(outStream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write);

            long readTotal = 0;

            int len;
            int tempSize = 100; 
            byte[] bin = new byte[tempSize];    

            while (readTotal < inStream.Length)
            {
                len = inStream.Read(bin, 0, tempSize);
                encStream.Write(bin, 0, len);
                readTotal = readTotal + len;
            }

            encStream.Close();
            outStream.Close();
            inStream.Close();
        }

        public static void DecryptFile(string sourceFile, string destFile, string keyStr, string ivStr)
        {
            using FileStream fileStream = new(sourceFile, FileMode.Open);
            using Aes aes = Aes.Create();

            var iv = GetIV(ivStr);
            var key = GetKey(keyStr);
            aes.IV = iv; aes.Key = key;

            using CryptoStream cryptoStream = new(fileStream,
                                       aes.CreateDecryptor(key, iv),
                                                  CryptoStreamMode.Read); 

            using FileStream outStream = new FileStream(destFile, FileMode.Create);

            using BinaryReader decryptReader = new(cryptoStream);

            int tempSize = 10;
            byte[] data;

            while (true)
            {
                data = decryptReader.ReadBytes(tempSize);
                if (data.Length == 0)
                    break;
                outStream.Write(data, 0, data.Length);
            }
        }
    }
}