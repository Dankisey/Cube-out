using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Localization
{
    public class Phrase : MonoBehaviour
    {
        [SerializeField] private List<Translation> _translations;

        [field: SerializeField] public string Code { get; private set; }

        public string GetTranslation(string language)
        {
            Translation translation = _translations.Where(t => t.Language.ToLower() == language).FirstOrDefault();

            return translation.TranslatedText;
        }
    }
}