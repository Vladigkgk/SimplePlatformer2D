using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using PixelCrew.Model.Data;
using PixelCrew.Utils.Disposables;
using PixelCrew.Components.LevelManegement;
using PixelCrew.Model.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using PixelCrew.Model.Data.Definitions.Player;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        [SerializeField] private string _defaultChecked;
        public PlayerData Data => _data;
        private PlayerData _save;

        private readonly CompositDisposables _trash = new CompositDisposables();

        public QuickInventoryModel QuickInventory { get; private set; }

        public PerkModel PerkModel { get; private set; }
        public StatsModel StatsModel { get; private set; }

        private readonly List<string> _checkPoints = new List<string>();

        private void Awake()
        {

            var exitsSession = GetExitsSession();
            if (exitsSession != null)
            {
                exitsSession.StartSession(_defaultChecked);
                Destroy(gameObject);
            }
            else
            {
                InitModel();
                Save();
                DontDestroyOnLoad(this);
                StartSession(_defaultChecked);
            }
        }

        private void StartSession(string defaultCheked)
        {
            SetCheck(defaultCheked);

            LoadUIs();
            SpawnHero();
        }

        private void SpawnHero()
        {
            var checkpoints = FindObjectsOfType<CheckPointComponent>();
            var lastCheckPoint = _checkPoints.Last();
            foreach (var checkPoint in checkpoints)
            {
                if (checkPoint.Id == lastCheckPoint)
                {
                    checkPoint.SpawnHero();
                    break;
                }
            }
        }

        private void InitModel()
        {
            QuickInventory = new QuickInventoryModel(_data);
            _trash.Retain(QuickInventory);

            PerkModel = new PerkModel(_data);
            _trash.Retain(PerkModel);

            StatsModel = new StatsModel(_data);
            _trash.Retain(StatsModel);

            _data.Hp.Value = (int) StatsModel.GetValue(StatId.Health);

        }

        private void LoadUIs()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
            SceneManager.LoadScene("Controllers", LoadSceneMode.Additive);
        }

        private GameSession GetExitsSession()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                    return gameSession;
            }

            return null;
        }

        public void Save()
        {
            _save = _data.Clone();
        }

        public bool IsChecked(string id)
        {
            return _checkPoints.Contains(id);
        }

        public void SetCheck(string id)
        {
            if (!_checkPoints.Contains(id))
            {
                Save();
                _checkPoints.Add(id);
            }
                
        }

        public void LoadLastSave()
        {
            _data = _save.Clone();

            _trash.Dispose();
            InitModel();
        }

        private List<string> _removedItems = new List<string>();

        public void StoreState(string id)
        {
            if (!_removedItems.Contains(id))
                _removedItems.Add(id);
        }

        public bool RestoreState(string id)
        {
            return _removedItems.Contains(id);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}