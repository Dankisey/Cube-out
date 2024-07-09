using UnityEngine;
using Game.Bomb;
using TMPro;

namespace Game.UI
{
    public class BombsAmountDisplay : MonoBehaviour
    {
        [SerializeField] private BombsHolder _holder;
        [SerializeField] private TMP_Text _display;

        private void OnEnable()
        {
            _holder.BombsAmountChanged += OnBombsAmountChanged;
        }

        private void OnDisable()
        {
            _holder.BombsAmountChanged -= OnBombsAmountChanged;
        }

        private void OnBombsAmountChanged(int amount)
        {
            _display.text = $"{amount}";
        }
    }
}