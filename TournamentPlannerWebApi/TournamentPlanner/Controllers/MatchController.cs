using Microsoft.AspNetCore.Mvc;
using TournamentPlanner.Data;

namespace TournamentPlanner.Controllers
{
	[ApiController]
	[Route("/api/matches")]
	public class MatchController : ControllerBase
	{
		private readonly TournamentPlannerDbContext _dbContext;

		public MatchController(TournamentPlannerDbContext dbContext)
        {
			_dbContext = dbContext;
		}

		[HttpGet("open")]
		public async Task<IList<Match>> GetAllIncompletMatchesAsync()
			=> await _dbContext.GetIncompleteMatches();

		[HttpPost("generate")]
		public async Task GenerateNextRoundMatchesAsync()
			=> await _dbContext.GenerateMatchesForNextRound();
    }
}
