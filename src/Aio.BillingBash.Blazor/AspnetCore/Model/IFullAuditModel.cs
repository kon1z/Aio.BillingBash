namespace Aio.BillingBash.AspnetCore.Model
{
	public interface IFullAuditModel : IHasCreationTime, IHasCreator, IHasLastModificationTime, IHasLastModifier, ISoftDelete, IHasDeletionUser, IHasDeletionTime
	{
	}
}
