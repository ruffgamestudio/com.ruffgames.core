using DG.Tweening;
using TMPro;
using UnityEngine;

namespace com.ruffgames.core.Runtime.Scripts
{
    public class MessageController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _toastText;

        public void SetText(string message)
        {
            _toastText.SetText(message);
        }

        public void FadeEffect(float target,float duration,float delay)
        {
            _toastText.DOFade(target, duration).SetDelay(delay);
        }

        public void StopFade()
        {
            DOTween.Kill(_toastText);
        }
    }
}