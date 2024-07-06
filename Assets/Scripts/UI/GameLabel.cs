using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TMP_Text))]
public class GameLabel : MonoBehaviour
{
    private const string GameName = nameof(GameName);

    [SerializeField] private LeanLocalization _localization;

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();

        _localization.SetCurrentLanguage($"{YandexGamesSdk.Environment.i18n.lang}");
        TMP_Text tmpText = GetComponent<TMP_Text>();
        string gameName = LeanLocalization.GetTranslationText(GameName);
        tmpText.text = gameName;
    }
}