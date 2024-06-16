using ED.Extensions.System;
using UnityEngine;
using Random = System.Random;

namespace ED.Tweening
{
    internal static class RangeExtensions
    {
        public static readonly Random Random = new();

        public static float GetRandom(this RangeFloat range, Random random = null)
        {
            random ??= Random;
            return random.RangeFloat(range.min, range.max);
        }

        public static Vector2 GetRandom(this RangeVector2 range, Random random = null)
        {
            random ??= Random;
            return new Vector2(
                random.RangeFloat(range.rangeX.min, range.rangeX.max),
                random.RangeFloat(range.rangeY.min, range.rangeY.max));
        }

        public static Vector3 GetRandom(this RangeVector3 range, Random random = null)
        {
            random ??= Random;
            return new Vector3(
                random.RangeFloat(range.rangeX.min, range.rangeX.max),
                random.RangeFloat(range.rangeY.min, range.rangeY.max),
                random.RangeFloat(range.rangeZ.min, range.rangeZ.max));
        }
    }
}