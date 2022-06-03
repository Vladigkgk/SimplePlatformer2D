using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs.Boss
{
    public class FloodControlller : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _floodingTime;

        private readonly static int IsFlooding = Animator.StringToHash("isFlood");

        private Coroutine _corountine;

        public void StartFlooding()
        {
            if (_corountine != null) return;
            _corountine = StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            _animator.SetBool(IsFlooding, true);
            yield return new WaitForSeconds(_floodingTime);
            _animator.SetBool(IsFlooding, false);
            _corountine = null;

        }
    }
}