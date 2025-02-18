namespace OthelloLogic
{
    public class Direction(int columnDelta, int rowDelta)
    {
        public readonly static Direction North = new Direction(0, -1);
        public readonly static Direction South = new Direction(0, 1);
        public readonly static Direction East = new Direction(1, 0);
        public readonly static Direction West = new Direction(-1, 0);
        public readonly static Direction NorthEast = North + East;
        public readonly static Direction NorthWest = North + West;
        public readonly static Direction SouthEast = South + East;
        public readonly static Direction SouthWest = South + West;
        public readonly static IEnumerable<Direction> AllDirections = [North, South, East, West, NorthEast, NorthWest, SouthEast, SouthWest];

        public int ColumnDelta { get; } = columnDelta;
        public int RowDelta { get; } = rowDelta;

        public static Direction operator +(Direction dir1, Direction dir2)
        {
            return new Direction(dir1.ColumnDelta + dir2.ColumnDelta, dir1.RowDelta + dir2.RowDelta);
        }

        public static Direction operator *(int scalar, Direction dir)
        {
            return new Direction(scalar * dir.ColumnDelta, scalar * dir.RowDelta);
        }
    }
}