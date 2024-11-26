using System.Security.Cryptography;
using System.Text;

namespace WrestlingTournamentSystem.DataAccess.Helpers
{
    public static class Extensions
    {
        public static string ToSha256(this string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
