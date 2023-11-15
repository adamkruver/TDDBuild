using Cysharp.Threading.Tasks;

namespace Sources.PresentationInterfaces.Views.Systems.PathDraw
{
    public interface IPathPointView
    {
        UniTask Show();
        UniTask Hide();
        void Clear();
    }
}