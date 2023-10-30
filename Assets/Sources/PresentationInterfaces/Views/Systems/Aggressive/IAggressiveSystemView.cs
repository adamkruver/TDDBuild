namespace Sources.PresentationInterfaces.Views.Systems.Aggressive
{
    public interface IAggressiveSystemView
    {
        void SetLevelProgressNormalized(float normalizedProgress);
        void SetProgress(string progress);
        void SetLevelTitle(string title);
        void SetLevelProgress(string levelProgress);
    }
}