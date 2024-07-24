using System;
using UnityEngine;
using Agava.YandexGames;

namespace Game
{
    public class Authorizer : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        public bool IsPlayerAuthorized => PlayerAccount.IsAuthorized;
#else
        public bool IsPlayerAuthorized { get; private set; }
#endif

        public void Authorize()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.Authorize();
#else
            IsPlayerAuthorized = true;
#endif
        }

        public void DoDataRequest(Action callback)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.RequestPersonalProfileDataPermission(onSuccessCallback: callback);
#else
            callback.Invoke();
#endif
        }
    }
}