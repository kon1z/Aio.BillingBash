using Microsoft.EntityFrameworkCore.Storage;

namespace Aio.BillingBash.AspnetCore.Data
{
	public interface ITransaction
	{
		IDbContextTransaction? GetCurrentTransaction();

		bool HasActiveTransaction { get; }

		Task<IDbContextTransaction> BeginTransactionAsync();

		Task CommitTransactionAsync(CancellationToken cancellationToken);

		void RollbackTransaction();
	}
}
