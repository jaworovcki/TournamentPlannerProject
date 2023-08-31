using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TournamentPlanner.Data
{
	public class Player
	{
		public int ID { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[MaxLength(10)]
        public string? PhoneNumber { get; set; }
    }

}
