using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelLoadButton : CustomButton
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Color _textDisabledColor;
    [SerializeField] private Image _lockImage;
    [SerializeField] private int _levelIndex;

    private void Start()
    {
        _label.text = $"{_levelIndex}";

        if (_levelLoader.CanLoadLevel(_levelIndex) == false)
            Lock();
    }

    protected override void OnButtonClick()
    {
        _levelLoader.TryLoadLevel(_levelIndex);
    }

    private void Lock()
    {
        _label.color = _textDisabledColor;
        _lockImage.enabled = true;
        DisableButtonInteractions();
    }
}