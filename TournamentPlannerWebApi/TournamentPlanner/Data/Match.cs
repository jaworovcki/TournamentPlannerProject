using System.ComponentModel.DataAnnotations;

namespace TournamentPlanner.Data
{
	public class Match
	{
		public int ID { get; set; }

		[Range(1, 5)]
		public int Round { get; set; }

		public Player? FirstPlayer { get; set; }

        public int FirstPlayerID { get; set; }

        public Player? SecondPlayer { get; set; } 

        public int SecondPlayerID { get; set; }

        public Player? Winner { get; set; }

        public int? WinnerID { get; set; }
    }
}
