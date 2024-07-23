using Aio.BillingBash.Data;
using Aio.BillingBash.Models;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace Aio.BillingBash.Helper
{
	public static class WebApplicationHelper
	{
		public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
		{
			return builder;
		}

		public static WebApplicationBuilder ConfigureAutofac(this WebApplicationBuilder builder)
		{
			builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureContainer<ContainerBuilder>(containerBuilder =>
				{
					containerBuilder.RegisterModule<AppModuleRegister>();
				});

			return builder;
		}

		public static WebApplicationBuilder ConfigureOpenIddictServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddOpenIddict()
				.AddCore(options =>
				{
					options.UseEntityFrameworkCore()
						.UseDbContext<AppDbContext>();
				})
				.AddClient(options =>
				{
					options.AllowAuthorizationCodeFlow();
					options.AddDevelopmentEncryptionCertificate();
					options.AddDevelopmentSigningCertificate();
					options.UseAspNetCore()
						.EnableStatusCodePagesIntegration()
						.EnableRedirectionEndpointPassthrough();

					options.UseSystemNetHttp()
						.SetProductInformation(typeof(Program).Assembly);
				})
				.AddServer(options =>
				{
					options.AllowAuthorizationCodeFlow()
						.AllowRefreshTokenFlow();

					options.SetAuthorizationEndpointUris("connect/authorize")
						.SetLogoutEndpointUris("connect/logout")
						.SetTokenEndpointUris("connect/token")
						.SetUserinfoEndpointUris("connect/userinfo");

					options.RegisterScopes(OpenIddictConstants.Scopes.Email, OpenIddictConstants.Scopes.Profile, OpenIddictConstants.Scopes.Roles);

					options.AddDevelopmentEncryptionCertificate()
						.AddDevelopmentSigningCertificate();

					options.UseAspNetCore()
						.EnableAuthorizationEndpointPassthrough()
						.EnableLogoutEndpointPassthrough()
						.EnableStatusCodePagesIntegration()
						.EnableTokenEndpointPassthrough();
				})
				.AddValidation(options =>
				{
					options.UseLocalServer();
					options.UseAspNetCore();
				});

			return builder;
		}

		public static WebApplicationBuilder ConfigureAutoMapperServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddAutoMapper(options =>
			{
				options.AddProfile<AppAutoMapperProfile>();
			});

			return builder;
		}

		public static WebApplicationBuilder ConfigureBlazorServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddRazorComponents()
				.AddInteractiveServerComponents();

			return builder;
		}

		public static WebApplicationBuilder ConfigureEntityFrameworkCoreServices(this WebApplicationBuilder builder)
		{
			var configuration = builder.Configuration;

			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				options.UseNpgsql(configuration.GetConnectionString("Default"));
				options.UseOpenIddict();
			});

			return builder;
		}

		public static WebApplicationBuilder ConfigureIdentityServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders()
				.AddDefaultUI();

			return builder;
		}

		public static WebApplicationBuilder ConfigureSwaggerServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddSwaggerGen();

			return builder;
		}

		public static WebApplicationBuilder ConfigurationApiControllerServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddControllers()
				.AddControllersAsServices();

			return builder;
		}
	}
}
