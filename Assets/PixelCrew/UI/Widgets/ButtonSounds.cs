using System.Collections;
using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.EventSystems;


namespace PixelCrew.PixelCrew.UI.Widgets
{
    public class ButtonSounds : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AudioClip _audioClip;

        private AudioSource source;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            source = AudioUtils.FindAudioSource();
            if (source == null) return;
            source.PlayOneShot(_audioClip);
        }
    }
}	
