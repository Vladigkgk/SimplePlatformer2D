using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.UI.Widgets;
using PixelCrew.Model.Definitions.Localization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PixelCrew.UI.Localization
{

    public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
    {
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _selector;
        [SerializeField] private SelectLocale _onSelected;

        private LocaleInfo _data;

        private void Start()
        {
            LocalizationManager.I.OnLocaleChanged += UpdateSelection;
        }

        private void UpdateSelection()
        {
            var isSelected = LocalizationManager.I.LocaleKey == _data.LocalId;
            _selector.SetActive(isSelected);
        }

        public void SetData(LocaleInfo localeInfo, int index)
        {
            _data = localeInfo;
            UpdateSelection();
            _text.text = localeInfo.LocalId.ToUpper();
        }

        public void OnSelected()
        {
            _onSelected?.Invoke(_data.LocalId);
        }

        private void OnDestroy()
        {
            LocalizationManager.I.OnLocaleChanged -= UpdateSelection;
        }


    }

    [Serializable]
    public class SelectLocale : UnityEvent<string>
    {

    }
        

    public class LocaleInfo
    {
        public string LocalId;
    }
    
}
