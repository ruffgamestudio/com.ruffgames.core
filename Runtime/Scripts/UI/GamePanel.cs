using System;
using System.Collections;
using System.Collections.Generic;
using com.ruffgames.core.Runtime.Scripts.Managers;
using com.ruffgames.core.Runtime.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GamePanel : Panel
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _currencyText;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _eventManager.OnLevelSpawned += SetLevelText;
    }

    private void OnDisable()
    {
        _eventManager.OnLevelSpawned -= SetLevelText;
    }

    protected override void OnAwake()
    {
        _restartButton.onClick.AddListener(_levelManager.RestartLevel);
    }
     protected override void OnShowed()
    {
        _currencyText.SetText($"{_currencyManager.GetTotalCurrency()}");
    }
    protected override void OnHide()
    {
      
    }

    private void SetLevelText(int currentLevel)
    {
        _levelText.SetText($"Level {currentLevel}");
    }
}
