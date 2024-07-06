using UnityEngine.SceneManagement;
using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class LevelLabel : MonoBehaviour
{
    private const string Level = nameof(Level);

    [SerializeField] private LeanLocalization _localization;

    private void Awake()
    {
        _localization.SetCurrentLanguage($"{YandexGamesSdk.Environment.i18n.lang}");
        TMP_Text tmpText = GetComponent<TMP_Text>();
        string levelText = LeanLocalization.GetTranslationText(Level);
        int levelNumber = GetLevelNumber();
        tmpText.text = $"{levelText} {levelNumber}";
    }

    private int GetLevelNumber()
    {
        int level = 0;

        string sceneName = SceneManager.GetActiveScene().name;
        string[] sceneNameParts = sceneName.Split(' ');

        foreach (string sceneNamePart in sceneNameParts)
        {
            if (int.TryParse(sceneNamePart, out level))
                return level;
        }

        return level;
    }
}
