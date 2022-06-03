using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Utils.Disposables;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Model.Models
{
    public class PerkModel : IDisposable
    {
        private readonly PlayerData _data;

        public readonly StringProperty InterfaceSelection = new StringProperty();
        public readonly Cooldown cooldown = new Cooldown();
        private readonly CompositDisposables _trash = new CompositDisposables();

        private event Action OnChanged;


        public string Used => _data.Perks.Used.Value;
        public bool IsSuperThrowSupported => _data.Perks.Used.Value == "Sword-Super-Throw" && cooldown.IsReady;
        public bool IsFlyingSupported => _data.Perks.Used.Value == "Flying" && cooldown.IsReady;

        public bool IsShieldSupported => _data.Perks.Used.Value == "Shield" && cooldown.IsReady;

        public PerkModel(PlayerData data)
        {
            _data = data;
            InterfaceSelection.Value = DefsFacade.I.Perks.All[0].Id;

            _trash.Retain(_data.Perks.Used.Subscribe((x, y) => OnChanged?.Invoke()));
            _trash.Retain(InterfaceSelection.Subscribe( (x, y) => OnChanged?.Invoke() ));

            if (!string.IsNullOrEmpty(Used))
            {
                var defPerk = DefsFacade.I.Perks.Get(Used);
                cooldown.Value = defPerk.Cooldawn;
            }
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposables(() => OnChanged -= call);
        }

        public void Unlock(string id)
        {
            var def = DefsFacade.I.Perks.Get(id);
            var isEnoughResoureses = _data.Inventory.IsEnough(def.Price);
            if (isEnoughResoureses)
            {
                _data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
                _data.Perks.AddPerk(id);
                OnChanged?.Invoke();
            }
        }

        public void SelectPerk(string selected)
        {
            var defPerk = DefsFacade.I.Perks.Get(selected);
            cooldown.Value = defPerk.Cooldawn;
            _data.Perks.Used.Value = selected;
        }



        public bool IsUsed(string perkId)
        {
            return _data.Perks.Used.Value == perkId;
        }

        public bool IsUnlock(string perkId)
        {
            return _data.Perks.IsUnlocked(perkId);
        }
        public bool CanBuy(string perkId)
        {
            var def = DefsFacade.I.Perks.Get(perkId);
            return _data.Inventory.IsEnough(def.Price);
        }

        public void Dispose()
        {
            _trash.Dispose();
        }

    }
}
