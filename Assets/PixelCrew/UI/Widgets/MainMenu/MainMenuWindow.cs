using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using PixelCrew.UI;
using PixelCrew.UI.LevelsLoader;

namespace PixelCrew.UI.Widgets.MainMenu
{
    public class MainMenuWindow : AnimatedWindow
    {
        protected Action CloseAction;

        public void OnStartGame()
        {
            CloseAction = () => 
            {
                var levelLoader = FindObjectOfType<LevelLoader>();
                levelLoader.LoadLevel("Level1");
            };
            Close();
        }

        public void OnOptions()
        {
            var window = Resources.Load<GameObject>("UI/SettingsMenuWindow");
            var canvas = GameObject.FindGameObjectWithTag("GameMenuCanvas");
            Instantiate(window, canvas.transform);

        }

        public void OnLanguagesOptions()
        {
            var window = Resources.Load<GameObject>("UI/LanguagesMenuWindow");
            var canvas = GameObject.FindGameObjectWithTag("GameMenuCanvas");
            Instantiate(window, canvas.transform);
        }

        public void OnExit()
        {
            CloseAction = () =>
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; 
#endif
            };
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            CloseAction?.Invoke();
        }
    }
}
