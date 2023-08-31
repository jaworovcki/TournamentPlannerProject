using System.ComponentModel.DataAnnotations;

namespace TournamentPlanner.Data
{
	public class Match
	{
		public int ID { get; set; }

		[Range(1, 5)]
		public int Round { get; set; }

		public Player FirstPlayer { get; set; } = null!;

        public int FistPlayerID { get; set; }

        public Player SecondPlayer { get; set; } = null!;

        public int SecondPlayerID { get; set; }

        public Player? WinnerPlayer { get; set; }

        public int? WinnerPlayerID { get; set; }
    }
}
