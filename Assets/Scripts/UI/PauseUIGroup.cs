using UnityEngine;

namespace Game.UI
{
    public class PauseUIGroup : UIGroup
    {
        [SerializeField] private BackgroundChangingObserver _backgroundChangingObserver;

        private void OnEnable()
        {
            _backgroundChangingObserver.IsRunningInBackground += TurnOn;
        }

        private void OnDisable()
        {
            _backgroundChangingObserver.IsRunningInBackground -= TurnOn;
        }
    }
}