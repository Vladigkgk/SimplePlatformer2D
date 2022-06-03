using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixelCrew.Model.Data.Properties;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Model.Date.Properties
{
    public abstract class PersistentProperty<TPropertyType> : ObservableProperty<TPropertyType>
    {
        protected TPropertyType _stored;

        private TPropertyType _defaultValue;

        public PersistentProperty(TPropertyType defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public override TPropertyType Value
        {
            get => _stored;

            set
            {
                var isEquals = _stored.Equals(value);
                if (isEquals) return;

                var oldValue = _value;
                Write(value);
                _stored = _value = value;

                InvokeChangedEvent(value, oldValue);
            }
        }
        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType defaultValue);


        protected void Init()
        {
            _stored = _value = Read(_defaultValue);
        }
        public void Validate()
        {
            if (!_stored.Equals(_value))
            {
                Value = _value;
            }
        }
    }
}
