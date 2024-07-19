namespace Aio.BillingBash.AspnetCore
{
	public interface IClockProvider
	{
		DateTime GetCurrentTime();
		DateTime GetCurrentUtcTime();
	}
}
