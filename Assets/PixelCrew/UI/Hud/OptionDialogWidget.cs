using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PixelCrew.UI.Hud
{
    public class OptionDialogWidget : MonoBehaviour, IItemRenderer<OptionData>
    {
        [SerializeField] private Text _label;
        [SerializeField] private SelectOption _onSelect;

        private OptionData _data;

        public void SetData(OptionData data, int index)
        {
            _data = data;
            _label.text = data.Text;
        }

        public void OnSelect()
        {
            _onSelect?.Invoke(_data);
        }

        [Serializable]
        public class SelectOption : UnityEvent<OptionData>
        {

        }
    }
}
