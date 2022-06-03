using System.Collections;
using PixelCrew.Creatures.Weapons;
using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class BombSpawnerComponent : MonoBehaviour
    {
        [SerializeField] private BombProjectileSpawner[] _prefabs;

        private bool _isSpawn = false;

        public void SetSpawn()
        {
            _isSpawn = !_isSpawn;
        }

        public void SpawnBombs()
        {
            if (_isSpawn == false) return;
             foreach(var prefab in _prefabs)
            {
                prefab.Spawn();
            }
        }
    }
}