using System;
using System.Collections.Generic;
using _game.Storage;
using com.ruffgames.core.Core.Runtime.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace com.ruffgames.core.Runtime.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private Level _currentLevelObj;

        private int _currentLevel
        {
            get => _storage.StorageData.GameLevelData.CurrentLevel;
            set => _storage.StorageData.GameLevelData.CurrentLevel = value;
        }
        private int _currentLevelIndex
        {
            get => _storage.StorageData.GameLevelData.CurrentLevelIndex;
            set => _storage.StorageData.GameLevelData.CurrentLevelIndex = value;
        }

        [SerializeField] private List<ParticleSystem> _confetties;
        [Inject] private IStorage _storage;
        [Inject] private CurrencyManager _currencyManager;
        [Inject] private Factories.LevelFactory _levelFactory;
        [Inject] private ProjectReferenceData _projectReferenceData;
        [Inject] private EventManager _eventManager;

        private void Start()
        {
           SpawnMap();
        }

        private void SpawnMap()
        {
            DestroyMap();
            var level = _projectReferenceData.LevelConfig.Levels[_currentLevelIndex];
            _currentLevelObj = _levelFactory.Create(level);
            _currentLevelObj.transform.SetParent(transform);
            
            _eventManager.TriggerGameStateChange(GameState.Play);
            _eventManager.TriggerLevelSpawn(_currentLevel);
        }
        [PropertySpace]
        [BoxGroup("Target Level", ShowLabel = false)]
        [Button, GUIColor(0.2f, 1f, 0)]
        public void TargetLevel(int level)
        {
            _currentLevel = level-1;
            NextLevel();
        }
        
        [PropertySpace]
        [Button(ButtonSizes.Large), GUIColor(0.2f, 1f, 0)]
        public void NextLevel()
        {
            _currentLevel++;
            _currencyManager.CollectCurrency();

            var levelCount = _projectReferenceData.LevelConfig.Levels.Count;

            if (_currentLevel >= levelCount)
            {
                if (_currentLevelIndex + 1 >= levelCount)
                {
                    _currentLevelIndex = _projectReferenceData.LevelConfig.RestartLevelIndex;
                }
                else
                {
                    _currentLevelIndex++;
                }
            }
            else
            {
                _currentLevelIndex = _currentLevel;
            }
            _storage.Save();
            SpawnMap();
        }
        

        private void DestroyMap()
        {
            if (_currentLevelObj is null) return;
            
            Destroy(_currentLevelObj.gameObject);
        }
        
        public void RestartLevel()
        {
            SpawnMap();
        }

        public int GetCurrentLevel()
        {
            return _currentLevel + 1;
        }

        public void PlayConfetties()
        {
            foreach (var confetty in _confetties)
            {
                confetty.Play();
            }
        }
    }
}
