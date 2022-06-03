using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Components.Health;
using PixelCrew.UI.Widgets;
using UnityEngine;

namespace PixelCrew.UI.Hud
{
    public class HealthBarMob : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;

        private int _maxHealth;

        public void OnMaxHp(int health)
        {
            _maxHealth = health;
        }
        public void OnHealthChanged(int health)
        {
            var value = (float) health / _maxHealth;
            _healthBar.SetProgress(value);

        }
    }
}
