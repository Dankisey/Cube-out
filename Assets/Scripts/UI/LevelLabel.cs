using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class LevelLabel : MonoBehaviour
{
    private void Awake()
    {
        TMP_Text tmpText = GetComponent<TMP_Text>();
        tmpText.text = SceneManager.GetActiveScene().name;
    }
}
