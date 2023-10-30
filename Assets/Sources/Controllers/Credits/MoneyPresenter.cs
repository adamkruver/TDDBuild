using Sources.Domain.Credits;
using Sources.Presentation.Ui;

namespace Sources.Controllers.Credits
{
    public class MoneyPresenter
    {
        private readonly TextUi _textUi;

        public MoneyPresenter(TextUi textUi, Money money)
        {
            _textUi = textUi;
            money.Value.AddListener(UpdateView);
        }

        private void UpdateView(int value) =>
            _textUi.SetText(value.ToString());
    }
}