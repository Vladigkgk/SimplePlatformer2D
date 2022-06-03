using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils;
using PixelCrew.Utils.Disposables;
using PixelCrew.Model;
using PixelCrew.Model.Date.Properties;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Data.Definitions.Repositories;

using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Perks
{
    public class ManagePerksWindow : AnimatedWindow
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _useButton;
        [SerializeField] private ItemWidget _price;
        [SerializeField] private Text _info;
        [SerializeField] private Transform _perksContainer;

        private PredefinedDataGroup<PerkDef, PerkWidgets> _dataGroup;
        private CompositDisposables _trash = new CompositDisposables();

        private GameSession _session;

        protected override void Start()
        {
            base.Start();

            _dataGroup = new PredefinedDataGroup<PerkDef, PerkWidgets>(_perksContainer);
            _session = FindObjectOfType<GameSession>();

            _trash.Retain(_session.PerkModel.Subscribe(PerksOnChanged));
            _trash.Retain(_buyButton.onClick.Subacribe(OnBuy));
            _trash.Retain(_useButton.onClick.Subacribe(OnUse));

            PerksOnChanged();
        }

        private void PerksOnChanged()
        {
            _dataGroup.SetData(DefsFacade.I.Perks.All);

            var selected = _session.PerkModel.InterfaceSelection.Value;

            _useButton.gameObject.SetActive(!_session.PerkModel.IsUnlock(selected));
            _useButton.interactable = _session.PerkModel.Used != selected;

            _buyButton.gameObject.SetActive(_session.PerkModel.IsUnlock(selected));
            _buyButton.interactable = _session.PerkModel.CanBuy(selected);

            var def = DefsFacade.I.Perks.Get(selected);
            _price.SetData(def.Price);

            _info.text = LocalizationManager.I.Localize(def.Info);
        }

        private void OnBuy()
        {
            var selected = _session.PerkModel.InterfaceSelection.Value;
            _session.PerkModel.Unlock(selected);
        }

        private void OnUse()
        {
            var selected = _session.PerkModel.InterfaceSelection.Value;
            _session.PerkModel.SelectPerk(selected);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}