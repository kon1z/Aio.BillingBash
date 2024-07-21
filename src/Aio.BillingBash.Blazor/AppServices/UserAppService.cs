using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aio.BillingBash.AspnetCore.AppServices;
using Aio.BillingBash.Components.ViewModels;
using Aio.BillingBash.Data;
using Aio.BillingBash.Helper;
using Aio.BillingBash.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;

namespace Aio.BillingBash.AppServices
{
    public class UserAppService : AppServiceBase
	{
		private readonly AppDbContext _dbContext;
		private readonly IConfiguration _configuration;
		private readonly JSRuntime _jsRuntime;

		public UserAppService(
			AppDbContext dbContext,
			IConfiguration configuration,
			JSRuntime jsRuntime)
		{
			_dbContext = dbContext;
			_configuration = configuration;
			_jsRuntime = jsRuntime;
		}

		public async Task<IdentityUserDto> RegisterAsync(RegisterInput input)
		{
			var user = new User(Guid.NewGuid(), input.Name, PasswordHashHelper.HashPassword(input.Password));

			await _dbContext.Users.AddAsync(user);
			await _dbContext.SaveEntitiesAsync();

			return AutoMapper.Map<User, IdentityUserDto>(user);
		}
	}
}
