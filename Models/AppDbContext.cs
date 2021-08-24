using Microsoft.EntityFrameworkCore;

namespace BabbApi.Models
{
	public sealed class AppDbContext : DbContext
	{
		public DbSet<TodoItem> TodoItems { get; set; }
		public DbSet<User> Users { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Username)
				.IsUnique();
			modelBuilder.Entity<User>()
				.HasData(new User
				{
					Id = 1,
					Username = "rob",
					Password = "rob"
				});
		}
	}
}