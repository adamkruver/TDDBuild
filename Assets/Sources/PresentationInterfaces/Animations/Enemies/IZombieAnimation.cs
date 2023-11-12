using Cysharp.Threading.Tasks;

namespace Sources.PresentationInterfaces.Animations.Enemies
{
    public interface IZombieAnimation
    {
        void Hit(float lastHitForwardProjection);
        UniTask Fall(float lastHitForwardProjection);
        void ResetToIdle();
    }
}