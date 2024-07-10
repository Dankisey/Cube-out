using UnityEngine;
using Agava.YandexGames;
using TMPro;

namespace Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class TranslatingLabel : MonoBehaviour
    {
        [SerializeField] private Localizator _localization;

        private void OnEnable()
        {
            TMP_Text tmpText = GetComponent<TMP_Text>();
            string languageCode = GetCurrentLanguageCode();
            SetLanguage(GetCurrentLanguage(languageCode));
            tmpText.text = GetText();
        }

        protected abstract string GetText();

        protected string GetTranslation(string phraseName)
        {
            return _localization.GetTranslation(phraseName);
        }

        private string GetCurrentLanguageCode()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        return YandexGamesSdk.Environment.i18n.lang.Trim();
#else
            return "ru";
#endif
        }

        private string GetCurrentLanguage(string languageCode)
        {
            return languageCode switch
            {
                "ru" => "Russian",
                "en" => "English",
                "tr" => "Turkish",
                _ => "English"
            };
        }

        private void SetLanguage(string language)
        {
            _localization.ChangeLanguage(language);
        }
    }
}