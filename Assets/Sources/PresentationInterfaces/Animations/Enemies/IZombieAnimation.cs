using Cysharp.Threading.Tasks;

namespace Sources.PresentationInterfaces.Animations.Enemies
{
    public interface IZombieAnimation
    {
        void Hit();
        UniTask Die();
        void ResetToIdle();
    }
}