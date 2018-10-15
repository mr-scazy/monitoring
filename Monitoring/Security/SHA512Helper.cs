using System;
using System.Security.Cryptography;
using System.Text;

namespace Monitoring.Security
{
    public static class SHA512Helper
    {
        public static string GetHash(string text)        
        {
            using(var sha512 = SHA512.Create())
            {
                var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));
                var hash = Convert.ToBase64String(bytes);
                return hash;
            }
        }
    }
}
