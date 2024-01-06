using System;
using System.Security.Cryptography;
using System.Text;

namespace Kursova.Utils
{
    public static class Hasher
    {
        public static string HashSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ByteArrayToString(hashBytes);
            }
        }

        public static bool CompareHashValues(string hashedPassword, string plainPassword)
        {
            return hashedPassword.Equals(HashSHA256(plainPassword));
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            StringBuilder sb = new StringBuilder(arrInput.Length);
            for (int i = 0; i < arrInput.Length; i++)
            {
                sb.Append(arrInput[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
