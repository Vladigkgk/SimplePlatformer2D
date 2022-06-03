using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

namespace PixelCrew.Effect.CameraRelate
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraShakeEffect : MonoBehaviour
    {
        [SerializeField] private float _animatedTime = 0.3f;
        [SerializeField] private float _instensty = 3f;

        private CinemachineBasicMultiChannelPerlin _cameraNoise;

        private Coroutine _coroutine;

        private void Awake()
        {
            var virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void Shack()
        {
            if (_coroutine != null)
                StopAnimation();
            _coroutine = StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation()
        {
            _cameraNoise.m_FrequencyGain = _instensty;
            yield return new WaitForSeconds(_animatedTime);
            StopAnimation();
        }

        private void StopAnimation()
        {
            _cameraNoise.m_FrequencyGain = 0f;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}