using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class CustomButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        protected void EnableButtonInteractions()
        {
            _button.interactable = true;
        }

        protected void DisableButtonInteractions()
        {
            _button.interactable = false;
        }

        protected abstract void OnButtonClick();
    }
}