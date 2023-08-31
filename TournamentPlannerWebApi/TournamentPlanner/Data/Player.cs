using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TournamentPlanner.Data
{
	public class Player
	{
		public int ID { get; set; }

		[Required]
		public string Name { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

		public Match Match { get; set; } = null!;
    }

}
