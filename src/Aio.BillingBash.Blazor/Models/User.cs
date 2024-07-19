using Aio.BillingBash.AspnetCore.Model;

namespace Aio.BillingBash.Models
{
	public class User : IHasCreationTime, IHasLastModificationTime
	{
		public User(Guid id, string username, string passwordHash)
		{
			Id = id;
			Username = username;
			PasswordHash = passwordHash;
		}

		public Guid Id { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public DateTime CreationTime { get; set; }
		public DateTime LastModificationTime { get; set; }
	}
}
