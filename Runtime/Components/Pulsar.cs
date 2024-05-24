using DG.Tweening;
using UnityEngine;

namespace EwigeDreamer.Tweening
{
    public class Pulsar : MonoBehaviour
    {
        [SerializeField]
        private float scale = 1.5f;
        [SerializeField]
        private float period = 1f;
        
        private Tween tween;

        private void Awake() {
            var from = transform.localScale;
            var to = from * scale;
            tween = DOTweenUtility.DoVector3(
                    () => from,
                    v => transform.localScale = v,
                    () => to,
                    period / 2f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void OnEnable() => tween?.Play();
        private void OnDisable() => tween?.Pause();

        private void OnDestroy() {
            tween?.Kill();
            tween = null;
        }
    }
}