using Aio.BillingBash.AspnetCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Aio.BillingBash.AspnetCore.Data
{
	public abstract class EfCoreDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork, ITransaction
	{
		public ServiceProvider ServiceProvider { get; set; } = null!;
		private ICurrentUser CurrentUser => ServiceProvider.GetRequiredService<ICurrentUser>();
		private IClockProvider ClockProvider => ServiceProvider.GetRequiredService<IClockProvider>();

		public async Task InitializeAsync()
		{
			ChangeTracker.Tracked += ChangeTracker_Tracked;
			ChangeTracker.StateChanged += ChangeTracker_StateChanged;

			await BeginTransactionAsync();
		}

		public async Task CompleteAsync(CancellationToken cancellationToken)
		{
			await SaveEntitiesAsync(cancellationToken);
			await CommitTransactionAsync(cancellationToken);
		}

		private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
		{
            SetAuditProperties(e.Entry);
		}

		private void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs e)
		{
            SetAuditProperties(e.Entry);
		}

		private void SetAuditProperties(EntityEntry entry)
		{
			if (entry.State == EntityState.Added)
			{
				if (entry.Entity is IHasCreator entityWithCreatorId)
				{
					entityWithCreatorId.CreatorUserId = CurrentUser.UserId;
				}

				if (entry.Entity is IHasCreationTime entityWithCreationTime)
				{
					entityWithCreationTime.CreationTime = ClockProvider.GetCurrentTime();
				}
			}

			if (entry.State == EntityState.Modified)
			{
				if (entry.Entity is IHasLastModifier entityWithLastModifier)
				{
					entityWithLastModifier.LastModifierUserId = CurrentUser.UserId;
				}

				if (entry.Entity is IHasLastModificationTime entityWithLastModificationTime)
				{
					entityWithLastModificationTime.LastModificationTime = ClockProvider.GetCurrentTime();
				}
			}

			if (entry.State == EntityState.Deleted)
			{
				if (entry.Entity is IHasDeleteUser entityWithDeleteUser)
				{
					entityWithDeleteUser.DeleteUserId = CurrentUser.UserId;
				}

				if (entry.Entity is ISoftDelete entityWithSoftDelete)
				{
					entityWithSoftDelete.IsDeleted = true;
					entry.State = EntityState.Modified;
				}
			}
		}

        public async Task SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
	        if (!HasActiveTransaction)
	        {
		        throw new InvalidOperationException("修改实体需要开启事务！");
	        }

	        await base.SaveChangesAsync(cancellationToken);
        }

        private IDbContextTransaction? _currentTransaction;
        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
	        if (_currentTransaction != null)
	        {
		        return Task.FromResult(_currentTransaction);
	        }

	        _currentTransaction = Database.BeginTransaction();
            return Task.FromResult(_currentTransaction);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
	        if (_currentTransaction == null)
	        {
		        throw new InvalidOperationException("修改实体需要开启事务！");
	        }

            try
            {
                await SaveChangesAsync(cancellationToken);
                await _currentTransaction.CommitAsync(cancellationToken);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
	}
}
