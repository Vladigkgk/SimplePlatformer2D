using System;
using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            InstanceSpawn();
        }

        public GameObject InstanceSpawn()
        {
            var instance = Instantiate(_prefab, _target.position, Quaternion.identity);

            var scale = _target.lossyScale;
            instance.transform.localScale = scale;
            instance.SetActive(true);

            return instance;
        }

        public void SetPrefab(GameObject projectTile)
        {
            _prefab = projectTile;
        }
    }
}