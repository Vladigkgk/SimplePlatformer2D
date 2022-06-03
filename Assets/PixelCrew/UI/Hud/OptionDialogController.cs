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
    public class OptionDialogController : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private Text _contentText;
        [SerializeField] private Transform _container;
        [SerializeField] private OptionDialogWidget _prefab;

        private DataGroup<OptionData, OptionDialogWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionDialogWidget>(_prefab, _container);
        }

        public void OnOptionsSelected(OptionData selectedOption)
        {
            selectedOption.OnSelect?.Invoke();
            _content.SetActive(false);
        }

        public void Show(OptionDialogData data)
        {
            _content.SetActive(true);

            _contentText.text = data.DialogText;

            _dataGroup.SetData(data.Options);
        }

    }

    [Serializable]
    public class OptionDialogData
    {
        public string DialogText;
        public OptionData[] Options;
    }

    [Serializable]
    public class OptionData
    {
        public string Text;
        public UnityEvent OnSelect;
    }

}
