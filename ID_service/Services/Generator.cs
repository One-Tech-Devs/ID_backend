﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ID_service.Services
{
    internal class Generator
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