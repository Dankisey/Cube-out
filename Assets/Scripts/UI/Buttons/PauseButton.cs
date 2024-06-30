using UnityEngine;

public class PauseButton : CustomButton
{
    [SerializeField] private LevelPauseController _pauseController;
    [SerializeField] private UIGroup _pauseMenuGroup;

    protected override void OnButtonClick()
    {
        _pauseController.Pause();
        _pauseMenuGroup.TurnOn();
    }
}