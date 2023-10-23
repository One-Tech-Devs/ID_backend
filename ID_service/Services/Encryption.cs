﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ID_service.Services
{
    internal class Encryption
    {
        static byte[] Encrypt(string data, byte[] key, byte[] iv)
        {
            byte[] encryptedData;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(data);
                        }

                        encryptedData = memoryStream.ToArray();
                    }
                }
            }
            return encryptedData;
        }

        static string Decrypt(byte[] encryptedData, byte[] key, byte[] iv)
        {
            string data = String.Empty;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            data = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return data;
        }
    }
}
