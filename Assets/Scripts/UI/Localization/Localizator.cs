using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization.Editor;
using System;

namespace Localization
{
    public class Localizator : MonoBehaviour
    {
        [SerializeField] private List<Phrase> _phrases;
        [SerializeField] private string _currentLanguage;

        private Action _callbacksQueue = null;
        private bool _isInitialized = false;

        private void Awake()
        {
            string language = GetCurrentLanguage();
            ChangeLanguage(language);
        }

        public void ChangeLanguage(string language)
        {
            _currentLanguage = language.Trim().ToLower();
            _isInitialized = true;
            _callbacksQueue?.Invoke();
        }

        public void GetTranslation(string phraseCode, Action<string> callback)
        {
            if (_isInitialized == false)
            {
                _callbacksQueue += () =>
                {
                    GetTranslation(phraseCode, callback);
                };
            }
            else
            {
                Phrase phrase = _phrases.Where(p => p.Code.ToLower() == phraseCode.ToLower()).FirstOrDefault();
                string result =  phrase.GetTranslation(_currentLanguage);
                callback.Invoke(result);
            }    
        }

        private string GetCurrentLanguage()
        {
            string languageCode;

#if UNITY_WEBGL && !UNITY_EDITOR
            languageCode = YandexGamesSdk.Environment.i18n.lang.Trim();
#else
            languageCode =  "ru";
#endif

            return languageCode switch
            {
                "ru" => "Russian",
                "en" => "English",
                "tr" => "Turkish",
                _ => "English"
            };
        }
    }
}