using Cysharp.Threading.Tasks;

namespace Sources.Infrastructure.Resource
{
    public abstract class ResourceObject
    {
        public abstract string Path { get; }

        public abstract UniTask LoadAsync();

        public abstract void Unload();
    }
}