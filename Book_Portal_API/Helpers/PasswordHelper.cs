using System.Security.Cryptography;

namespace Book_Portal_API.Helpers
{
    public class PasswordHelper
    {
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;
        private static readonly int Iterations = 10000;

        public static string Encode(string password)
        {
            byte[] salt;
            rng.GetBytes(salt = new byte[SaltSize]);
            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = key.GetBytes(HashSize);

            var hashByte = new byte[SaltSize + HashSize];

            Array.Copy(salt, 0, hashByte, 0, SaltSize);
            Array.Copy(hash, 0, hashByte, SaltSize, HashSize);

            var base64Hash = Convert.ToBase64String(hashByte);

            return base64Hash;
        }

        public static bool Decode(string password, string base64Hash)
        {
            var hashByte = Convert.FromBase64String(base64Hash);

            var salt = new byte[SaltSize];

            Array.Copy(hashByte, 0, salt, 0, SaltSize);

            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = key.GetBytes(HashSize);

            for (var i = 0; i < HashSize; i++)
            {
                if (hashByte[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;

        }
    }
}
