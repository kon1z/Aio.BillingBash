using Aio.BillingBash;
using Aio.BillingBash.AspnetCore.Middlewares;
using Aio.BillingBash.Components;
using Aio.BillingBash.Data;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
	.ConfigureContainer<ContainerBuilder>(containerBuilder =>
	{
		containerBuilder.RegisterModule<AppModuleRegister>();
	});

var configuration = builder.Configuration;

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseNpgsql(configuration.GetConnectionString("Default"));
	options.UseOpenIddict();
});
//builder.Services.AddOpenIddict()
//	.AddCore(options =>
//	{
//		options.UseEntityFrameworkCore()
//			.UseDbContext<AppDbContext>();
//	})
//	.AddClient(options =>
//	{
//		options.AllowAuthorizationCodeFlow();
//#if DEBUG
//		options.AddDevelopmentEncryptionCertificate();
//		options.AddDevelopmentSigningCertificate();
//#endif
//		options.UseAspNetCore()
//			.EnableRedirectionEndpointPassthrough();

//		options.UseSystemNetHttp()
//			.SetProductInformation(typeof(Program).Assembly);
//	})
//	.AddServer(options =>
//	{
//		options.SetAuthorizationEndpointUris("authorize")
//			.SetTokenEndpointUris("token");

//		options.AllowAuthorizationCodeFlow();
//#if DEBUG
//		options.AddDevelopmentEncryptionCertificate()
//			.AddDevelopmentSigningCertificate();
//#endif
//		options.UseAspNetCore()
//			.EnableAuthorizationEndpointPassthrough();
//	})
//	.AddValidation(options =>
//	{
//		options.UseLocalServer();
//		options.UseAspNetCore();
//	});
builder.Services.AddAutoMapper(options =>
{
	options.AddProfile<AppAutoMapperProfile>();
});

builder.Services.AddAuthorization()
	.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseMiddleware<UnitOfWorkMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
