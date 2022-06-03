using System.Collections;
using System.Globalization;
using PixelCrew.Model;
using PixelCrew.Model.Data.Definitions.Player;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Stats
{
    public class StatsWidget : MonoBehaviour, IItemRenderer<StatDef>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _name;
        [SerializeField] private Text _currentValue;
        [SerializeField] private Text _increaseValue;
        [SerializeField] private ProgressBarWidget _progressBar;
        [SerializeField] private GameObject _selector;

        private GameSession _session;

        private StatDef _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            UpdateView();

        }

        public void SetData(StatDef data, int index)
        {
            _data = data;

            if (_session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            var StatsModel = _session.StatsModel;

            _icon.sprite = _data.Icon;
            _name.text = LocalizationManager.I.Localize(_data.Name);
            var currentValue = StatsModel.GetValue(_data.Id);
            _currentValue.text = currentValue.ToString(CultureInfo.InvariantCulture);

            var currentLevel = StatsModel.GetLevel(_data.Id);
            var nextLevel = currentLevel + 1;
            var nextLevelValue = StatsModel.GetValue(_data.Id, nextLevel);
            var increaseValue = nextLevelValue - currentValue;
            _increaseValue.text = $"+ {increaseValue}";
            _increaseValue.gameObject.SetActive(increaseValue > 0);

            var maxLevel = DefsFacade.I.Player.GetStat(_data.Id).Levels.Length - 1;
            _progressBar.SetProgress((float) currentLevel / maxLevel);

            _selector.SetActive(StatsModel.InterfaceSelectedStat.Value == _data.Id);
        }

        public void OnSelected()
        {
            _session.StatsModel.InterfaceSelectedStat.Value = _data.Id;
        }

    }
}