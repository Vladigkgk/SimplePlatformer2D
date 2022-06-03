using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PixelCrew.Effect
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float _valueEffect;
        [SerializeField] private Transform _followTarget;

        private float _startX;

        private void Start()
        {
            _startX = transform.position.x;    
        }

        private void LateUpdate()
        {
            var currentPosition = transform.position;
            var deltaX = _followTarget.position.x * _valueEffect;
            transform.position = new Vector3(_startX + deltaX, currentPosition.y, currentPosition.y);
        }
    }
}
