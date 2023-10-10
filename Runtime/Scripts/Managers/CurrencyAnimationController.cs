using System.Collections.Generic;
using com.ruffgames.core.Runtime.Scripts.Managers;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace _game.Scripts
{
    public class CurrencyAnimationController : MonoBehaviour
    {
        [SerializeField] private Image _currencyImage;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private int _burstCount;
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _minBurstRadius;
        [SerializeField] private float _maxBurstRadius;
        private List<Image> currencies = new List<Image>();
        private Camera _camera;
        [Inject] private CurrencyManager _currencyManager;

        private void Awake()
        {
            _camera = Camera.main;
        }
   
        [Button]
        public void CreateCurrency(Vector3 position)
        {
            if (currencies.Count != 0)
            {
                SetPosition(position);
                PlayAnimation();
                return;
            }
            
            for (var i = 0; i < _burstCount; i++)
            {
                var currency = Instantiate(_currencyImage, _targetTransform);
                currency.transform.localScale = new Vector3(1.3f,1.3f,1.3f);
                currency.gameObject.SetActive(false);
                currencies.Add(currency);
            }
            
            SetPosition(position);
            PlayAnimation();
        }

        private void SetPosition(Vector3 position)
        {
            var initialTargetPosition = Vector2.zero;
            for (var i = 0; i < _burstCount; i++)
            {
                var currencyPosition = _camera.WorldToScreenPoint(position);
                initialTargetPosition =
                    new Vector2(currencyPosition.x,currencyPosition.y) +
                    Random.insideUnitCircle.normalized * Random.Range(_minBurstRadius, _maxBurstRadius);
                
                currencies[i].transform.position = initialTargetPosition;
                currencies[i].gameObject.SetActive(true);
            }
        }

        private void PlayAnimation()
        {
            foreach (var currency in currencies)
            {
               MoveCurrency(currency.transform);
            }

        }

        private void MoveCurrency(Transform currency)
        {
            var moveSequence = DOTween.Sequence();
            var randDelay = Random.Range(0.1f, 0.2f);
            moveSequence.AppendInterval(randDelay).
                Append(currency.transform.DOMove(_targetTransform.position, _animationDuration)
                    .SetDelay(randDelay));

            moveSequence.OnComplete(() =>
            {
                _currencyManager.IncrementCurrency(10);
               currency.gameObject.SetActive(false);
            });
        }
    }
}
