using UnityEngine;
using Game.Level;

namespace Game.UI
{
    public class GameUISwitch : MonoBehaviour
    {
        [SerializeField] private PauseController _pauseController;
        [SerializeField] private UIGroup _gameUI;

        private void OnEnable()
        {
            _pauseController.IsPaused += OnGamePaused;
            _pauseController.IsResumed += OnGameResumed;
        }

        private void OnDisable()
        {
            _pauseController.IsPaused -= OnGamePaused;
            _pauseController.IsResumed -= OnGameResumed;
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
}