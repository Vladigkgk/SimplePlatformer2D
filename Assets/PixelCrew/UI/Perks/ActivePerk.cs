using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Model.Definitions;
using PixelCrew.Model;
using PixelCrew.Utils;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;
using PixelCrew.Utils.Disposables;

namespace PixelCrew.UI.Perks
{
    public class ActivePerk : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _lockPerk;

        private GameSession _session;

        private CompositDisposables _trash = new CompositDisposables();

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.PerkModel.Subscribe(SetActivePerk));

            SetActivePerk();
        }

        private void SetActivePerk()
        {
            var usedPerkId = _session.PerkModel.Used;
            var hasId = !string.IsNullOrEmpty(usedPerkId);
            if (hasId)
            {
                var def = DefsFacade.I.Perks.Get(usedPerkId);
                _icon.sprite = def.Icon;
                _icon.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            var cooldawn = _session.PerkModel.cooldown;
            _lockPerk.fillAmount = cooldawn.RemingTime / cooldawn.Value;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
