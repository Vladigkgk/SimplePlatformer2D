using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud
{
    public class PersonalizedDialogBoxController : DialogBoxController
    {
        [SerializeField] private DialogContent _right;

        protected override DialogContent CurrentContent => CurrentSentence.Side == Model.Data.Side.Left ? _content : _right;

        protected override void OnStartAnimationDialog()
        {
            _content.gameObject.SetActive(CurrentSentence.Side == Model.Data.Side.Left);
            _right.gameObject.SetActive(CurrentSentence.Side == Model.Data.Side.Right);
            base.OnStartAnimationDialog();
        }
    }

}