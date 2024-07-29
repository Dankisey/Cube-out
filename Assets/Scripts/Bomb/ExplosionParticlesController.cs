using System.Collections;
using UnityEngine;

namespace Game.Bomb
{
    public class ExplosionParticlesController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particlesPrefab;

        private void Awake()
        {
            var time = _particlesPrefab.main.duration;
            StartCoroutine(DestroySelf(time));
        }

        private IEnumerator DestroySelf(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            Destroy(gameObject);
        }
    }
}