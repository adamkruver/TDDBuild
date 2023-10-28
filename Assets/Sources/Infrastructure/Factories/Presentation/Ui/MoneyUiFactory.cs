using Sources.Controllers.Credits;
using Sources.Domain.Credits;
using Sources.Presentation.Ui;
using UnityEngine;

namespace Sources.Infrastructure.Factories.Presentation.Ui
{
    public class MoneyUiFactory
    {
        private const string PrefabPath = "Ui/Credits/MoneyUi";
        
        public TextUi Create(Money money)
        {
            TextUi ui = Object.Instantiate(Resources.Load<TextUi>(PrefabPath));
            MoneyPresenter presenter = new MoneyPresenter(ui, money);
            
            return ui;
        }
    }
}