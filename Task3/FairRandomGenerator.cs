using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System.Security.Cryptography;

namespace Task3
{
    public static class FairRandomGenerator
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();

        public static (int number, string hmac, byte[] key) GenerateFairNumber(int maxValue)
        {
            if (maxValue <= 0) throw new ArgumentException("maxValue must be greater than 0");
            byte[] key = GenerateKey();
            int number = RandomNumberGenerator.GetInt32(maxValue);
            string hmac = ComputeHmac(number, key);
            return (number, hmac, key);
        }

        private static byte[] GenerateKey()
        {
            byte[] key = new byte[32];
            rng.GetBytes(key);
            return key;
        }

        private static string ComputeHmac(int number, byte[] key)
        {
            byte[] numberBytes = BitConverter.GetBytes(number);
            byte[] hash = ComputeHmacHash(numberBytes, key);
            return BitConverter.ToString(hash).Replace("-", "");
        }

        private static byte[] ComputeHmacHash(byte[] numberBytes, byte[] key)
        {
            var sha3 = new Sha3Digest(256);
            var hmac = new HMac(sha3);
            hmac.Init(new KeyParameter(key));
            hmac.BlockUpdate(numberBytes, 0, numberBytes.Length);
            return GetHmacResult(hmac, sha3);
        }

        private static byte[] GetHmacResult(HMac hmac, Sha3Digest sha3)
        {
            byte[] hash = new byte[sha3.GetDigestSize()];
            hmac.DoFinal(hash, 0);
            return hash;
        }
    }
}
