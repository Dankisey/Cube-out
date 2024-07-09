using UnityEngine;
using Game.Cube;

namespace Game.Bomb
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _explosionRadius;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Entity cube))
            {
                DoExplosion();
            }
        }

        public void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        private void DoExplosion()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Entity cube))
                    cube.Explode();
            }

            Destroy(gameObject);
        }
    }
}