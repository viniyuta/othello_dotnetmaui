using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OthelloLogic
{
    public class Result
    {
        public Player Winner { get; }
        public Dictionary<Player, int> StoneCounts { get; }

        public Result(Player winner, Dictionary<Player, int> stoneCounts)
        {
            Winner = winner;
            StoneCounts = stoneCounts;
        }

        public static Result Win(Player winner, Dictionary<Player, int> stoneCounts)
        {
            return new Result(winner, stoneCounts);
        }

        public static Result Draw(Dictionary<Player, int> stoneCounts)
        {
            return new Result(Player.None, stoneCounts);
        }
    }
}