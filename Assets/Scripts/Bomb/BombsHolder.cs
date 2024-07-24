using UnityEngine;
using System;
using com.cyborgAssets.inspectorButtonPro;
#if UNITY_WEBGL && !UNITY_EDITOR
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;
#endif

namespace Game.Bomb
{
    public class BombsHolder : MonoBehaviour
    {
        private const string Bombs = nameof(Bombs);

        [SerializeField] private Bomb _prefab;
        [SerializeField] private int _bombsOnStart = 3;

        public int BombsAmount { get; private set; }

        public event Action<int> BombsAmountChanged;

        private void Awake()
        {
            if (PlayerPrefs.HasKey(Bombs))
                BombsAmount = PlayerPrefs.GetInt(Bombs);
            else
                BombsAmount = _bombsOnStart;
        }

        private void Start()
        {
            UpdateSavedBombsValue();
        }

        public void AddBomb()
        {
            BombsAmount++;
            UpdateSavedBombsValue();
        }

        public void AddBombs(int amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            BombsAmount += amount;
            UpdateSavedBombsValue();
        }

        public bool TryGetBombPrefab(out Bomb prefab)
        {
            prefab = null;

            if (BombsAmount <= 0)
                return false;

            prefab = _prefab;
            BombsAmount--;
            UpdateSavedBombsValue();

            return true;
        }

        [ProButton]
        private void ResetBombsAmount()
        {
            BombsAmount = _bombsOnStart;
            UpdateSavedBombsValue();
        }

        private void UpdateSavedBombsValue()
        {
            BombsAmountChanged?.Invoke(BombsAmount);
            PlayerPrefs.SetInt(Bombs, BombsAmount);
            PlayerPrefs.Save();
        }
    }
}