using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CubeSettingsScriptableObject", order = 1)]
public class CubeSettings : ScriptableObject
{
    [field: SerializeField] public float DistanceBeforeDestroy { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float TrembleDistance { get; private set; }
    [field: SerializeField] public float TrembleTime { get; private set; }
    [field: SerializeField] public int BumpsToFreeze { get; private set; }
}