﻿using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace PixelCrew.Creatures.Mobs.Boss
{
    public class ModifyLights : MonoBehaviour
    {
        [SerializeField] private Light2D[] _lights;

        [ColorUsage(true, true)] [SerializeField]
        private Color _color;

        [ContextMenu("Setup")]
        public void SetColor()
        {
            foreach(var light in _lights)
            {
                light.color = _color;
            }
        }
    }
}