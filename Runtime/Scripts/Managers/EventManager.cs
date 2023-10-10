using com.ruffgames.core.Runtime.Scripts.Managers;
using UnityEngine.Events;

namespace com.ruffgames.core.Core.Runtime.Scripts
{
    public class EventManager
    {
        public UnityAction<GameState> OnGameStateChanged;
        public UnityAction<int> OnCurrencyChanged;
        public UnityAction<int> OnLevelSpawned;
        public UnityAction<bool> OnSoundStateChanged;

        public void TriggerGameStateChange(GameState gameState) => OnGameStateChanged?.Invoke(gameState);
        public void TriggerCurrencyChange(int value) => OnCurrencyChanged?.Invoke(value);
        public void TriggerLevelSpawn(int value) => OnLevelSpawned?.Invoke(value);
        public void TriggerOnSoundStateChanged(bool value) => OnSoundStateChanged?.Invoke(value);
    }
}
