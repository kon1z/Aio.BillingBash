using Aio.BillingBash;
using Aio.BillingBash.AspnetCore.Middlewares;
using Aio.BillingBash.Components;
using Aio.BillingBash.Data;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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
});
builder.Services.AddAutoMapper(options =>
{
	options.AddProfile<AppAutoMapperProfile>();
});

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

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
