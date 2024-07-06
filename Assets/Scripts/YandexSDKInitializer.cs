using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class YandexSDKInitializer : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    { 
        yield return YandexGamesSdk.Initialize();

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.StartAuthorizationPolling(1500);
    }
}