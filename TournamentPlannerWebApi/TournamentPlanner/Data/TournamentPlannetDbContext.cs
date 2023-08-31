using Microsoft.EntityFrameworkCore;

namespace TournamentPlanner.Data
{
	public class TournamentPlannetDbContext : DbContext
	{
        public DbSet<Player> Players { get; set; } = null!;

        public DbSet<Match> Matches { get; set; } = null!;

        public TournamentPlannetDbContext(DbContextOptions<TournamentPlannetDbContext> options)
            : base(options)
        { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Match>()
				.HasOne(m => m.FirstPlayer)
				.WithMany()
				.HasForeignKey(m => m.FirstPlayerID)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Match>()
				.HasOne(m => m.SecondPlayer)
				.WithMany()
				.HasForeignKey(m => m.SecondPlayerID)
				.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<Match>()
				.HasOne(m => m.Winner)
				.WithMany()
				.HasForeignKey(m => m.WinnerID)
				.OnDelete(DeleteBehavior.NoAction);

		}
	}
}
