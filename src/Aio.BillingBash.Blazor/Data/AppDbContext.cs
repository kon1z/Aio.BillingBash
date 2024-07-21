using Aio.BillingBash.AspnetCore.Data;
using Aio.BillingBash.Models;
using Microsoft.EntityFrameworkCore;

namespace Aio.BillingBash.Data
{
	public sealed class AppDbContext : EfCoreDbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Party> Parties { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ConfigAppEntities();

			base.OnModelCreating(modelBuilder);
		}
	}
}
