namespace Aio.BillingBash.AspnetCore.Model
{
	public interface IHasCreator
	{
		Guid CreatorUserId { get; set; }
	}
}
