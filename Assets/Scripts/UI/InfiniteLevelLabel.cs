using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class InfiniteLevelLabel : MonoBehaviour
{
    private const string Infinite = nameof(Infinite);
    private const string Level = nameof(Level);

    [SerializeField] private LeanLocalization _localization;

    private void Awake()
    {
        _localization.SetCurrentLanguage($"{YandexGamesSdk.Environment.i18n.lang}");
        TMP_Text tmpText = GetComponent<TMP_Text>();
        string infinite = LeanLocalization.GetTranslationText(Infinite);
        string level = LeanLocalization.GetTranslationText(Level);
        tmpText.text = $"{infinite} {level}";
    }
}
