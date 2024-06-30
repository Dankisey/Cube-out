using UnityEngine;

public class GameUISwitch : MonoBehaviour
{
    [SerializeField] private LevelPauseController _levelPauseController;
    [SerializeField] private UIGroup _gameUI;

    private void OnEnable()
    {
        _levelPauseController.IsPaused += OnGamePaused;
        _levelPauseController.IsResumed += OnGameResumed;
    }

    private void OnDisable()
    {
        _levelPauseController.IsPaused -= OnGamePaused;
        _levelPauseController.IsResumed -= OnGameResumed;
    }

    private void OnGamePaused()
    {
        _gameUI.TurnOff();
    }

    private void OnGameResumed()
    {
        _gameUI.TurnOn();
    }
}