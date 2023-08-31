using Microsoft.AspNetCore.Mvc;
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

    }
}
