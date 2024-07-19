using AutoMapper;

namespace Aio.BillingBash.AspnetCore.AppServices
{
	public abstract class AppServiceBase
	{
		public IServiceProvider ServiceProvider { get; set; } = null!;
		protected IMapper AutoMapper => ServiceProvider.GetRequiredService<IMapper>();
		protected ICurrentUser CurrentUser => ServiceProvider.GetRequiredService<ICurrentUser>();
		protected IClockProvider ClockProvider => ServiceProvider.GetRequiredService<IClockProvider>();
	}
}
