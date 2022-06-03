using System;
using System.Collections;
using PixelCrew.Model;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.Effect.CameraRelate
{
    [RequireComponent(typeof(Animator))]
    public class BloodSplashOverlay : MonoBehaviour
    {
        [SerializeField] private Transform _overlay;

        private GameSession _session;
        private Vector3 _overScale;
        private Animator _animator;

        private readonly CompositDisposables _trash = new CompositDisposables();
        private static readonly int Health = Animator.StringToHash("Health");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _overScale = _overlay.localScale - Vector3.one;

            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.Data.Hp.SubscribeAndInvoke(OnHealthChanged));
        }

        private void OnHealthChanged(int newValue, int _)
        {
            var maxHp = _session.StatsModel.GetValue(Model.Data.Definitions.Player.StatId.Health);
            var hpNormalized = newValue / maxHp;
            _animator.SetFloat(Health, hpNormalized);

            var modifyScale = Mathf.Max(hpNormalized - 0.3f, 0);
            _overlay.localScale = Vector3.one + _overScale * modifyScale;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}