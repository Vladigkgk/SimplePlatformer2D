using System.Collections;
using PixelCrew.Components.Health;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.UI.Widgets
{
    public class BossHpWidget : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private ProgressBarWidget _hpBar;
        [SerializeField] private CanvasGroup _canvas;
        private float _maxHealth;

        private void Start()
        {
            _maxHealth = _health.Health;
        }

        [ContextMenu("Show")]
        public void ShowUI()
        {
            this.LerpAnimated(0, 1, 1, SetAlpha);
        }

        [ContextMenu("Hide")]
        public void HideUI()
        {
            this.LerpAnimated(1, 0, 1, SetAlpha);
        }

        private void SetAlpha(float alpha)
        {
            _canvas.alpha = alpha;
        }

        public void OnHpChanged(int health)
        {
            _hpBar.SetProgress(health / _maxHealth);
        }

    }
}