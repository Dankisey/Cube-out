using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Localization
{
    public class Localizator : MonoBehaviour
    {
        [SerializeField] private List<Phrase> _phrases;
        [SerializeField] private string _currentLanguage;

        public void ChangeLanguage(string language) => _currentLanguage = language.Trim().ToLower();

        public string GetTranslation(string phraseCode)
        {      
            Phrase phrase = _phrases.Where(p => p.Code.ToLower() == phraseCode.ToLower()).FirstOrDefault();

            return phrase?.GetTranslation(_currentLanguage);
        }
    }
}