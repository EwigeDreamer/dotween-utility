using System;
using DG.Tweening;
using ED.Tweening.Utilities;

namespace ED.Tweening.Extensions
{
    public static class DOTweenExtensions
    {
        public static IDisposable ToDisposable<T>(this T tween, bool complete = false) where T : Tween
        {
            return new DisposableTween<T>(tween, complete);
        }

        private class DisposableTween<T> : IDisposable where T : Tween
        {
            private readonly T _tween;
            private readonly bool _complete;
            public DisposableTween(T tween, bool complete) {
                _tween = tween;
                _complete = complete;
                tween.AddOnComplete(() => tween = null);
            }
            public void Dispose() {
                _tween?.Kill(_complete);
            }
        }
    }
}