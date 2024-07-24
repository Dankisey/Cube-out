using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Game.Cube;

namespace Game.Level.Map
{
    public class MapObserver : MonoBehaviour
    {
        private List<Entity> _cubes;

        public event Action FrozenAppeared;

        private void OnDisable()
        {
            foreach (Entity cube in _cubes)
            {
                cube.Destroying -= OnCubeDestroying;
                cube.Frozen -= OnCubeFrozen;
            }
        }

        public void Initialize(Entity[] cubes)
        {
            _cubes = cubes.ToList();

            foreach (Entity cube in cubes)
            {
                cube.Frozen += OnCubeFrozen;
                cube.Destroying += OnCubeDestroying;
            }
        }

        private void OnCubeDestroying(Entity entity)
        {
            entity.Destroying -= OnCubeDestroying;
            entity.Frozen -= OnCubeFrozen;
            _cubes.Remove(entity);
        }

        private void OnCubeFrozen()
        {
            FrozenAppeared?.Invoke();
        }
    }
}