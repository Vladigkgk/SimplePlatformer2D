using System.Collections;
using PixelCrew.Components.Health;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs.Boss
{
    public class HealthAnimationGlue : MonoBehaviour
    {
        [SerializeField] private HealthComponent _hp;
        [SerializeField] private Animator _animator;
        private readonly static int Health = Animator.StringToHash("Health");

        private void Awake()
        {
            _animator.SetInteger(Health, _hp.Health);
        }

        public void OnHealthChanged(int health)
        {
            _animator.SetInteger(Health, health);
        }
    }
}