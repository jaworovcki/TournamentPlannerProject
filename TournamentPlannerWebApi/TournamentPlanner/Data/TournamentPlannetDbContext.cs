using Microsoft.EntityFrameworkCore;
using System.Data;

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

		public enum PlayerNumber { Player1 = 1, Player2 = 2 };

		public async Task<Player> AddPlayer(Player newPlayer)
		{
			Players.Add(newPlayer);
			return await SaveChangesAsync()
				.ContinueWith(_ => newPlayer);
		}

		public async Task<Match> AddMatch(int player1Id, int player2Id, int round)
		{
			var newMatch = new Match
			{
				FirstPlayerID = player1Id,
				SecondPlayerID = player2Id,
				Round = round
			};

			Matches.Add(newMatch);
			return await SaveChangesAsync()
				.ContinueWith(_ => newMatch);
		}

		public async Task<Match> SetWinner(int matchId, PlayerNumber player)
		{
			var match = await Matches.FirstOrDefaultAsync(m => m.ID == matchId);

			if (match != null)
			{
				if (player.Equals(1))
				{
					match.Winner = match.FirstPlayer;
				}
				else
				{
					match.Winner = match.SecondPlayer;

				}
			}
			else
			{
				throw new Exception("Match is not found");
			}

			return await SaveChangesAsync()
				.ContinueWith(_ => match);

		}


	}
}
