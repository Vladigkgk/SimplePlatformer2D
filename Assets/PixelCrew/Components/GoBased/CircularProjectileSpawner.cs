using System;
using System.Collections;
using PixelCrew.Utils;
using PixelCrew.Creatures.Weapons;
using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class CircularProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private CircularProjectileSetting[] _settings;
        public int Stage { get; set; }

        [ContextMenu("Launch!")]
        public void LaunchProjectile()
        {
            StartCoroutine(SpawnProjectile());
        }
        private IEnumerator SpawnProjectile()
        {
            var setting = _settings[Stage];
            var count = setting.BurstCount / setting.ItemPerBurst;
            var sectorStep = 2 * Mathf.PI / setting.ItemPerBurst;

            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < setting.ItemPerBurst; i++)
                {
                    var angle = sectorStep * i;
                    var diretion = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                    var instanse = SpawnUtils.Spawn(setting.Projectile.gameObject, transform.position);
                    var projectile = instanse.GetComponent<DirectionProjectile>();
                    projectile.Launch(diretion);

                    yield return new WaitForSeconds(setting.Delay);
                }

                yield return new WaitForSeconds(setting.DelayPerShoot);
            }
        }

        [Serializable]
        public struct CircularProjectileSetting
        {
            [SerializeField] private DirectionProjectile _projectile;
            [SerializeField] private int _burstCount;
            [SerializeField] private int _itemPerBurst;
            [SerializeField] private float _delay;
            [SerializeField] private float _delayPerShoot;

            public DirectionProjectile Projectile => _projectile;
            public int BurstCount => _burstCount;
            public int ItemPerBurst => _itemPerBurst;
            public float Delay => _delay;
            public float DelayPerShoot => _delayPerShoot;
        }


    }
}