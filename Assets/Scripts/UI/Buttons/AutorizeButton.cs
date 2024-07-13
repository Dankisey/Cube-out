using UnityEngine;

namespace Game.UI.Buttons
{
    public class AutorizeButton : CustomButton
    {
        [SerializeField] private Authorizer _authorizer;
        [SerializeField] private UIGroup _mainGroup;
        [SerializeField] private UIGroup _authorizeGroup;

        protected override void OnButtonClick()
        {
            _authorizeGroup.TurnOff();
            _mainGroup.TurnOn();
            _authorizer.Authorize();
        }
    }
}