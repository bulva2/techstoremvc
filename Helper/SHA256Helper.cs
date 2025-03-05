using System.Security.Cryptography;
using System.Text;

namespace TechStoreMVC.Helper
{
    public class SHA256Helper
    {
        public static string HashPassword(string password)
        {
            byte[] data = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
