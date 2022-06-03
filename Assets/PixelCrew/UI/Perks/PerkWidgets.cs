using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Definitions.Repositories;
using PixelCrew.UI.Widgets;
using PixelCrew.Model;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Perks
{
    public class PerkWidgets : MonoBehaviour, IItemRenderer<PerkDef>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _isLocked;
        [SerializeField] private GameObject _isUsed;
        [SerializeField] private GameObject _isSelected;

        private GameSession _session;
        private PerkDef _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(PerkDef data, int index)
        {
            _data = data;

            if (_session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            _icon.sprite = _data.Icon;
            _isUsed.SetActive(_session.PerkModel.IsUsed(_data.Id));
            _isSelected.SetActive(_session.PerkModel.InterfaceSelection.Value == _data.Id);
            _isLocked.SetActive(_session.PerkModel.IsUnlock(_data.Id));
        }

        public void IsSelected()
        {
            _session.PerkModel.InterfaceSelection.Value = _data.Id;
        }
    }
}
