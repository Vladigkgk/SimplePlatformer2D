using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Model.Data.Definitions.Repositories;
using UnityEngine;

namespace PixelCrew.Model.Data.Definitions.Player
{
    [Serializable]
    public struct StatDef
    {
        [SerializeField] private string _name;
        [SerializeField] private StatId _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private StatLevelDef[] _levels;

        public StatId Id => _id;
        public string Name => _name;
        public Sprite Icon => _icon;
        public StatLevelDef[] Levels => _levels;

    }

    [Serializable]
    public struct StatLevelDef
    {
        [SerializeField] private float value;
        [SerializeField] ItemWithCount _price;

        public float Value => value;
        public ItemWithCount Price => _price;
    }

    [Serializable] 
    public enum StatId
    {
        Health,
        Speed,
        RangeDamage,
        CriticalDamage
    }
}
