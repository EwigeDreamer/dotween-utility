using System.Collections.Generic;
using DG.Tweening;
using ED.Extensions.System;
using ED.Extensions.Unity;
using ED.Tweening.Extensions;
using ED.Tweening.Misc;
using UnityEngine;

namespace ED.Tweening.Components
{
	public class Floater : MonoBehaviour
	{
		[SerializeField]
		private RangeFloat _positionDuration = new (1f, 1f);
		[SerializeField]
		private RangeVector3 _positionStrength = new (new (1f, 1f), new (1f, 1f), new (1f, 1f));
		
		[SerializeField]
		private RangeFloat _rotationDuration = new RangeFloat(1f, 1f);
		[SerializeField]
		private RangeVector3 _rotationStrength = new (new (10f, 10f), new (10f, 10f), new (10f, 10f));
		 
		private readonly List<Tween> Tweens = new();

		private void Awake()
		{
			var tr = transform;

			var posStr = _positionStrength.GetRandom();
			posStr.x *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			posStr.y *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			posStr.z *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			var posFrom = tr.localPosition - posStr / 2f;
			var posTo = posFrom + posStr;
			tr.localPosition = posFrom;
			Tweens.Add(tr
				.DOLocalMoveX(posTo.x, _positionDuration.GetRandom())
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.InOutSine));
			Tweens.Add(tr
				.DOLocalMoveY(posTo.y, _positionDuration.GetRandom())
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.InOutSine));
			Tweens.Add(tr
				.DOLocalMoveZ(posTo.z, _positionDuration.GetRandom())
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.InOutSine));

			var rotStr = _rotationStrength.GetRandom();
			rotStr.x *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			rotStr.y *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			rotStr.z *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			var rotFrom = tr.localRotation.eulerAngles - rotStr / 2f;
			var rotTo = rotFrom + rotStr;
			tr.localRotation = Quaternion.Euler(rotFrom);
			Tweens.Add(
				DOTween.To(
						() => rotFrom.x,
						v => tr.localRotation = Quaternion.Euler(tr.localRotation.eulerAngles.SetX(v)),
						rotTo.x,
						_rotationDuration.GetRandom())
					.SetLoops(-1, LoopType.Yoyo)
					.SetEase(Ease.InOutSine));
			Tweens.Add(
				DOTween.To(
						() => rotFrom.y,
						v => tr.localRotation = Quaternion.Euler(tr.localRotation.eulerAngles.SetY(v)),
						rotTo.y,
						_rotationDuration.GetRandom())
					.SetLoops(-1, LoopType.Yoyo)
					.SetEase(Ease.InOutSine));
			Tweens.Add(
				DOTween.To(
						() => rotFrom.z,
						v => tr.localRotation = Quaternion.Euler(tr.localRotation.eulerAngles.SetZ(v)),
						rotTo.z,
						_rotationDuration.GetRandom())
					.SetLoops(-1, LoopType.Yoyo)
					.SetEase(Ease.InOutSine));

			foreach (var tween in Tweens)
				tween.SetLink(gameObject);
		}

		private void OnEnable() {
			foreach (var tween in Tweens)
				tween.Play();
		}
		
		private void OnDisable() {
			foreach (var tween in Tweens)
				tween.Pause();
		}
		
		private void OnDestroy()
		{
			Tweens.Clear();
		}
	}
}