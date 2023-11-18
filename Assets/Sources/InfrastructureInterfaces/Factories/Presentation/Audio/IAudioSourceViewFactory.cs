using Sources.PresentationInterfaces.Audio;
using UnityEngine;

namespace Sources.InfrastructureInterfaces.Factories.Presentation.Audio
{
    public interface IAudioSourceViewFactory
    {
        IAudioSourceView Create(Vector3 position);
    }
}