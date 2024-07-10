using UnityEngine;
using UnityEngine.UI;
using Game.Level;
using TMPro;

namespace Game.UI.Buttons
{
    public class LevelLoadButton : CustomButton
    {
        [SerializeField] private Loader _loader;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Color _textDisabledColor;
        [SerializeField] private Image _lockImage;
        [SerializeField] private int _levelIndex;

        public void Initialize(int maxAvailableLevel)
        {
            if (_levelIndex <= maxAvailableLevel + 1)
                Unlock();
        }

        protected override void OnAwake()
        {
            _label.text = $"{_levelIndex}";
            Lock();
        }

        protected override void OnButtonClick()
        {
            _loader.TryLoadLevel(_levelIndex);
        }

        private void Lock()
        {
            _label.color = _textDisabledColor;
            _lockImage.enabled = true;
            DisableButtonInteractions();
        }

        private void Unlock()
        {
            _label.color = Color.black;
            _lockImage.enabled = false;
            EnableButtonInteractions();
        }
    }
}