using Sources.Controllers.Systems;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.Ui.Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Presentation.Ui.Systems.Upgrades
{
    public class UpgradeSystemUi : PresentationViewBase<UpgradeSystemPresenter>, IUpgradeSystemUi
    {
        [SerializeField] private Button _upgradeCooldownButton;
        [SerializeField] private Button _upgradeMaxFireDistanceButton;
        [SerializeField] private Button _upgradeDamageButton;
        [SerializeField] private TextMeshProUGUI _cooldownText;
        [SerializeField] private TextMeshProUGUI _maxFireDistanceText;
        [SerializeField] private TextMeshProUGUI _damageText;

        protected override void OnBeforeEnable()
        {
            if(Presenter is null)
                return;
            
            _upgradeCooldownButton.onClick.AddListener(Presenter.UpgradeCooldown);
            _upgradeMaxFireDistanceButton.onClick.AddListener(Presenter.UpgradeMaxFireDistance);
            _upgradeDamageButton.onClick.AddListener(Presenter.UpgradeDamage);
        }

        protected override void OnAfterDisable()
        {
            if(Presenter is null)
                return;
            
            _upgradeCooldownButton.onClick.RemoveListener(Presenter.UpgradeCooldown);
            _upgradeMaxFireDistanceButton.onClick.RemoveListener(Presenter.UpgradeMaxFireDistance);
            _upgradeDamageButton.onClick.RemoveListener(Presenter.UpgradeDamage);
        }

        public void SetCooldown(string cooldown) => 
            _cooldownText.text = cooldown;
        
        public void SetMaxFireDistance(string maxFireDistance) =>
            _maxFireDistanceText.text = maxFireDistance;
        
        public void SetDamage(string damage) => 
            _damageText.text = damage;
    }
}