namespace Sources.Domain.Systems.Upgrades
{
    public class UpgradeSystem
    {
        private const int MaxLevel = 10;
        
        public UpgradeSystem(float cooldownDelta = -.1f, float maxFireDistanceDelta = .5f, float damageDelta = 5f)
        {
            Cooldown = new UpgradeModifier(cooldownDelta, MaxLevel);
            MaxFireDistance = new UpgradeModifier(maxFireDistanceDelta, MaxLevel);
            Damage = new UpgradeModifier(damageDelta, MaxLevel);
        }

        public UpgradeModifier Cooldown { get; }
        public UpgradeModifier MaxFireDistance { get; }
        public UpgradeModifier Damage { get; }
    }
}