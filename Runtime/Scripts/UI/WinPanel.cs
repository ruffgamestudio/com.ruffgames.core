using com.ruffgames.core.Runtime.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;


public class WinPanel : Panel
{
    [SerializeField] private Button _nextLevelButton;
    protected override void OnAwake()
    {
        _nextLevelButton.onClick.AddListener(_levelManager.NextLevel);
    }
    protected override void OnShowed()
    {
      
    }
    protected override void OnHide()
    {
      
    }
}
