using _game.Storage;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace com.ruffgames.core.Runtime.Scripts.Managers
{
    public class CurrencyManager : MonoBehaviour
    {
        private int _collectedCurrency
        {
            get => _storage.StorageData.CurrencyData.CollectedCurrency;
            set => _storage.StorageData.CurrencyData.CollectedCurrency = value;
        }

        private int _totalCurrency
        {
            get => _storage.StorageData.CurrencyData.TotalCurrency;
            set => _storage.StorageData.CurrencyData.TotalCurrency = value;
        }

        [Inject] private IStorage _storage;

        [PropertySpace]
        [Button(ButtonSizes.Large), GUIColor(0.2f, 1f, 0)]
        public void IncrementCurrency(int value)
        {
            _collectedCurrency += value;
        }

        public void CollectCurrency()
        {
            _totalCurrency = _collectedCurrency;
            ClearCollected();
        }

        public int GetTotalCurrency()
        {
            return _totalCurrency;
        }

        public int GetCollectedCurrency()
        {
            return _collectedCurrency;
        }

        private void ClearCollected()
        {
            _collectedCurrency = 0;
        }
    }
}
