using Microsoft.EntityFrameworkCore;

namespace TournamentPlanner.Data
{
	public class TournamentPlannerDbContext : DbContext
	{
        public DbSet<Player> Players { get; set; } = null!;

        public DbSet<Match> Matches { get; set; } = null!;

        public TournamentPlannerDbContext(DbContextOptions<TournamentPlannerDbContext> options)
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

		public async Task<Player> AddPlayer(Player newPlayer)
		{
			Players.Add(newPlayer);
			return await SaveChangesAsync()
				.ContinueWith(_ => newPlayer);
		}
	}
}
