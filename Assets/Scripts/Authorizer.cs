using System;
using UnityEngine;
using Agava.YandexGames;

namespace Game
{
    public class Authorizer : MonoBehaviour
    {
        private const int True = 1;
        private const int False = 0;

        public bool IsPlayerAuthorized
        {
            get
            {
#if UNITY_WEBGL && !UNITY_EDITOR
                return PlayerAccount.IsAuthorized;
#else
                return IsPlayerAuthorized;
#endif
            }

            private set
            {
                IsPlayerAuthorized = value;
            }
        }

        public bool IsPersonalDataAllowed => GetDataPermission();

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

        public void AllowDataRequest()
        {
            PlayerPrefs.SetInt(nameof(IsPersonalDataAllowed), True);
        }

        private bool GetDataPermission()
        {
            if (PlayerPrefs.HasKey(nameof(IsPersonalDataAllowed)) == false)
            {
                PlayerPrefs.SetInt(nameof(IsPersonalDataAllowed), False);

                return false;
            }

            int value = PlayerPrefs.GetInt(nameof(IsPersonalDataAllowed));

            return value == True;
        }
    }
}