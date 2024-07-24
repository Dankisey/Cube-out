using System;
using UnityEngine;
using Game.Bomb;
using Game.Level.Map;
using Game.UI;
using System.Collections;

public class AdShower : MonoBehaviour
{
    [SerializeField] private MapObserver _mapObserver;
    [SerializeField] private BombsHolder _bombHolder;
    [SerializeField] private UIGroup _adGroup;
    [SerializeField][Range(0f, 2f)] private float _adSuggestingDelay = 1f;

    public event Action AdStarted;
    public event Action AdEnded;

    private void OnEnable()
    {
        if (_mapObserver != null)
            _mapObserver.FrozenAppeared += OnFrozenCubeAppeared;
    }

    private void OnDisable()
    {
        if (_mapObserver != null)
            _mapObserver.FrozenAppeared -= OnFrozenCubeAppeared;
    }

    public void SuggestAd()
    {
        _adGroup.TurnOn();
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

    private IEnumerator SuggestingCoroutine()
    {
        yield return new WaitForSeconds(_adSuggestingDelay);

        _adGroup.TurnOn();
    }

    private void OnFrozenCubeAppeared()
    {
        if (_bombHolder.BombsAmount <= 0)
            StartCoroutine(SuggestingCoroutine());
    }

    private void GiveBombReward() => _bombHolder.AddBomb();

    private void OnAdStarted() => AdStarted?.Invoke();

    private void OnAdEnded()
    {
        _adGroup.TurnOff();
        AdEnded?.Invoke();
    }
}