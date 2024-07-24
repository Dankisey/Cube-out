using UnityEngine;
using Game.Bomb;
using Game.Level;

namespace Game.UI.Buttons
{
    public class BombButton : CustomButton
    {
        [SerializeField] private InputHandler _input;
        [SerializeField] private BombsHolder _bombHolder;
        [SerializeField] private AdShower _adShower;
        [SerializeField] private UIGroup _mainGroup;

        protected override void OnButtonClick()
        {
            if (_bombHolder.BombsAmount <= 0)
            {
                _adShower.SuggestAd();
                _mainGroup.TurnOff();
            }
            else
            {
                _input.ActivateBombAiming();
            }
        }
    }
}