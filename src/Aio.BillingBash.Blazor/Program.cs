using Aio.BillingBash.AspnetCore.Middlewares;
using Aio.BillingBash.Components;
using Aio.BillingBash.Helper;

namespace Aio.BillingBash;

public static class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder
			.ConfigureSerilog()
			.ConfigureAutofac()
			.ConfigureIdentityServices()
			//.ConfigureOpenIddictServices()
			.ConfigureAutoMapperServices()
			.ConfigurationApiControllerServices()
			.ConfigureSwaggerServices()
			.ConfigureBlazorServices()
			.ConfigureEntityFrameworkCoreServices();

		var app = builder.Build();

		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error", createScopeForErrors: true);
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseSwagger();
		app.UseSwaggerUI();

		app.UseStaticFiles(); 

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseMiddleware<UnitOfWorkMiddleware>();

		app.UseAntiforgery();

		app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode();

		app.MapControllers();

		app.Run();
	}
}