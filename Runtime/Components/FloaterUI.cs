using System.Collections.Generic;
using DG.Tweening;
using ED.Extensions.System;
using ED.Tweening.Extensions;
using ED.Tweening.Misc;
using UnityEngine;

namespace ED.Tweening.Components
{
	[RequireComponent(typeof(RectTransform))]
	public class FloaterUI : MonoBehaviour
	{
		[SerializeField]
		private RangeFloat _positionDuration = new(1f, 1f);
		[SerializeField]
		private RangeVector2 _positionStrength = new(new(10f, 10f), new(10f, 10f));

		[SerializeField]
		private RangeFloat _rotationDuration = new(1f, 1f);
		[SerializeField]
		private RangeFloat _rotationStrength = new(10f, 10f);

		private readonly List<Tween> Tweens = new();

		private void Awake()
		{
			var rtr = GetComponent<RectTransform>();

			var posStr = _positionStrength.GetRandom();
			posStr.x *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			posStr.y *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			var posFrom = rtr.anchoredPosition - posStr / 2f;
			var posTo = posFrom + posStr;
			rtr.anchoredPosition = posFrom;
			Tweens.Add(rtr
				.DOAnchorPosX(posTo.x, _positionDuration.GetRandom())
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.InOutSine));
			Tweens.Add(rtr
				.DOAnchorPosY(posTo.y, _positionDuration.GetRandom())
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.InOutSine));

			var rotStr = new Vector3(0f, 0f, _rotationStrength.GetRandom());
			rotStr.z *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			var rotFrom = rtr.localRotation.eulerAngles - rotStr / 2f;
			var rotTo = rotFrom + rotStr;
			rtr.localRotation = Quaternion.Euler(rotFrom);
			Tweens.Add(rtr
				.DOLocalRotate(rotTo, _rotationDuration.GetRandom())
				.SetLoops(-1, LoopType.Yoyo)
				.SetEase(Ease.InOutSine));
			
			foreach (var tween in Tweens)
				tween.SetLink(gameObject);
		}

		private void OnEnable()
		{
			foreach (var tween in Tweens)
				tween.Play();
		}

		private void OnDisable()
		{
			foreach (var tween in Tweens)
				tween.Pause();
		}

		private void OnDestroy()
		{
			Tweens.Clear();
		}
	}
}