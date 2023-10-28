namespace Sources.Domain.Grids
{
    public class GridCell
    {
        public GridCell(int x, int y, object @object)
        {
            X = x;
            Y = y;
            Object = @object;
        }

        public int X { get; }
        public int Y { get; }
        public object Object { get; }
    }
}