using Sources.Domain.Systems.Progresses;
using Sources.Frameworks.LiveDatas;
using Sources.PresentationInterfaces.Ui.Systems;

namespace Sources.Controllers.Systems
{
    public class ProgressSystemPresenter : PresenterBase
    {
        private readonly IProgressSystemUi _ui;
        private readonly LiveData<Progress> _progress;

        public ProgressSystemPresenter(IProgressSystemUi ui, ProgressSystem system)
        {
            _ui = ui;
            _progress = system.Progress;
        }

        public override void Enable() =>
            _progress.AddListener(OnProgressChanged);

        public override void Disable() =>
            _progress.RemoveListener(OnProgressChanged);

        private void OnProgressChanged(Progress progress) =>
            _ui.SetProgress(progress.Value.ToString());
    }
}