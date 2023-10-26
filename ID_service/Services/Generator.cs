using System.Security.Cryptography;

namespace ID_service.Services
{
    public class Generator
    {
        private readonly RandomNumberGenerator rng;

        public Generator()
        {
            rng = RandomNumberGenerator.Create();
        }

        public byte[] GenerateKey()
        {
            byte[] key = new byte[16];
            rng.GetBytes(key);
            return key;
        }

        public byte[] GenerateIV()
        {
            byte[] iv = new byte[16];
            rng.GetBytes(iv);
            return iv;
        }
    }
}
