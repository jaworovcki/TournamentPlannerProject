using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentPlanner.Data;

namespace TournamentPlanner.Controllers
{
	[ApiController]
	[Route("api")]
	public class PlayersController : ControllerBase
	{
		private readonly TournamentPlannerDbContext _dbContext;

		public PlayersController(TournamentPlannerDbContext dbContext)
        {
			_dbContext = dbContext;
		}

		[HttpGet("players")]
		public async Task<IList<Player>> GetAllPlayersAsync() => await _dbContext.Players
			.AsNoTracking()
			.ToListAsync();

    }
}
