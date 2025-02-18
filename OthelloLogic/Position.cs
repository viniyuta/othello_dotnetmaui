namespace OthelloLogic
{
    public class Position(int col, int row)
    {
        public int Column { get; } = col;
        public int Row { get; } = row;

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                    Column == position.Column &&
                    Row == position.Row;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Column, Row);
        }

        public static bool operator ==(Position left, Position right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public static Position operator +(Position pos, Direction dir)
        {
            return new Position(pos.Column + dir.ColumnDelta, pos.Row + dir.RowDelta);
        }
    }
}