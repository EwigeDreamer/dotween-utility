using System;
using DG.Tweening;

namespace EwigeDreamer.Tweening
{
    public static class DOTweenExtensions
    {
        public static IDisposable ToDisposable<T>(this T tween, bool complete = false) where T : Tween
        {
            return new DisposableTween<T>(tween, complete);
        }

        private class DisposableTween<T> : IDisposable where T : Tween
        {
            private readonly T tween;
            private readonly bool complete;
            public DisposableTween(T tween, bool complete) {
                this.tween = tween;
                this.complete = complete;
                tween.AddOnComplete(() => tween = null);
            }
            public void Dispose() {
                tween?.Kill(complete);
            }
        }
    }
}