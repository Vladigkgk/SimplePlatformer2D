﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Components.GoBased;
using PixelCrew.Model;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.LevelManegement
{
    [RequireComponent(typeof(SpawnComponent))]
    public class CheckPointComponent : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private SpawnComponent _heroSpawn;
        [SerializeField] private UnityEvent _setChecked;
        [SerializeField] private UnityEvent _setUnchecked;

        public string Id => _id;

        private GameSession _session;
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            if (_session.IsChecked(_id))
            {
                _setChecked?.Invoke();
            }
            else
            {
                _setUnchecked?.Invoke();
            }
        }

        public void Check()
        {
            _session.SetCheck(_id);
            _setChecked?.Invoke();
        }

        public void SpawnHero()
        {
            _heroSpawn.Spawn();
        }
    }
}
