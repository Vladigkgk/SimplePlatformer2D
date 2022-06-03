using System;
using System.Collections;
using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Definitions.Player;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Model.Definitions;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.Model.Models
{
    public class StatsModel : IDisposable
    {
        private readonly PlayerData _data;

        public event Action OnChanged;
        public event Action<StatId> OnUpgraded;

        public ObservableProperty<StatId> InterfaceSelectedStat = new ObservableProperty<StatId>();
        private readonly CompositDisposables _trash = new CompositDisposables();


        public StatsModel(PlayerData data)
        {
            _data = data;
            _trash.Retain(InterfaceSelectedStat.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposables(() => OnChanged -= call);
        }

        public void LevelUp(StatId id)
        {
            var def = DefsFacade.I.Player.GetStat(id);
            var nextLevel = GetLevel(id) + 1;

            if (def.Levels.Length <= nextLevel) return;

            var price = def.Levels[nextLevel].Price;
            if (!_data.Inventory.IsEnough(price)) return;

            _data.Inventory.Remove(price.ItemId, price.Count);
            _data.Levels.LevelUp(id);

            OnChanged?.Invoke();
            OnUpgraded?.Invoke(id);

        }


        public float GetValue(StatId id, int level = -1)
        {
            return GetCurrentLevelDef(id, level).Value;
        }

        public StatLevelDef GetCurrentLevelDef(StatId id, int level = -1 )
        {
            if (level == -1) level = GetLevel(id);
            var def = DefsFacade.I.Player.GetStat(id);
            if (def.Levels.Length > level)
                return def.Levels[level];

            return default;
        }

        public int GetLevel(StatId id) => _data.Levels.GetLevel(id);

        public bool CanBuy(StatId statId)
        {

            //var def = DefsFacade.I.Perks.Get(perkId);
            //return _data.Inventory.IsEnough(def.Price);
            var def = DefsFacade.I.Player.GetStat(statId);
            var nextLevel = GetLevel(statId) + 1;

            if (def.Levels.Length <= nextLevel) return false;

            var price = def.Levels[nextLevel].Price;
            if (!_data.Inventory.IsEnough(price)) return false;
            return true;
        }

        public void Dispose()
        {
            _trash.Dispose();
        }
    }
}