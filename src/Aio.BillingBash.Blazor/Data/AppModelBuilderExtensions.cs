using Aio.BillingBash.Models;
using Microsoft.EntityFrameworkCore;

namespace Aio.BillingBash.Data
{
	public static class AppModelBuilderExtensions
	{
		public static void ConfigAppEntities(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(b =>
			{
				b.ToTable("Sys_User");

				b.HasKey(x => x.Id);
				b.Property(x => x.Username).IsRequired().HasMaxLength(20);
				b.Property(x => x.PasswordHash).IsRequired().HasMaxLength(1024);

				b.HasIndex(x => x.Username);
			});
		}
	}
}
