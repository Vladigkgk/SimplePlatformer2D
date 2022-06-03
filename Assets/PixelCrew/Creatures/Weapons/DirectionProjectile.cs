using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class DirectionProjectile : BaseProjectile
    {
        public void Launch(Vector2 direction)
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.AddForce(direction, ForceMode2D.Impulse);
        }
    }
}