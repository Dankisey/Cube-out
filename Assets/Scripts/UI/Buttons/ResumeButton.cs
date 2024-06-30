using UnityEngine;

public class ResumeButton : CustomButton
{
    [SerializeField] private LevelPauseController _pauseController;
    [SerializeField] private UIGroup _pauseMenuGroup;

    protected override void OnButtonClick()
    {
        _pauseController.Resume();
        _pauseMenuGroup.TurnOff();
    }
}