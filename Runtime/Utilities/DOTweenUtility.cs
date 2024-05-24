using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using EwigeDreamer.Curves;
using EwigeDreamer.Extensions.Unity;
using UnityEngine;

namespace EwigeDreamer.Tweening
{
    public static class DOTweenUtility
    {
        public static TweenerCore<float, float, FloatOptions> DoFloat(Func<float> getter, Action<float> setter, Func<float> value, float duration)
        {
            float start = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(Mathf.LerpUnclamped(start, value(), t)),
                1f,
                duration);
            return tween.OnPlay(() => start = getter());
        }

        public static TweenerCore<float, float, FloatOptions> DoInt(Func<int> getter, Action<int> setter, Func<int> value, float duration)
        {
            int start = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(Mathf.RoundToInt(Mathf.LerpUnclamped(start, value(), t))),
                1f,
                duration);
            return tween.OnPlay(() => start = getter()).OnComplete(() => setter(value()));
        }

        public static TweenerCore<float, float, FloatOptions> DoVector2(Func<Vector2> getter, Action<Vector2> setter, Func<Vector2> value, float duration)
        {
            Vector2 start = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(Vector2.LerpUnclamped(start, value(), t)),
                1f,
                duration);
            return tween.OnPlay(() => start = getter());
        }

        public static TweenerCore<float, float, FloatOptions> DoVector3(Func<Vector3> getter, Action<Vector3> setter, Func<Vector3> value, float duration)
        {
            Vector3 start = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(Vector3.LerpUnclamped(start, value(), t)),
                1f,
                duration);
            return tween.OnPlay(() => start = getter());
        }

        public static TweenerCore<float, float, FloatOptions> DoVector3Slerp(Func<Vector3> getter, Action<Vector3> setter, Func<Vector3> value, float duration)
        {
            Vector3 start = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(Vector3.SlerpUnclamped(start, value(), t)),
                1f,
                duration);
            return tween.OnPlay(() => start = getter());
        }

        public static TweenerCore<float, float, FloatOptions> DoColor(Func<Color> getter, Action<Color> setter, Func<Color> value, float duration)
        {
            Color start = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(Color.LerpUnclamped(start, value(), t)),
                1f,
                duration);
            return tween.OnPlay(() => start = getter());
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier3(Action<Vector2> setter, Func<Vector2> a, Func<Vector2> b, Func<Vector2> c, float duration)
        {
            Vector2 aa = default;
            Vector2 bb = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(BezierUtility.GetPointV2(aa, bb, c(), t)),
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier3(Action<Vector3> setter, Func<Vector3> a, Func<Vector3> b, Func<Vector3> c, float duration)
        {
            Vector3 aa = default;
            Vector3 bb = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(BezierUtility.GetPointV3(aa, bb, c(), t)),
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier3(Action<Vector4> setter, Func<Vector4> a, Func<Vector4> b, Func<Vector4> c, float duration)
        {
            Vector4 aa = default;
            Vector4 bb = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(BezierUtility.GetPointV4(aa, bb, c(), t)),
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier4(Action<Vector2> setter, Func<Vector2> a, Func<Vector2> b, Func<Vector2> c, Func<Vector2> d, float duration)
        {
            Vector2 aa = default;
            Vector2 bb = default;
            Vector2 cc = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(BezierUtility.GetPointV2(aa, bb, cc, d(), t)),
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); cc = c(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier4(Action<Vector3> setter, Func<Vector3> a, Func<Vector3> b, Func<Vector3> c, Func<Vector3> d, float duration)
        {
            Vector3 aa = default;
            Vector3 bb = default;
            Vector3 cc = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(BezierUtility.GetPointV3(aa, bb, cc, d(), t)),
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); cc = c(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier4(Action<Vector4> setter, Func<Vector4> a, Func<Vector4> b, Func<Vector4> c, Func<Vector4> d, float duration)
        {
            Vector4 aa = default;
            Vector4 bb = default;
            Vector4 cc = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(BezierUtility.GetPointV4(aa, bb, cc, d(), t)),
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); cc = c(); });
        }
        

        public static TweenerCore<float, float, FloatOptions> DoBezier3(Action<Vector2, Vector2> setter, Func<Vector2> a, Func<Vector2> b, Func<Vector2> c, float duration)
        {
            Vector2 aa = default;
            Vector2 bb = default;
            var tween = DOTween.To(
                () => 0f,
                t => {
                    BezierUtility.GetPointV2(aa, bb, c(), t, out var point, out var tangent);
                    setter(point, tangent);
                },
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier3(Action<Vector3, Vector3> setter, Func<Vector3> a, Func<Vector3> b, Func<Vector3> c, float duration)
        {
            Vector3 aa = default;
            Vector3 bb = default;
            var tween = DOTween.To(
                () => 0f,
                t => {
                    BezierUtility.GetPointV3(aa, bb, c(), t, out var point, out var tangent);
                    setter(point, tangent);
                },
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier3(Action<Vector4, Vector4> setter, Func<Vector4> a, Func<Vector4> b, Func<Vector4> c, float duration)
        {
            Vector4 aa = default;
            Vector4 bb = default;
            var tween = DOTween.To(
                () => 0f,
                t => {
                    BezierUtility.GetPointV4(aa, bb, c(), t, out var point, out var tangent);
                    setter(point, tangent);
                },
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier4(Action<Vector2, Vector2> setter, Func<Vector2> a, Func<Vector2> b, Func<Vector2> c, Func<Vector2> d, float duration)
        {
            Vector2 aa = default;
            Vector2 bb = default;
            Vector2 cc = default;
            var tween = DOTween.To(
                () => 0f,
                t => {
                    BezierUtility.GetPointV2(aa, bb, cc, d(), t, out var point, out var tangent);
                    setter(point, tangent);
                },
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); cc = c(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier4(Action<Vector3, Vector3> setter, Func<Vector3> a, Func<Vector3> b, Func<Vector3> c, Func<Vector3> d, float duration)
        {
            Vector3 aa = default;
            Vector3 bb = default;
            Vector3 cc = default;
            var tween = DOTween.To(
                () => 0f,
                t => {
                    BezierUtility.GetPointV3(aa, bb, cc, d(), t, out var point, out var tangent);
                    setter(point, tangent);
                },
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); cc = c(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoBezier4(Action<Vector4, Vector4> setter, Func<Vector4> a, Func<Vector4> b, Func<Vector4> c, Func<Vector4> d, float duration)
        {
            Vector4 aa = default;
            Vector4 bb = default;
            Vector4 cc = default;
            var tween = DOTween.To(
                () => 0f,
                t => {
                    BezierUtility.GetPointV4(aa, bb, cc, d(), t, out var point, out var tangent);
                    setter(point, tangent);
                },
                1f,
                duration);
            return tween.OnPlay(() => { aa = a(); bb = b(); cc = c(); });
        }

        public static TweenerCore<float, float, FloatOptions> DoQuaternion(Func<Quaternion> getter, Action<Quaternion> setter, Func<Quaternion> value, float duration)
        {
            Quaternion start = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(Quaternion.SlerpUnclamped(start, value(), t)),
                1f,
                duration);
            return tween.OnPlay(() => start = getter());
        }

        public static Sequence Delay(float duration, bool ignoreTimeScale = false) {
            return DOTween.Sequence().AppendInterval(duration).SetUpdate(ignoreTimeScale);
        }

        public static Sequence DelayedCall(float delay, TweenCallback callback, bool ignoreTimeScale = false)
        {
            return Delay(delay).AppendCallback(callback).SetUpdate(ignoreTimeScale);
        }
        
        public static void PlayDelayedCall(float duration, TweenCallback action, bool ignoreTimeScale = false)
        {
            DelayedCall(duration, action).SetUpdate(ignoreTimeScale).Play();
        }

        public static TweenerCore<float, float, FloatOptions> DoAngleDegrees(Func<float> getter, Action<float> setter, Func<float> value, float duration)
        {
            float start = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(Mathf.LerpAngle(start, value(), t)),
                1f,
                duration);
            return tween.OnPlay(() => start = getter());
        }

        public static TweenerCore<float, float, FloatOptions> DoAngleRadians(Func<float> getter, Action<float> setter, Func<float> value, float duration)
        {
            float start = default;
            var tween = DOTween.To(
                () => 0f,
                t => setter(Mathf.LerpAngle(start * Mathf.Rad2Deg, value() * Mathf.Rad2Deg, t) * Mathf.Deg2Rad),
                1f,
                duration);
            return tween.OnPlay(() => start = getter());
        }

        public static Tween DoJellyBounce(this Transform t, float factor = 1.25f, float duration = 0.5f) {
            var offset = duration * 0.05f;
            duration -= offset;
            var threshold1 = 0.1f;
            var threshold2 = 0.05f;
            var duration1 = duration * threshold1;
            var duration2 = duration * threshold2;;
            var duration3 = duration * (1f - threshold1 - threshold2);
            var mainScale = t.localScale;
            var bounceScale1 = mainScale
                .SetX(mainScale.x * factor)
                .SetY(mainScale.y * 2f - mainScale.y * factor);
            var bounceScale2 = Vector3.LerpUnclamped(mainScale, bounceScale1, 0.5f);
            return DOTween.Sequence()
                .Append(DOTween.Sequence()
                    .Append(t.DOScaleX(bounceScale1.x, duration1 + offset).SetEase(Ease.OutSine))
                    .Append(t.DOScaleX(bounceScale2.x, duration2).SetEase(Ease.InSine))
                    .Append(t.DOScaleX(mainScale.x, duration3).SetEase(Ease.OutElastic)))
                .Join(DOTween.Sequence()
                    .Append(t.DOScaleY(bounceScale1.y, duration1).SetEase(Ease.OutSine))
                    .Append(t.DOScaleY(bounceScale2.y, duration2).SetEase(Ease.InSine))
                    .Append(t.DOScaleY(mainScale.y, duration3).SetEase(Ease.OutElastic)))
                .Join(DOTween.Sequence()
                    .Append(t.DOScaleZ(bounceScale1.z, duration1 + offset).SetEase(Ease.OutSine))
                    .Append(t.DOScaleZ(bounceScale2.z, duration2).SetEase(Ease.InSine))
                    .Append(t.DOScaleZ(mainScale.z, duration3).SetEase(Ease.OutElastic)))
                .SetLink(t.gameObject);
        }

        public static Tween DoJellyPulse(this Transform t, float factor, float duration) {
            var mainScale = t.localScale;
            var pulseScale1 = mainScale * factor;
            var pulseScale2 = Vector3.LerpUnclamped(mainScale, pulseScale1, 0.5f);
            var threshold1 = 0.1f;
            var threshold2 = 0.05f;
            var duration1 = duration * threshold1;
            var duration2 = duration * threshold2;;
            var duration3 = duration * (1f - threshold1 - threshold2);
            return DOTween.Sequence()
                .Append(t.DOScale(pulseScale1, duration1).SetEase(Ease.OutSine))
                .Append(t.DOScale(pulseScale2, duration2).SetEase(Ease.InSine))
                .Append(t.DOScale(mainScale, duration3).SetEase(Ease.OutElastic))
                .SetLink(t.gameObject);
        }

        public static T AddOnPlay<T>(this T tween, TweenCallback callback) where T : Tween
        {
            tween.onPlay += callback;
            return tween;
        }
        public static T AddOnComplete<T>(this T tween, TweenCallback callback) where T : Tween
        {
            tween.onComplete += callback;
            return tween;
        }
        public static T AddOnKill<T>(this T tween, TweenCallback callback) where T : Tween
        {
            tween.onKill += callback;
            return tween;
        }
    }
}
