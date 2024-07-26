using System;
using System.Collections;
using UnityEngine;
using Game.Bomb;
using Game.Level.Map;
using Game.UI;
using Game.Level;

public class AdShower : MonoBehaviour
{
    [SerializeField] private MapObserver _mapObserver;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private BombsHolder _bombHolder;
    [SerializeField] private UIGroup _adGroup;
    [SerializeField] [Range(0f, 2f)] private float _adSuggestingDelay = 0.5f;

    public event Action AdStarted;
    public event Action AdEnded;

    private void OnEnable()
    {
        if (_mapObserver != null)
            _mapObserver.FrozenAppeared += OnFrozenCubeAppeared;

        if (_mapObserver != null)
            _inputHandler.BombThrowingFailed += OnBombThrowingFailed;
    }
    
    private void OnDisable()
    {
        if (_mapObserver != null)
            _mapObserver.FrozenAppeared -= OnFrozenCubeAppeared;
        
        if (_mapObserver != null)
            _inputHandler.BombThrowingFailed -= OnBombThrowingFailed;
    }

    public void ShowBombRewardedAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.VideoAd.Show(OnAdStarted, GiveBombReward, OnAdEnded);
#else
        OnAdStarted();
        GiveBombReward();
        OnAdEnded();
#endif
    }
    
    private bool TrySuggestBombAd()
    {
        if (_bombHolder.BombsAmount > 0)
            return false;
        
        StartCoroutine(SuggestingCoroutine());
        return true;
    }
    
    private IEnumerator SuggestingCoroutine()
    {
        yield return new WaitForSeconds(_adSuggestingDelay);

        _adGroup.TurnOn();
    }

    private void OnFrozenCubeAppeared()
    {
        TrySuggestBombAd();
    }
    
    private void OnBombThrowingFailed()
    {
        TrySuggestBombAd();
    }

    private void GiveBombReward() => _bombHolder.AddBomb();

    private void OnAdStarted() => AdStarted?.Invoke();

    private void OnAdEnded()
    {
        _adGroup.TurnOff();
        AdEnded?.Invoke();
    }
}