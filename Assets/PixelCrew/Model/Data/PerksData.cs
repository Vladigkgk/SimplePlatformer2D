using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class PerksData
    {
        [SerializeField] private StringProperty _used = new StringProperty();
        [SerializeField] private List<string> _unLocked = new List<string>();

        public StringProperty Used => _used;

        public void AddPerk(string id)
        {
            if (!_unLocked.Contains(id))
                _unLocked.Add(id);
        }

        public bool IsUnlocked(string id)
        {
            return !_unLocked.Contains(id);
        }
    }
}
