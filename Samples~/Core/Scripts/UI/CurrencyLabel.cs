using System;
using _game.Storage;
using com.ruffgames.core.Core.Runtime.Scripts;
using TMPro;
using UnityEngine;
using Zenject;

namespace com.ruffgames.core.Runtime.Scripts.UI
{
    public class CurrencyLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyText;
        private int _totalCurrency => _storage.StorageData.CurrencyData.TotalCurrency;
        
        [Inject] private IStorage _storage;
        [Inject] private EventManager _eventManager;
        private void OnEnable()
        {
            _eventManager.OnCurrencyChanged += UpdateCurrencyText;
        }

        private void OnDisable()
        {
            _eventManager.OnCurrencyChanged -= UpdateCurrencyText;
        }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            UpdateCurrencyText(_totalCurrency);
        }

        private void UpdateCurrencyText(int currency)
        {
            _currencyText.SetText($"{currency}");
        }
    }
}
