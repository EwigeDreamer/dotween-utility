using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace ED.Tweening
{
    internal static class DOTweenExtensionsInternal
    {
        internal static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPosX(this RectTransform target, float endValue, float duration, bool snapping = false)
        {
            var t = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(endValue, 0), duration);
            t.SetOptions(AxisConstraint.X, snapping).SetTarget(target);
            return t;
        }
        
        internal static TweenerCore<Vector2, Vector2, VectorOptions> DOAnchorPosY(this RectTransform target, float endValue, float duration, bool snapping = false)
        {
            var t = DOTween.To(() => target.anchoredPosition, x => target.anchoredPosition = x, new Vector2(0, endValue), duration);
            t.SetOptions(AxisConstraint.Y, snapping).SetTarget(target);
            return t;
        }
        
        internal static Tweener DOShakeAnchorPos(this RectTransform target, float duration, Vector2 strength, int vibrato = 10, float randomness = 90, bool snapping = false, bool fadeOut = true, ShakeRandomnessMode randomnessMode = ShakeRandomnessMode.Full)
        {
            return DOTween.Shake(() => target.anchoredPosition, x => target.anchoredPosition = x, duration, strength, vibrato, randomness, fadeOut, randomnessMode)
                .SetTarget(target).SetSpecialStartupMode(SpecialStartupMode.SetShake).SetOptions(snapping);
        }
    }
}