using _game.Storage;
using com.ruffgames.core.Runtime.Scripts.Storage;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace com.ruffgames.core.Runtime.Scripts.Tutorial
{
    public abstract class Tutorial : MonoBehaviour
    {
        public ID Id;
        public string EventName;
        public bool IsActive;
        public TutorialData _tutorialData;
        
        [SerializeField] private string Message;
        [SerializeField] protected GameObject _hand;
       
        protected TutorialManager _tutorialManager;
        protected MessageController _messageController;
        protected IStorage _storage;

        [Inject]
        private void Construct(IStorage storage,TutorialManager tutorialManager,MessageController messageController)
        {
            _storage = storage;
            _tutorialManager = tutorialManager;
            _messageController = messageController;
        }

        public void SetData()
        {
            _tutorialData = _storage.StorageData.TutorialDatas.GetTutorial(Id.Id);
        }
        
        protected virtual void OnStepStart(){}
        protected virtual void OnStepUpdate(){}
        protected virtual void OnStepComplete(){}

        public void StartPhase()
        {
            if (!IsActive) return;
         
            _messageController.FadeEffect(1,0,0);
            
            if (_tutorialData.IsCompleted)
            {
                OnStepComplete();
            }
            else
            {
                IsActive = true;
                _messageController.SetText(Message);
                OnStepStart();
                _hand.SetActive(true);
            }
            
        }

        protected void Update()
        {
            if (_tutorialData.IsCompleted) return;
            if (!IsActive) return;
            OnStepUpdate();
        }

        [Button]
        protected void CompletePhase()
        {
            IsActive = false;
            _messageController.SetText("");
            _hand.SetActive(false);
            
            OnStepComplete();
            _tutorialData.IsCompleted = true;
            _tutorialManager.NextStep();
          

        }
        
    }
}