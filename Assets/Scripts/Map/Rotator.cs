using UnityEngine;

namespace Game.Level.Map
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _startRotation;
        [SerializeField] private Rigidbody _target;
        [SerializeField] private float _rotationSpeed;

        private void Awake()
        {
            _target.transform.rotation = Quaternion.Euler(_startRotation);
        }

        public void Rotate(Vector3 delta)
        {
            delta *= _rotationSpeed * Time.deltaTime;
            _target.AddTorque(new Vector3(delta.y, -delta.x));
        }
    }
}