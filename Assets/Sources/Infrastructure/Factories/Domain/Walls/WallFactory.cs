using Sources.Domain.Walls;

namespace Sources.Infrastructure.Factories.Domain.Walls
{
    public class WallFactory
    {
        public Wall Create()
        {
            return new Wall();
        }
    }
}