using System;
using System.Collections;
using PixelCrew.Model;
using PixelCrew.Model.Data.Definitions.Player;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Stats
{
    public class PlayerStasWindow : AnimatedWindow
    {
        [SerializeField] private Transform _statsContainer;
        [SerializeField] private StatsWidget _prefab;

        [SerializeField] private Button _upgradeButton;
        [SerializeField] private ItemWidget _price;

        private readonly CompositDisposables _trash = new CompositDisposables();

        private DataGroup<StatDef, StatsWidget> _dataGroup;

        private GameSession _session;

        protected override void Start()
        {
            base.Start();



            _dataGroup = new DataGroup<StatDef, StatsWidget>(_prefab, _statsContainer);

            _session = FindObjectOfType<GameSession>();
            _session.StatsModel.InterfaceSelectedStat.Value = DefsFacade.I.Player.Stats[0].Id;
            _trash.Retain(_session.StatsModel.Subscribe(OnStatsChanged));
            _trash.Retain(_upgradeButton.onClick.Subacribe(OnUpgrade));

            OnStatsChanged();
        }

        private void OnUpgrade()
        {
            var selected = _session.StatsModel.InterfaceSelectedStat.Value;
            _session.StatsModel.LevelUp(selected);
        }

        private void OnStatsChanged()
        {
            var stats = DefsFacade.I.Player.Stats;
            _dataGroup.SetData(stats);

            var selected = _session.StatsModel.InterfaceSelectedStat.Value;
            var nextLevel = _session.StatsModel.GetLevel(selected) + 1;
            var def = _session.StatsModel.GetCurrentLevelDef(selected, nextLevel);
            _price.SetData(def.Price);

            _price.gameObject.SetActive(def.Price.Count != 0);
            _upgradeButton.gameObject.SetActive(def.Price.Count != 0);
            _upgradeButton.interactable = _session.StatsModel.CanBuy(selected);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}