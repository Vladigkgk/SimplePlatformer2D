using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PixelCrew.Utils
{
    public class AudioUtils
    {
        private const string SfxAudioSource = "AudioSFX";
        public static AudioSource FindAudioSource()
        {
            return GameObject.FindWithTag(SfxAudioSource).GetComponent<AudioSource>();
        }
    }
}
