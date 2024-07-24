using UnityEngine;
using System.Collections.Generic;

namespace Localization
{
    public class Phrase : MonoBehaviour
    {
        [SerializeField] private List<Translation> _translations;

        private Dictionary<string, Translation> _translationsDictionary = new();

        [field: SerializeField] public string Code { get; private set; }

        public void Initialize()
        {
            foreach (var translation in _translations)
                _translationsDictionary.Add(translation.Language.ToLower(), translation);
        }

        public string GetTranslation(string language) => _translationsDictionary[language.ToLower()].TranslatedText;
    }
}