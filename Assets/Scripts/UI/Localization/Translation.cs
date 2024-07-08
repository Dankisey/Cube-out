using UnityEngine;

public class Translation : MonoBehaviour
{
    [field : SerializeField] public string Language { get; private set; }
    [TextArea(1, 5)] [SerializeField] private string _translatedText;

    public string TranslatedText => _translatedText;
}