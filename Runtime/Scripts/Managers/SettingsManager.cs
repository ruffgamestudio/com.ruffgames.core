using _game.Storage;
using UnityEngine;
using Zenject;

namespace com.ruffgames.core.Runtime.Scripts.Managers
{
    public class SettingsManager
    {
        public bool IsSoundEnabled => _storage.StorageData.SettingsData.IsSoundEnabled;
        public bool IsVibrationEnabled => _storage.StorageData.SettingsData.IsVibrationEnabled;

        [Inject] private IStorage _storage;
    }
}
