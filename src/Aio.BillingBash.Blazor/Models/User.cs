using Aio.BillingBash.AspnetCore.Model;
using Microsoft.AspNetCore.Identity;

namespace Aio.BillingBash.Models
{
	public class User : IdentityUser, IHasCreationTime, IHasLastModificationTime
	{
		public DateTime CreationTime { get; set; }
		public DateTime? LastModificationTime { get; set; }
		public virtual ICollection<Party> JoinedParties { get; set; } = new List<Party>();
	}
}
