using Aio.BillingBash.AspnetCore.Model;

namespace Aio.BillingBash.Models
{
	public class PartyMember : IHasCreationTime, ISoftDelete, IHasDeletionTime
	{
		public Guid PartyId { get; set; }
		public Guid UserId { get; set; }

		public DateTime CreationTime { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletionTime { get; set; }
	}
}
