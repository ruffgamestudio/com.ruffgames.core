using System.Collections.Generic;
using _game.Storage;
using com.ruffgames.core.Core.Runtime.Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace com.ruffgames.core.Runtime.Scripts.Tutorial
{
   public class TutorialManager : MonoBehaviour
   {
      public bool IsActive => _isActive;
      [SerializeField] private List<Tutorial> _tutorialSteps;

      [ShowInInspector, ReadOnly] private bool _isActive;
      [ShowInInspector, ReadOnly] private int _stepIndex;
      [ShowInInspector, ReadOnly] private Tutorial _activeTutorial;
      
      [Inject] private IStorage _storage;

      private void Awake()
      {
         if (_storage.StorageData.TutorialDatas.IsTutorialCompleted)
         {
            Destroy(gameObject);
         }
      }

      public void Start()
      {
         for (_stepIndex = 0; _stepIndex < _tutorialSteps.Count; _stepIndex++)
         {
            var tutorialStep = _tutorialSteps[_stepIndex];
            if (tutorialStep._tutorialData.IsCompleted) continue;
            _isActive = true;
            _activeTutorial = tutorialStep;
            _activeTutorial.StartPhase();
            break;
         }
         
         if (_isActive)
         {
            return;
         }

         DisableTutorials();
      }

      private void DisableTutorials()
      {
         foreach (Transform t in transform)
         {
            t.gameObject.SetActive(false);
         }

         _isActive = false;

      }
      
      public void SetTutorialComplete()
      {
         foreach (var step in _tutorialSteps)
         {
            step.SetData();
            step._tutorialData.IsCompleted = true;
         }

         _storage.StorageData.TutorialDatas.IsTutorialCompleted = true;
      }
      public void NextStep()
      {
         do
         { _stepIndex++;
         } while (_stepIndex <_tutorialSteps.Count && !_tutorialSteps[_stepIndex].IsActive);

         if (_stepIndex >= _tutorialSteps.Count)
         {
            DisableTutorials();
            return;
         }

         _activeTutorial = _tutorialSteps[_stepIndex];
         
         _activeTutorial.IsActive = true;
         _activeTutorial.StartPhase();
      }
   }
}

