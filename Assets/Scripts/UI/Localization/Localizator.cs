using System;
using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    public class Localizator : MonoBehaviour
    {
        [SerializeField] private List<Phrase> _phrases;
        [SerializeField] private string _currentLanguage;

        private Dictionary<string, Phrase> _phrasesDictionary = new();
        private Action _callbacksQueue = null;
        private bool _isInitialized = false;

        private void Awake()
        {
            string language = GetCurrentLanguage();
            FillDictionary();
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
                string result = _phrasesDictionary[phraseCode].GetTranslation(_currentLanguage);
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

        private void FillDictionary()
        {
            foreach (var phrase in _phrases)
            {
                _phrasesDictionary.Add(phrase.Code, phrase);
                phrase.Initialize();
            }
        }
    }
}