using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Hud;
using UnityEngine;
using UnityEngine.Events;
namespace PixelCrew.Components.Dialog
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private Mode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] private DialogDef _external;
        [SerializeField] private UnityEvent _onComplete;

        private DialogBoxController _dialogBox;

        public void Show()
        {
            if (_dialogBox == null)
            {
                _dialogBox = FindDialogController();

            }

            _dialogBox.ShowDialogBox(Data, _onComplete);
        }

        private DialogBoxController FindDialogController()
        {
            if (_dialogBox != null) return _dialogBox;

            GameObject ControllerGo;

            switch (Data.Type)
            {
                case DialogType.Simple:
                        ControllerGo = GameObject.FindWithTag("SimpleDialog");
                        break;
                case DialogType.Personalized:
                    ControllerGo = GameObject.FindWithTag("PersonalizedDialog");
                    break;
                default:
                    throw new ArgumentException("Undefined dialog type");
            }


            _dialogBox = ControllerGo.GetComponent<DialogBoxController>();

            return _dialogBox;
        }

        public void Show(DialogDef def)
        {
            _external = def;
            Show();
        }

        public DialogData Data
        {
            get
            {
                switch (_mode)
                {
                    case Mode.Bound:
                        return _bound;
                        break;
                    case Mode.External:
                        return _external.Data;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public enum Mode
        {
            Bound,
            External
        }
    }
}
