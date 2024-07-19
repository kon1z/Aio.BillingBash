using Aio.BillingBash.AspnetCore.AppServices;
using Aio.BillingBash.Data;

namespace Aio.BillingBash.AppServices
{
	public class ItemAppService : AppServiceBase
	{
		private	readonly AppDbContext _context;

		public ItemAppService(AppDbContext context)
		{
			_context = context;
		}
	}
}
