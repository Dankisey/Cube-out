using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Star : MonoBehaviour
{
    [SerializeField] private Color _disabledColor;
    [SerializeField] private Color _enabledColor;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.color = _disabledColor;
    }

    public void Activate()
    {
        _image.color = _enabledColor;
    }

    public void Deactivate()
    {
        _image.color = _disabledColor;
    }
}