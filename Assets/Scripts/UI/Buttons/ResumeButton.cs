using UnityEngine;
using Game.Level;

namespace Game.UI.Buttons
{
    public class ResumeButton : CustomButton
    {
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private UIGroup _pauseMenuGroup;

        protected override void OnButtonClick()
        {
            _pauseController.Resume();
            _pauseMenuGroup.TurnOff();
        }
    }
}