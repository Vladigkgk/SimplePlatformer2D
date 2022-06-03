using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Creatures;

namespace PixelCrew.Creatures.Mobs.Patrolling
{
    public class PatrolPlatform : Patrol
    {
        [SerializeField] private LayerCheck _layer;

        private Creature _creator;

        private Vector2 _direction = new Vector2(1f, 0f);

        private void Awake()
        {
            _creator = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (_layer.IsTouchingLayer == false)
                {
                    _direction.x *= -1;
                }
                _creator.SetDirection(_direction);

                yield return null;
            }
        }
    }
}
