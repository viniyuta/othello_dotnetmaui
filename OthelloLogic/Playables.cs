using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OthelloLogic
{
    public class Playables
    {
        private static readonly int COLS = Board.COLS;
        private static readonly int ROWS = Board.ROWS;

        private readonly IEnumerable<Direction>[,] playables = new IEnumerable<Direction>[COLS, ROWS];

        public IEnumerable<Direction> this[int col, int row]
        {
            get { return playables[col, row]; }
        }

        public IEnumerable<Direction> this[Position pos]
        {
            get { return playables[pos.Column, pos.Row]; }
        }

        public Playables(Dictionary<Position, IEnumerable<Direction>> playablePositionsWithDirections)
        {
            foreach(Position pos in playablePositionsWithDirections.Keys)
            {
                SetPlayable(pos, playablePositionsWithDirections[pos]);
            }
        }

        public void Clear()
        {
            for (int col = 0; col < COLS; col++)
            {
                for (int row = 0; row < ROWS; row++)
                {
                    playables[col, row] = Enumerable.Empty<Direction>();
                }
            }
        }

        private void SetPlayable(Position pos, IEnumerable<Direction> directions)
        {
            playables[pos.Column, pos.Row] = directions;
        }

        public bool IsPlayable(Position pos)
        {
            return playables[pos.Column, pos.Row] != null;
        }

        public bool IsPlayable(int col, int row)
        {
            return playables[col, row] != null;
        }

        public bool HasPlayable()
        {
            for (int col = 0; col < COLS; col++)
            {
                for (int row = 0; row < ROWS; row++)
                {
                    if (IsPlayable(col, row)) { return true; }
                }
            }

            return false;
        }

    }
}