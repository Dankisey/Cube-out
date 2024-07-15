using UnityEngine;
using TMPro;

namespace Localization
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class TranslatingLabel : MonoBehaviour
    {
        [SerializeField] private Localizator _localizator;

        private TMP_Text _tmpText;

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
            string phraseCode = GetPhraseCode();
            RequestTranslation(phraseCode);
        }

        protected void AddText(string text) => _tmpText.text += text;
        
        protected abstract string GetPhraseCode();

        protected virtual void OnTextSetted() { }

        private void RequestTranslation(string phraseName) => _localizator.GetTranslation(phraseName, UpdateText);

        private void UpdateText(string text)
        {
            _tmpText.text = text;
            OnTextSetted();
        }
    }
}