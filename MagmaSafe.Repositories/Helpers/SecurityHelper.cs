using System.Text;
using System.Security.Cryptography;
using MagmaSafe.Borders.Repositories.Helpers;

namespace MagmaSafe.Repositories.Helpers
{
    public class SecurityHelper : ISecurityHelper
    {
        public string MD5Hash(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(passwordBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
