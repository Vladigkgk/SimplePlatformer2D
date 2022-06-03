using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repositories;
using PixelCrew.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace PixelCrew.Model.Data.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Defs/Repository/Perks", fileName = "Perks")]

    public class PerkRepository : DefRepository<PerkDef>
    {
    }

    [Serializable]
    public struct PerkDef : IHaveId
    {
        [SerializeField] string _id;
        [SerializeField] Sprite _icon;
        [SerializeField] string _info;
        [SerializeField] ItemWithCount _price;
        [SerializeField] float _coolDawn;

        public string Id => _id;

        public Sprite Icon => _icon;

        public string Info => _info;

        public ItemWithCount Price => _price;

        public float Cooldawn => _coolDawn;
    }

    [Serializable]
    public struct ItemWithCount
    {
        [InventoryId] [SerializeField] private string _itemId;
        [SerializeField] private int _count;

        public string ItemId => _itemId;

        public int Count => _count;
    }

}
