using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using PixelCrew.Model.Date;
using PixelCrew.Model.Date.Properties;
using PixelCrew.Utils.Disposables;

namespace PixelCrew.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingsComponent : MonoBehaviour
    {
        [SerializeField] private SoundSettings _mode;

        private AudioSource _audioSource;
        private FloatPersistentProperty _model;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            _model = FindProperty();
            _model.OnChanged += OnSoundsSettingsChanged;
            OnSoundsSettingsChanged(_model.Value, _model.Value);
        }

        private void OnSoundsSettingsChanged(float newValue, float oldValue)
        {
            _audioSource.volume = newValue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (_mode)
            {
                case SoundSettings.Music: return GameSettings.I.Music;
                case SoundSettings.SFX: return GameSettings.I.Sfx;
            }
            throw new ArgumentException("Undefined mode");
        }

        private void OnDestroy()
        {
            _model.OnChanged -= OnSoundsSettingsChanged;
        }

    }
}
