using Aio.BillingBash.AspnetCore;
using Aio.BillingBash.AspnetCore.Data;
using Aio.BillingBash.Data;
using Autofac;

namespace Aio.BillingBash
{
	public class AppModuleRegister : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			var allTypes = ThisAssembly.GetTypes();

			RegisterAppAspnetCoreService(builder);
			RegisterMiddlewares(builder, allTypes);
			RegisterApplicationService(builder, allTypes);

			base.Load(builder);
		}

		private void RegisterMiddlewares(ContainerBuilder builder, Type[] allTypes)
		{
			var middlewares = allTypes
				.Where(x => x.IsAssignableTo(typeof(IMiddleware)))
				.ToArray();
			builder.RegisterTypes(middlewares)
				.AsSelf()
				.PropertiesAutowired()
				.InstancePerDependency();
		}

		private void RegisterAppAspnetCoreService(ContainerBuilder builder)
		{
			builder.RegisterType<AppDbContext>()
				.AsSelf()
				.AsImplementedInterfaces()
				.PropertiesAutowired()
				.InstancePerLifetimeScope();

			builder.RegisterType<UnitOfWorkManager>()
				.AsSelf()
				.PropertiesAutowired()
				.InstancePerLifetimeScope();

			builder.RegisterType<CurrentUser>()
				.As<ICurrentUser>()
				.InstancePerLifetimeScope();

			builder.RegisterType<ClockProvider>()
				.As<IClockProvider>()
				.SingleInstance();
		}

		private void RegisterApplicationService(ContainerBuilder builder, Type[] allTypes)
		{
			var appServiceTypes = allTypes
				.Where(x => x.Name.EndsWith("AppService") && !x.IsAbstract)
				.ToArray();
			builder.RegisterTypes(appServiceTypes)
				.AsSelf()
				.AsImplementedInterfaces()
				.PropertiesAutowired()
				.InstancePerDependency();
		}
	}
}
