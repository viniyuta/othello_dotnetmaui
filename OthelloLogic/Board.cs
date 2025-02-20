using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NetworkExtension;

namespace OthelloLogic
{
    public class Board
    {
        public static readonly int COLS = 8;
        public static readonly int ROWS = 8;
        private readonly Stone[,] boardStones = new Stone[COLS, ROWS];

        public Stone this[int col, int row]
        {
            get { return boardStones[col, row]; }
            private set { boardStones[col, row] = value; }
        }

        public Stone this[Position pos]
        {
            get { return this[pos.Column, pos.Row]; }
            private set { this[pos.Column, pos.Row] = value; }
        }

        public static Board Initial()
        {
            Board board = new();
            board.AddStartPieces();
            return board;
        }

        private void AddStartPieces()
        {
            this[3, 3] = new Stone(Player.White);
            this[3, 4] = new Stone(Player.Black);
            this[4, 3] = new Stone(Player.Black);
            this[4, 4] = new Stone(Player.White);
        }

        private void AddPiece(Player stoneColor, Position pos)
        {
            if (IsEmpty(pos))
            {
                this[pos] = new Stone(stoneColor);
            }
        }

        public void MakePlay(Player player, Position pos)
        {
            IEnumerable<Direction> flippableDirections = FlippablePositionDirections(player, pos);
            if (flippableDirections.Any())
            {
                AddPiece(player, pos);
                foreach(Direction dir in flippableDirections)
                {
                    List<Position> flippableStones = FindFlippableStones(player, pos, dir);
                    FlipStones(flippableStones);
                }
            } else {
                Console.WriteLine("エラー： 駒を置けない場所が選択されました！");
            }
        }

        public Playables UpdatePlayables(Player player)
        {
            Dictionary<Position, IEnumerable<Direction>> flipplablePositions = FindPlayablePositions(player);
            return new Playables(flipplablePositions);
        }
        private Dictionary<Position, IEnumerable<Direction>> FindPlayablePositions(Player player)
        {
            Dictionary<Position, IEnumerable<Direction>> flippablePositions = [];

            for (int col = 0; col < COLS; col++) {
                for (int row = 0; row < ROWS; row++) {
                    Position pos = new(col, row);
                    if (IsEmpty(pos)) {
                        IEnumerable<Direction> flipplableDirections = FlippablePositionDirections(player, pos);
                        if (flipplableDirections.Any()) {
                            flippablePositions.Add(pos, flipplableDirections);
                        }
                    }
                }
            }
            return flippablePositions;
        }

        private void FlipStones(List<Position> positions)
        {
            foreach (Position pos in positions)
            {
                Player opponentColor = this[pos].Color.Opponent();
                this[pos] = new Stone(opponentColor);
            }
        }


        private IEnumerable<Direction> FlippablePositionDirections(Player player, Position pos)
        {
            foreach(Direction dir in Direction.AllDirections)
            {
                if (IsInside(pos + dir))
                {
                    if (!IsEmpty(pos + dir))
                    {
                        if (this[pos + dir].Color == player.Opponent())
                        {
                            List<Stone> stones = PositionsToStones(PositionsByDirection(pos, dir));
                            for (int i = 2; i < stones.Count; i++)
                            {
                                if (stones[i] == null) { break; }
                                if (stones[i].Color == player) { yield return dir; }
                            }
                        }
                    }
                }
            }
        }

        private List<Position> FindFlippableStones(Player player, Position pos, Direction dir)
        {
            List<Position> flippablePositions = [];
            pos += dir;
            while (IsInside(pos) && this[pos].Color == player.Opponent())
            {
                flippablePositions.Add(pos);
                pos += dir;
            }
            return flippablePositions;
        }

        private static List<Position> PositionsByDirection(Position pos, Direction dir)
        {
            List<Position> positions = [];
            while(IsInside(pos)) {
                positions.Add(pos);
                pos += dir;
            }
            return positions;
        } 


        private List<Stone> PositionsToStones(List<Position> positions)
        {
            List<Stone> stones = [];
            foreach (Position pos in positions)
            {
                stones.Add(this[pos]);
            }
            return stones;
        }

        public static bool IsInside(Position pos)
        {
            return pos.Column >= 0 && pos.Row >= 0 && pos.Column < COLS && pos.Row < ROWS;
        }

        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }

        public bool IsEmpty(int col, int row)
        {
            return this[col, row] == null;
        }

        public bool AllPlaced()
        {
            for (int col = 0; col < COLS; col++)
            {
                for (int row = 0; row < ROWS; row++)
                {
                    if (IsEmpty(col, row)) return false; 
                }
            }
            return true;
        }

        public Dictionary<Player, int> CountStones()
        {
            Dictionary<Player, int> counts = new()
            {
                {Player.Black, 0},
                {Player.White, 0}
            };

            for (int col = 0; col < COLS; col++)
            {
                for (int row = 0; row < ROWS; row++)
                {
                    if (!IsEmpty(col, row))
                    {
                        Player player = this[col, row].Color;
                        counts[player]++;
                    }
                }
            }
            return counts;
        }

        // private void AddSkipTestStartPieces()
        // {
        //     for (int row = 0; row < ROWS; row++)
        //     {
        //         this[2, row] = new Stone(Player.Black);
        //         this[3, row] = new Stone(Player.Black);
        //         this[4, row] = new Stone(Player.Black);
        //     }

        //     this[5,1] = new Stone(Player.Black);
        //     this[5,2] = new Stone(Player.White);
        //     this[5,3] = new Stone(Player.White);
        //     this[5,4] = new Stone(Player.White);

        //     for (int row = 1; row <= 3; row++)
        //     {
        //         this[6, row] = new Stone(Player.Black);
        //         this[7, row] = new Stone(Player.Black);
        //     }

        //     this[7,0] = new Stone(Player.White);
        // }
    }
}