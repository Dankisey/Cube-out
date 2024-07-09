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
            int lastCompletedLevel = _progressSaver.GetLastCompletedLevelIndex();

            for (int i = 1; i <= lastCompletedLevel; i++)
                _progressSaver.DeleteLevelProgress(i);
        }
    }
}