using System.Text;
using System.Security.Cryptography;

namespace ClickSharp.Helpers
{
    public class HashHelper
    {
        public string GetHash(string input)
        {
            using (SHA256 hashAlgorithm = SHA256.Create())
            {
                byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                var sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
        public bool VerifyHash(string pwFromDB, string pw)
        {
            var hashOfInput = GetHash(pw);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, pwFromDB) == 0;
        }
    }
}
