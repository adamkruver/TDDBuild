using Sources.Controllers.Systems;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.Ui.Systems;
using TMPro;
using UnityEngine;

namespace Sources.Presentation.Ui.Systems.Progresses
{
    public class ProgressSystemUi : PresentationViewBase<ProgressSystemPresenter>, IProgressSystemUi
    {
        [SerializeField] private TextMeshProUGUI _progress;

        public void SetProgress(string progress) =>
            _progress.text = progress;
    }
}