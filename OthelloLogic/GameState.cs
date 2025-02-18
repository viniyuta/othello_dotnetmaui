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
        private bool skippedLastPlayer = false;
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
            Playables = Board.UpdatePlayables(CurrentPlayer);
            StoneCounts = Board.CountStones();
            CheckSkipPlayer();
        }

        private void CheckSkipPlayer()
        {
            if (!Playables.HasPlayable())
            {
                SkipCurrentPlayer();
            }
            else 
            {
                skippedLastPlayer = false;
            }
        }

        private void SkipCurrentPlayer()
        {
            if (skippedLastPlayer)
            {
                EndGame();
            }
            else 
            {
                CurrentPlayer = CurrentPlayer.Opponent();
                skippedLastPlayer = true;
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
            // show game over screen
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