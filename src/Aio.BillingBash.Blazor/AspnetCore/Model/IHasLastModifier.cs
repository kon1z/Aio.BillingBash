namespace Aio.BillingBash.AspnetCore.Model
{
	public interface IHasLastModifier
	{
		Guid LastModifierUserId { get; set; }
	}
}
