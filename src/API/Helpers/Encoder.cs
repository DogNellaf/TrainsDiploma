using System.Security.Cryptography;
using System.Text;

namespace API.Helpers
{
    public static class Encoder
    {
        public static string Encode(string raw)
        {
            if (raw is null)
                raw = DateTime.Now.ToFileTimeUtc().ToString();

            var Sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(raw));

                foreach (var b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public static bool CheckHash(string value, string hash)
        {
            var valueHash = Encode(value);
            if (valueHash == hash)
            { 
                return true; 
            }
            return false;
        }
    }
}
