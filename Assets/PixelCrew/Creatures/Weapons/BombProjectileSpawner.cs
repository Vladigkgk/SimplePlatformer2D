using System.Collections;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class BombProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;

        public void Spawn()
        {
            SpawnUtils.Spawn(_prefab, transform.position);
        }
    }
}