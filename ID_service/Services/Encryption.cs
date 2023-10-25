using System.Security.Cryptography;

namespace ID_service.Services
{
    public class Encryption
    {
        public static byte[] Encrypt(string data, byte[] key, byte[] iv)
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

        public static string Decrypt(byte[] encryptedData, byte[] key, byte[] iv)
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
