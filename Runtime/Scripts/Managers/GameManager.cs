using com.ruffgames.core.Core.Runtime.Scripts;
using com.ruffgames.core.Runtime.Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace com.ruffgames.core.Runtime.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameState CurrentGameState => _currentGameState;
        [ShowInInspector, ReadOnly] private GameState _currentGameState;

        [Inject] private EventManager _eventManager;
        [Inject] private PanelController _panelController;
        [Inject] private LevelManager _levelManager;
        [Inject] private AudioManager _audioManager;
        [Inject] private FeelVibrationManager _feelVibrationManager;

        private void OnEnable()
        {
            _eventManager.OnGameStateChanged += OnGameStateChange;
        }

        private void OnDisable()
        {
            _eventManager.OnGameStateChanged -= OnGameStateChange;
        }

        private void OnGameStateChange(GameState gameState)
        {
            _currentGameState = gameState;

            switch (_currentGameState)
            {
                case GameState.Idle:
                    break;
                case GameState.Play:
                    _panelController.Show(PanelType.Game);
                    _audioManager.Play(AudioClipType.ButtonClick);
                    break;
                case GameState.Win:
                    _panelController.Show(PanelType.Win);
                    _levelManager.PlayConfetties();
                    _feelVibrationManager.Vibrate(VibrationType.Continuous,0.5f);
                    break;
                case GameState.Fail:
                    _panelController.Show(PanelType.Fail);
                    break;
            }
        }

        [Button]
        private void ChangeGameState(GameState gameState)
        {
            _eventManager.TriggerGameStateChange(gameState);
        }

    }

    public enum GameState
    {
        Idle,
        Play,
        Win,
        Fail
    }
}