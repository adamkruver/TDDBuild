namespace Sources.PresentationInterfaces.Ui.Systems
{
    public interface IAggressiveSystemUi
    {
        void SetLevelProgressNormalized(float normalizedProgress);
        void SetProgress(string progress);
        void SetLevelTitle(string title);
        void SetLevelProgress(string levelProgress);
    }
}