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
    }
}
