using System.Security.Cryptography;
using System.Text;

namespace Lambda.Generic {
    public static class Hashing {
        public static string ComputeSha256Hash(string rawData) {
            // Create a SHA256
            // ComputeHash - returns byte array
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string
            StringBuilder builder = new();

            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));

            return builder.ToString();
        }
    }
}
