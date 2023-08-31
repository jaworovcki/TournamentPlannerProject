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
				match.Winner = player switch
				{
					PlayerNumber.Player1 => match.FirstPlayer,
					PlayerNumber.Player2 => match.SecondPlayer,
					_ => throw new ArgumentOutOfRangeException(nameof(player)),
				};
			}
			else
			{
				throw new Exception("Match is not found");
			}

			return await SaveChangesAsync()
				.ContinueWith(_ => match);
		}

		public async Task<IList<Match>> GetIncompleteMatches() 
			=> await Matches.Where(m => m.Winner == null)
				.AsNoTracking()
				.ToListAsync();

		public async Task DeleteEverything()
		{
			await Matches.ForEachAsync(match => Remove(match));
			await Players.ForEachAsync(player => Remove(player));
			await SaveChangesAsync();
		}

		public async Task<IList<Player>> GetFilteredPlayers(string playerFilter = null)
		{
			var players = await Players.Where(player => player.Name.Contains(playerFilter))
				.ToListAsync();

			if (players.Any())
			{
				return players;
			}
			else
			{
				return await Players.ToListAsync();
			}
		}

		public async Task GenerateMatchesForNextRound()
		{
			if (Matches.Any(m => m.Winner == null))
				throw new Exception("Not all matches have been finished!");

			if (await Players.CountAsync() != 32)
				throw new Exception("Incorrect number of players!");

			var numOfMatches = await Matches.CountAsync();
			switch (numOfMatches)
			{
				case 0:
					AddFirstRound(Matches, await GetFilteredPlayers());
					break;
				case var n when n is 16 or 24 or 28 or 32:
					await AddSubsequentRound(Matches);
					break;
				default:
					throw new InvalidOperationException("Invalid number of rounds!");
			}

			await SaveChangesAsync();
		}

		static void AddFirstRound(DbSet<Match> matches, IList<Player> players)
		{
			var rnd = new Random();
			for (var i = 0; i < 16; i++)
			{
				var player1 = players[rnd.Next(players.Count)];
				players.Remove(player1);
				var player2 = players[rnd.Next(players.Count)];
				players.Remove(player2);

				matches.Add(new()
				{
					FirstPlayer = player1,
					SecondPlayer = player2,
					Round = 1
				}); ;
			}
		}

		static async Task AddSubsequentRound(DbSet<Match> matches)
		{
			var rnd = new Random();
			var prevRound = await matches.MaxAsync(m => m.Round);
			var prevRoundMatches = await matches.Where(m => m.Round == prevRound)
				.ToListAsync();
			var winnersList = prevRoundMatches.Select(m => m.Winner)
					.ToList();

			for (var n = prevRoundMatches.Count()/2; n > 0; n--)
			{
				var player1 = winnersList[rnd.Next(winnersList.Count)];
				winnersList.Remove(player1);
				var player2 = winnersList[rnd.Next(winnersList.Count)];
				winnersList.Remove(player2);

				matches.Add(new()
				{
					FirstPlayer = player1,
					SecondPlayer = player2,
					Round = prevRound + 1
				});
			}
		}
	}
}
