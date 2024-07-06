using com.cyborgAssets.inspectorButtonPro;
using System;
using UnityEngine;
#if UNITY_WEBGL && !UNITY_EDITOR
using PlayerPrefs = Agava.YandexGames.Utility.PlayerPrefs;
#endif

public class BombsHolder : MonoBehaviour
{
    private const string Bombs = nameof(Bombs);

    [SerializeField] private Bomb _prefab;
    [SerializeField] private int _bombsOnStart = 3;

    private int _bombsAmount;

    public event Action<int> BombsAmountChanged;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(Bombs))
            _bombsAmount = PlayerPrefs.GetInt(Bombs);
        else
            _bombsAmount = _bombsOnStart;
    }

    private void Start()
    {
        UpdateSavedBombsValue();
    }

    public void AddBombs(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        _bombsAmount += amount;
        UpdateSavedBombsValue();
    }

    public bool TryGetBombPrefab(out Bomb prefab)
    {
        prefab = null;

        if (_bombsAmount <= 0)
            return false;

        prefab = _prefab;
        _bombsAmount--;
        UpdateSavedBombsValue();

        return true;
    }

    [ProButton]
    private void ResetBombsAmount()
    {
        _bombsAmount = _bombsOnStart;
        UpdateSavedBombsValue();
    }

    private void UpdateSavedBombsValue()
    {
        BombsAmountChanged?.Invoke(_bombsAmount);
        PlayerPrefs.SetInt(Bombs, _bombsAmount);
        PlayerPrefs.Save();
    }
}