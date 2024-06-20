using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private const string Level = nameof(Level);

    [SerializeField] private LevelProgressSaver _levelProgressSaver;

    public void LoadLastAvailableLevel()
    {
        int lastAvailableLevelIndex = _levelProgressSaver.GetLastCompletedLevelIndex() + 1;
        lastAvailableLevelIndex = Mathf.Clamp(lastAvailableLevelIndex, 1, _levelProgressSaver.GetLevelsAmount());
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