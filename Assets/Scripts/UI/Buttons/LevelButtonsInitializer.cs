using UnityEngine;

namespace Game.UI.Buttons
{
    public class LevelButtonsInitializer : MonoBehaviour
    {
        [SerializeField] private ProgressSaver _progressSaver;
        [SerializeField] private LevelLoadButton[] _buttons;

        private void Start()
        {
            _progressSaver.LoadProgress(OnProgressLoaded);
        }

        private void OnProgressLoaded()
        {
            int maxAvailableLevel = _progressSaver.GetLastCompletedLevelIndex();

            foreach (LevelLoadButton button in _buttons)
                button.Initialize(maxAvailableLevel);
        }
    }
}