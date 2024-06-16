using System.Collections.Generic;
using DG.Tweening;
using ED.Extensions.System;
using ED.Extensions.Unity;
using UnityEngine;

namespace ED.Tweening
{
	[RequireComponent(typeof(Rigidbody))]
	public class FloaterRigidbody : MonoBehaviour
	{
		[SerializeField]
		private RangeFloat _positionDuration = new (1f, 1f);
		[SerializeField]
		private RangeVector3 _positionStrength = new (new (1f, 1f), new (1f, 1f), new (1f, 1f));
		
		[SerializeField]
		private RangeFloat _rotationDuration = new RangeFloat(1f, 1f);
		[SerializeField]
		private RangeVector3 _rotationStrength = new (new (10f, 10f), new (10f, 10f), new (10f, 10f));

		private Rigidbody _rb;

		private readonly List<Tween> Tweens = new();
		
		private Vector3 _tweenedPosition;
		private Vector3 _tweenedRotation;
		private Quaternion _refRotation;

		private void Awake()
		{
			_rb = GetComponent<Rigidbody>();
			_rb.isKinematic = true;

			var posStr = _positionStrength.GetRandom();
			posStr.x *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			posStr.y *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			posStr.z *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			var posFrom = _rb.position - posStr / 2f;
			var posTo = posFrom + posStr;
			_tweenedPosition = posFrom;
			Tweens.Add(
				DOTween.To(
						() => posFrom.x,
						v => _tweenedPosition = _tweenedPosition.SetX(v),
						posTo.x,
						_positionDuration.GetRandom())
					.SetLoops(-1, LoopType.Yoyo)
					.SetUpdate(UpdateType.Fixed)
					.SetEase(Ease.InOutSine));
			Tweens.Add(
				DOTween.To(
						() => posFrom.y,
						v => _tweenedPosition = _tweenedPosition.SetY(v),
						posTo.y,
						_positionDuration.GetRandom())
					.SetLoops(-1, LoopType.Yoyo)
					.SetUpdate(UpdateType.Fixed)
					.SetEase(Ease.InOutSine));
			Tweens.Add(
				DOTween.To(
						() => posFrom.z,
						v => _tweenedPosition = _tweenedPosition.SetZ(v),
						posTo.z,
						_positionDuration.GetRandom())
					.SetLoops(-1, LoopType.Yoyo)
					.SetUpdate(UpdateType.Fixed)
					.SetEase(Ease.InOutSine));

			var rotStr = _rotationStrength.GetRandom();
			rotStr.x *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			rotStr.y *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			rotStr.z *= RangeExtensions.Random.RangeFloat01() > 0.5f ? 1f : -1f;
			var rotFrom = -rotStr / 2f;
			var rotTo = rotFrom + rotStr;
			_refRotation = _rb.rotation;
			_tweenedRotation = rotFrom;
			Tweens.Add(
				DOTween.To(
						() => rotFrom.x,
						v => _tweenedRotation = _tweenedRotation.SetX(v),
						rotTo.x,
						_rotationDuration.GetRandom())
					.SetLoops(-1, LoopType.Yoyo)
					.SetUpdate(UpdateType.Fixed)
					.SetEase(Ease.InOutSine));
			Tweens.Add(
				DOTween.To(
						() => rotFrom.y,
						v => _tweenedRotation = _tweenedRotation.SetY(v),
						rotTo.y,
						_rotationDuration.GetRandom())
					.SetLoops(-1, LoopType.Yoyo)
					.SetUpdate(UpdateType.Fixed)
					.SetEase(Ease.InOutSine));
			Tweens.Add(
				DOTween.To(
						() => rotFrom.z,
						v => _tweenedRotation = _tweenedRotation.SetZ(v),
						rotTo.z,
						_rotationDuration.GetRandom())
					.SetLoops(-1, LoopType.Yoyo)
					.SetUpdate(UpdateType.Fixed)
					.SetEase(Ease.InOutSine));

			foreach (var tween in Tweens)
				tween.SetLink(gameObject);

			_rb.position = _tweenedPosition;
			_rb.rotation = Quaternion.Euler(_tweenedRotation) * _refRotation;
		}

		private void FixedUpdate()
		{
			_rb.MovePosition(_tweenedPosition);
			_rb.MoveRotation(Quaternion.Euler(_tweenedRotation) * _refRotation);
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