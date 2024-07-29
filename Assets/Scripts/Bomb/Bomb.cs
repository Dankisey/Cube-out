using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Cube;

namespace Game.Bomb
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private ExplosionParticlesController _particlesController;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private float _delayBeforeDestroy;
        [SerializeField] private float _explosionRadius;

        private List<Entity> _affectedCubes = new();
        private Rigidbody _rigidbody;
        private Coroutine _lifeTimeCoroutine;
        private bool _isExplosionPrepared;
        private float _lifeTime = 10f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _lifeTimeCoroutine = StartCoroutine(LifeTimeCoroutine());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Entity cube))
                PrepareExplosion();
        }

        private void OnDestroy()
        {
            if (_lifeTimeCoroutine != null)
                StopCoroutine(_lifeTimeCoroutine);
        }

        public void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        private void PrepareExplosion()
        {
            if (_isExplosionPrepared)
                return;
            
            _isExplosionPrepared = true;
            Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out Entity cube))
                    _affectedCubes.Add(cube);
            }

            Instantiate(_particlesController, transform.position, Quaternion.identity);
            _meshRenderer.enabled = false;
            StartCoroutine(DestroyObjectsDelayed());
        }

        private IEnumerator LifeTimeCoroutine()
        {
            yield return new WaitForSeconds(_lifeTime);
            
            Destroy(gameObject);
        }

        private IEnumerator DestroyObjectsDelayed()
        {
            yield return new WaitForSeconds(_delayBeforeDestroy);

            foreach (var cube in _affectedCubes)
                cube.Explode();
            
            Destroy(gameObject);
        }
    }
}