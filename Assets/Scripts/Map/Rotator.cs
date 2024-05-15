using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Rigidbody _target;
    [SerializeField] private float _rotationSpeed;

    public void Rotate(Vector3 delta)
    {
        delta *= _rotationSpeed * Time.deltaTime;
        _target.AddTorque(new Vector3(delta.y, -delta.x));
    }
}