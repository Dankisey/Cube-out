using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class CustomButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    protected void DisableButtonInteractions()
    {
        _button.interactable = false;
    }

    protected abstract void OnButtonClick();
}