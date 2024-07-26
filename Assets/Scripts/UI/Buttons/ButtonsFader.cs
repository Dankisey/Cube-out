using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Buttons
{
    public class ButtonsFader : MonoBehaviour
    {
        [SerializeField] private List<CustomButton> _buttons;
        [SerializeField] private CustomButton _fadedOnStart;

        private CustomButton _currentlyFaded;
        
        private void OnValidate()
        {
            if (_buttons == null || _fadedOnStart == null)
                return;
            
            if(_buttons.Contains(_fadedOnStart) == false)
                Debug.LogWarning("Buttons list does not contain faded on start button");
        }

        private void Awake()
        {
            _currentlyFaded = _fadedOnStart;
            _fadedOnStart.Fade();
        }

        private void OnEnable()
        {
            foreach (var button in _buttons)
                button.Activated += OnActivated;
        }

        private void OnDisable()
        {
            foreach (var button in _buttons)
                button.Activated -= OnActivated;
        }

        private void OnActivated(CustomButton activatedButton)
        {
            _currentlyFaded.UnFade();
            activatedButton.Fade();
            _currentlyFaded = activatedButton;
        }
    }
}