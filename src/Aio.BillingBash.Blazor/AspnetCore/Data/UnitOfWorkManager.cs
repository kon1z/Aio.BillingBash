namespace Aio.BillingBash.AspnetCore.Data
{
	public class UnitOfWorkManager
	{
		public IServiceProvider ServiceProvider { get; set; }

		private IUnitOfWork UnitOfWork => ServiceProvider.GetRequiredService<IUnitOfWork>();

		public async Task<IUnitOfWork> BeginAsync()
		{
			await UnitOfWork.InitializeAsync();
			return UnitOfWork;
		}
	}
}
