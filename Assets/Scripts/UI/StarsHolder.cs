using UnityEngine;
using Game.Level;

namespace Game.UI
{
    public class StarsHolder : MonoBehaviour
    {
        [SerializeField] private ProgressObserver _progressObserver;
        [SerializeField] private Star[] _stars;

        private void OnEnable()
        {
            _progressObserver.StarsAmountChanged += OnStarsAmountChanged;
        }

        private void OnDisable()
        {
            _progressObserver.StarsAmountChanged -= OnStarsAmountChanged;
        }

        private void OnStarsAmountChanged(int starsAmount)
        {
            for (int i = 0; i < starsAmount; i++)
                _stars[i].Activate();
        }
    }
}