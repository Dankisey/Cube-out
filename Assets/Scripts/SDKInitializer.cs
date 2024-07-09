using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;

namespace Game
{
    public class SDKInitializer : MonoBehaviour
    {
        private const string MainMenu = nameof(MainMenu);

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize(OnInitialized);
        }

        private void OnInitialized()
        {
            YandexGamesSdk.GameReady();
            SceneManager.LoadScene(MainMenu);
        }
    }
}