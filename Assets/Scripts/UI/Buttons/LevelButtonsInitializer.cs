using UnityEngine;

namespace Game.UI.Buttons
{
    public class LevelButtonsInitializer : MonoBehaviour
    {
        [SerializeField] private ProgressSaver _progressSaver;
        [SerializeField] private LevelLoadButton[] _buttons;

        private void Start()
        {
            _progressSaver.CheckLastCompletedLevelIndex(OnLastCompletedLevelRecieved);
        }

        private void OnLastCompletedLevelRecieved(int completedLevel)
        {
            foreach (LevelLoadButton button in _buttons)
                button.Initialize(completedLevel);
        }
    }
}