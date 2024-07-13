using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;

namespace Game
{
    public class ProgressReseter : MonoBehaviour
    {
        [SerializeField] private ProgressSaver _progressSaver;

        [ProButton]
        public void ResetProgress()
        {
            _progressSaver.CheckLastCompletedLevelIndex(OnLastCompletedLevelIndexRecieved);     
        }

        private void OnLastCompletedLevelIndexRecieved(int lastCompletedLevel)
        {
            for (int i = 1; i <= lastCompletedLevel; i++)
                _progressSaver.DeleteLevelProgress(i);
        }
    }
}