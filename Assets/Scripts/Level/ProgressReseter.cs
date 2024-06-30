using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

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