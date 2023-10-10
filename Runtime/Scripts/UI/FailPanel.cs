using UnityEngine;
using UnityEngine.UI;

namespace com.ruffgames.core.Runtime.Scripts.UI
{
   public class FailPanel : Panel
   {
      [SerializeField] private Button _restartButton;
      protected override void OnAwake()
      {
         _restartButton.onClick.AddListener(_levelManager.RestartLevel);
      }
      protected override void OnShowed()
      {
      
      }
      protected override void OnHide()
      {
      
      }
   }
}
