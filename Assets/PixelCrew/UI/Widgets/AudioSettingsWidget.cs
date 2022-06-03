using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using PixelCrew.Model.Date.Properties;
using PixelCrew.Utils.Disposables;

namespace PixelCrew.UI.Widgets
{

    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _value;

        private FloatPersistentProperty _model;

        private CompositDisposables _trash = new CompositDisposables();

        private void Start()
        {
            _trash.Retain(_slider.onValueChanged.Subacribe(OnSliderValueChanged));
        }

        private void OnSliderValueChanged(float value)
        {
            _model.Value = value;
        }

        public void SetModel(FloatPersistentProperty model)
        {
            _model = model;
            _trash.Retain(model.Subscribe(OnValueChanged));
            OnValueChanged(model.Value, model.Value);
        }

        private void OnValueChanged(float newValue, float OldValue)
        {
            var textValue = Mathf.Round(newValue * 100);
            _value.text = textValue.ToString();
            _slider.normalizedValue = newValue;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
            _model.OnChanged -= OnValueChanged;
        }


    }
}
