using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrew.UI.Widgets.MainMenu;
using PixelCrew.Model;

namespace PixelCrew.UI.Widgets.StopMenu
{
    public class GameMenuWindow : MainMenuWindow
    {
        private float _defaultTimeScale;

        protected override void Start()
        {
            base.Start();
            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;

        }

        public void Continue()
        {
            Close();
        }

        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
        }


    }
}
