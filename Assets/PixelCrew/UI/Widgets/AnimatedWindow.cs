using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PixelCrew.UI.Widgets
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;

        private static readonly int ShowKey = Animator.StringToHash("show");
        private static readonly int HideKey = Animator.StringToHash("hide");



        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();

            _animator.SetTrigger(ShowKey);
        }

        public void Close()
        {
            _animator.SetTrigger(HideKey);
        }

        public virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }


    }
}
