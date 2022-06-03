using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class ProbabilityDropComponent : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField] private DropDate[] _drop;
        [SerializeField] private DropEvent _onDropCalculete;
        [SerializeField] private bool _spawnOnEnabled;

        public void SetCount(int count)
        {
            _count = count;
        }

        private void OnEnable()
        {
            if (_spawnOnEnabled)
            {
                CalculateDrop();
            }
        }

        public void CalculateDrop()
        {
            var itemsDrop = new GameObject[_count];
            var itemCount = 0;
            var total = _drop.Sum(dropDate => dropDate.ProbilityDrop);
            var sortedDrop = _drop.OrderBy(dropDate => dropDate.ProbilityDrop);

            while (itemCount < _count)
            {
                var random = UnityEngine.Random.value * total;
                var current = 0f;
                foreach (var dropDate in sortedDrop)
                {
                    current += dropDate.ProbilityDrop;
                    if (current >= random)
                    {
                        itemsDrop[itemCount] = dropDate.Drop;
                        itemCount++;
                        break;
                    }
                }
            }

            _onDropCalculete?.Invoke(itemsDrop);
        }

        [Serializable]
        public class DropDate
        {
            public GameObject Drop;
            [Range(0f, 100f)] public float ProbilityDrop;
        }


    }

    [Serializable]
    public class DropEvent : UnityEvent<GameObject[]>
    {

    }
}