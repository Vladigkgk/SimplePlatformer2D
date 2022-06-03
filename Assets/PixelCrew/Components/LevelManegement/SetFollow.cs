using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Creatures.Hero;
using UnityEngine;
using Cinemachine;

namespace PixelCrew.Components.LevelManegement
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public  class SetFollow : MonoBehaviour
    {
        private void Start()
        {
            var camera = GetComponent<CinemachineVirtualCamera>();
            var hero = FindObjectOfType<Hero>();
            camera.Follow = hero.transform;
        }
    }
}
