using Sources.Controllers.Scenes;

namespace Sources.InfrastructureInterfaces.Factories.Scenes
{
    public interface ISceneFactory
    {
        IScene Create(object payload);
    }
}