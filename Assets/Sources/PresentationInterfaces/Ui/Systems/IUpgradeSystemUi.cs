namespace Sources.PresentationInterfaces.Ui.Systems
{
    public interface IUpgradeSystemUi
    {
        void SetCooldownValue(string cooldown);
        void SetMaxFireDistanceValue(string maxFireDistance);
        void SetDamageValue(string damage);
        void SetCooldownLevel(int level);
        void SetMaxFireDistanceLevel(int level);
        void SetDamageLevel(int level);
    }
}