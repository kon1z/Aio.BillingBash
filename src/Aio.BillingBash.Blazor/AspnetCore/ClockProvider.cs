namespace Aio.BillingBash.AspnetCore
{
	public class ClockProvider : IClockProvider
	{
		public DateTime GetCurrentTime()
		{
			return DateTime.Now;
		}

		public DateTime GetCurrentUtcTime()
		{
			return DateTime.UtcNow;
		}
	}
}
