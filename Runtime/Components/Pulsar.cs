using DG.Tweening;
using ED.Tweening.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace ED.Tweening.Components
{
    public class Pulsar : MonoBehaviour
    {
        [SerializeField]
        private float _scale = 1.5f;
        [SerializeField]
        private float _period = 1f;
        
        private Tween _tween;

        private void Awake() {
            var from = transform.localScale;
            var to = from * _scale;
            _tween = DOTweenUtility.DoVector3(
                    () => from,
                    v => transform.localScale = v,
                    () => to,
                    _period / 2f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void OnEnable() => _tween?.Play();
        private void OnDisable() => _tween?.Pause();

        private void OnDestroy() {
            _tween?.Kill();
            _tween = null;
        }
    }
}