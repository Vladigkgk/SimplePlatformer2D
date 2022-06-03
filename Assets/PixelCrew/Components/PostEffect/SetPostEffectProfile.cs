using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace PixelCrew.Components.PostEffect
{
    public class SetPostEffectProfile : MonoBehaviour
    {
        [SerializeField] private VolumeProfile _profile;
        public void Set()
        {
            var volumes = FindObjectsOfType<Volume>();
            foreach (var valume in volumes)
            {
                if (!valume.isGlobal) continue;

                valume.profile = _profile;
                break;
            }
        }
    }
}