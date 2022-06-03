using System.Collections;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.Components.Health
{
    [RequireComponent(typeof(HealthComponent))]
    public class ImmuneAfterHit : MonoBehaviour
    {
        [SerializeField] private float _immuneTime;
        private HealthComponent _health;
        private CompositDisposables _trash = new CompositDisposables();
        private Coroutine _coroutine;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _health._onDamage.AddListener(OnDamage);
        }

        private void OnDamage()
        {
            TryStop();
            if (_immuneTime > 0)
                _coroutine = StartCoroutine(MakeImmun());
        }

        private void TryStop()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator MakeImmun()
        {
            _health.Immun.Retain(this);
            yield return new WaitForSeconds(_immuneTime);
            _health.Immun.Release(this);
        }

        private void OnDestroy()
        {
            TryStop();
            _health._onDamage.RemoveListener(OnDamage);
        }
    }
}