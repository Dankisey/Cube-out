using UnityEngine;
using Game.Level;
using UnityEngine.UI;

namespace Game.UI.Buttons
{
    public class MusicButton : CustomButton
    {
        [SerializeField] private MusicController _musicController;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Sprite _unmuted;
        [SerializeField] private Sprite _muted;

        private void Awake()
        {
            Sprite currentState = _musicController.IsMuted ? _muted : _unmuted;
            _buttonImage.sprite = currentState;
        }

        protected override void OnButtonClick()
        {
            if (_musicController.IsMuted)
                Unmute();
            else
                Mute();
        }

        private void Mute()
        {
            _musicController.MuteAll();
            _buttonImage.sprite = _muted;
        }

        private void Unmute()
        {
            _musicController.UnmuteAll();
            _buttonImage.sprite = _unmuted;
        }
    }
}