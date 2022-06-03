using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Components.Audio
{
    public class PlaySfxSounds : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;

        private AudioSource _source;

        public void Play()
        {
            _source = AudioUtils.FindAudioSource();
            if (_source == null) return;
            _source.PlayOneShot(_clip);
        }
    }
}
