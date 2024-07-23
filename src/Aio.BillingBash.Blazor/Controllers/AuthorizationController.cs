using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;

namespace Aio.BillingBash.Controllers
{
	[Controller]
	public class AuthorizationController : Controller
	{
		[HttpPost("~/connect/authorize")]
		public async Task<IActionResult> Authorize()
		{
			return Ok();
		}

		[ActionName("~/connect/logout"), HttpPost("~/connect/logout"), ValidateAntiForgeryToken]
		public async Task<IActionResult> LogoutPost()
		{
			return SignOut(
				authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
				properties: new AuthenticationProperties
				{
					RedirectUri = "/"
				});
		}

		[HttpPost("~/connect/token"), IgnoreAntiforgeryToken, Produces("application/json")]
		public async Task<IActionResult> Exchange()
		{
			return Ok();
		}

		[HttpPost("connect/token")]
		public IActionResult Token()
		{
			return Ok();
		}
	}
}
