using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Buttons
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public abstract class CustomButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private Color _startColor;
        private Color _fadedColor = new Color(0.7f, 0.7f, 0.7f);

        public event Action<CustomButton> Activated;
        
        private void OnValidate()
        {
            _button ??= GetComponent<Button>();
            _image ??= GetComponent<Image>();
            _startColor = _image.color;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnActivated);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnActivated);
        }

        public void Fade() => _image.color *= _fadedColor;

        public void UnFade() => _image.color = _startColor;

        protected void EnableButtonInteractions()
        {
            _button.interactable = true;
        }

        protected void DisableButtonInteractions()
        {
            _button.interactable = false;
        }
        
        protected abstract void OnButtonClick();

        private void OnActivated()
        {
            Activated?.Invoke(this);
            OnButtonClick();
        }
    }
}