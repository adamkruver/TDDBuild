using System;

namespace Sources.App.Di.Activation
{
    public interface IActivateStrategy
    {
        object Activate(Type type);
    }
}