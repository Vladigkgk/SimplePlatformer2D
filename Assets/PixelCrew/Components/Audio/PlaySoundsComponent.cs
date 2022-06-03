using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Components.Audio
{
    public class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioDate[] _sounds;

        private AudioSource _source;

        private void Start()
        {
            _source = AudioUtils.FindAudioSource();
        }

        public void Play(string id)
        {
            if (_source != null)
            {
                foreach (var sound in _sounds)
                {
                    if (sound.Id != id) continue;
                    _source.PlayOneShot(sound.Clip);
                    break;
                }
            }
        }



        [Serializable]
        public class AudioDate
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;
            public AudioClip Clip => _clip;
        }
    }
}
