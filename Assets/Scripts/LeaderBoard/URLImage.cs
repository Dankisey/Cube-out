using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

namespace LeaderBoard
{
    [RequireComponent(typeof(Image))]
    public class URLImage : MonoBehaviour
    {
        private Image _target;

        private void Awake()
        {
            _target = GetComponent<Image>();
        }

        public void Initialize(string url)
        {
            StartCoroutine(LoadImage(url));
        }

        private IEnumerator LoadImage(string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);
                _target.sprite = sprite;
            } 
        }
    }
}