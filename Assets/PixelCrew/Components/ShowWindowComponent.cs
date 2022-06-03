using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PixelCrew.Components
{
    public class ShowWindowComponent : MonoBehaviour
    {
        [SerializeField] private string _path;

        public void Show()
        {
            var window = Resources.Load<GameObject>(_path);
            var canvas = GameObject.FindGameObjectWithTag("GameMenuCanvas");
            Instantiate(window, canvas.transform);
        }
    }
}
