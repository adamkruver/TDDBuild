namespace Sources.Domain.Tiles
{
    public class TileModel
    {
        public TileModel(int x, int y, object @object)
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