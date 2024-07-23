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
				b.HasMany(x => x.JoinedParties).WithMany(x => x.Users);
			});

			modelBuilder.Entity<Party>(b =>
			{
				b.ToTable("Party");

				b.HasKey(x => x.Id);
				b.Property(x => x.Name).IsRequired().HasMaxLength(20);
				b.Property(x => x.Description).HasMaxLength(1024);

				b.HasIndex(x => x.Name);
			});
		}
	}
}
