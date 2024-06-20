using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelSettingsScriptableObject", order = 0)]
public class LevelSettings : ScriptableObject
{
    [field: SerializeField] public int AvailableLevels { get; private set; }
    [field: SerializeField] public int MaxStars { get; private set; } = 3;
}