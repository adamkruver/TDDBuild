using System;
using System.Collections.Generic;
using Sources.Controllers.Bullets;
using Sources.Domain.Bullets;
using Sources.Presentation.Audio;
using Sources.PresentationInterfaces.Views.Bullets;

namespace Sources.Infrastructure.Factories.Controllers.Bullets
{
    public class BulletPresenterFactory
    {
        public BulletPresenter Create(IBulletView view, IBullet bullet, AudioMixerView audioMixerView) =>
            new BulletPresenter(view, bullet, audioMixerView);
    }
}