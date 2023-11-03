using Sources.Frameworks.LiveDatas;

namespace Sources.Domain.Systems.Progresses
{
    public class ProgressSystem
    {
        private readonly MutableLiveData<Progress> _progress;

        public ProgressSystem(long progress = 0) =>
            _progress = new MutableLiveData<Progress>(new Progress(progress));

        public LiveData<Progress> Progress => _progress;

        public void AddProgress(long progress) =>
            _progress.Value += new Progress(progress);
    }
}