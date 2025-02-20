using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OthelloLogic
{
    public class GameState
    {
        public Board Board { get; }
        public Playables Playables { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public Dictionary<Player, int> StoneCounts { get; private set; }
        public bool SkippedLastPlayer { get; private set; } = false;
        public Result Result { get; private set; } = null;


        public GameState(Player player, Board board)
        {
            CurrentPlayer = player;
            Board = board;
            Playables = Board.UpdatePlayables(CurrentPlayer);
            StoneCounts =  Board.CountStones();
        }
        
        public void PlayStone(Position pos)
        {
            Board.MakePlay(CurrentPlayer, pos);
            CurrentPlayer = CurrentPlayer.Opponent();
            SkippedLastPlayer = false;
            Playables = Board.UpdatePlayables(CurrentPlayer);
            StoneCounts = Board.CountStones();
        }

        public void SkipPlayer()
        {
            CurrentPlayer = CurrentPlayer.Opponent();
            Playables = Board.UpdatePlayables(CurrentPlayer);
            if (SkippedLastPlayer)
            {
                SkippedLastPlayer = false;
                EndGame();
            } 
            else 
            {
                SkippedLastPlayer = true;
            }
        }

        private void EndGame()
        {
            Player winner = CheckWinner();
            if (winner == Player.None)
            {
                Result = Result.Draw(StoneCounts);
            }
            else 
            {
                Result = Result.Win(winner, StoneCounts);
            }
        }

        private Player CheckWinner()
        {
            StoneCounts = Board.CountStones();
            int mostStones = 0;
            Player winner = Player.None;
            foreach(Player player in StoneCounts.Keys)
            {
                if (StoneCounts[player] > mostStones)
                {
                    mostStones = StoneCounts[player];
                    winner = player;
                }
                else if (StoneCounts[player] == mostStones)
                {
                    winner = Player.None;
                }
            }
            return winner;
        }

        public bool IsGameOver()
        {
            return Result != null;
        }

    }
}