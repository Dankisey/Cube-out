using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelLoader : MonoBehaviour
{
    private const string Level = nameof(Level);

    [SerializeField] private ProgressSaver _levelProgressSaver;

    public void LoadLastAvailableLevel()
    {
        int lastAvailableLevelIndex = _levelProgressSaver.GetLastCompletedLevelIndex() + 1;
        int levelsAmount = _levelProgressSaver.GetLevelsAmount();

        if (levelsAmount < 1)
            throw new ArgumentOutOfRangeException(nameof(levelsAmount));

        lastAvailableLevelIndex = Mathf.Clamp(lastAvailableLevelIndex, 1, levelsAmount);
        TryLoadLevel(lastAvailableLevelIndex);
    }

    public bool TryLoadLevel(int levelIndex)
    {
        if (levelIndex > _levelProgressSaver.GetLevelsAmount() || levelIndex <= 0)
            return false;

        SceneManager.LoadScene($"{Level} {levelIndex}");

        return true;
    }
}