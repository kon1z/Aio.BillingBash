using System.Security.Cryptography;
using System.Text;

namespace Aio.BillingBash.Helper
{
	public static class PasswordHashHelper
	{
		public static string HashPassword(string password)
		{
			using var sha256Hash = SHA256.Create();
			var hash = BitConverter
				.ToString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)))
				.Replace("-", "").ToLower();  
			return hash;
		} 
	}
}
