namespace Aio.BillingBash.AspnetCore.Data
{
	public interface IUnitOfWork
	{
		Task SaveEntitiesAsync(CancellationToken cancellationToken = default);
		Task InitializeAsync();
		Task CompleteAsync(CancellationToken cancellationToken);
	}
}
