using Sources.Controllers.Bullets;
using Sources.Domain.Bullets;
using Sources.Presentation.Audio;
using Sources.Presentation.Views.Bullets;

namespace Sources.Infrastructure.Factories.Controllers.Bullets
{
    public class RocketPresenterFactory
    {
        public RocketPresenter Create(RocketView view, Rocket rocket, AudioMixerView audioMixerView) =>
            new RocketPresenter(view, rocket, audioMixerView);
    }
}