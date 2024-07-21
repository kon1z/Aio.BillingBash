using Aio.BillingBash.AspnetCore.Model;

namespace Aio.BillingBash.Models
{
    public class Party : IFullAuditModel
	{
		public Party(Guid id, string name, string? description = null)
		{
			Id = id;
			Name = name;
			Description = description;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }

		public DateTime CreationTime { get; set; }
		public Guid CreatorUserId { get; set; }
		public DateTime? LastModificationTime { get; set; }
		public Guid LastModifierUserId { get; set; }
		public bool IsDeleted { get; set; }
		public Guid DeletionUserId { get; set; }
		public DateTime? DeletionTime { get; set; }

		public virtual ICollection<User> Users { get; set; }
		//public virtual ICollection<Item> Items { get; set; }
	}
}
