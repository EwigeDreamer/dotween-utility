using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace EwigeDreamer.Tweening
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShakerRigidbody : MonoBehaviour
    {
        [SerializeField]
        private float _positionDuration = 5f;
        [SerializeField]
        private Vector3 _positionStrength = Vector3.one;
        [SerializeField]
        private int _positionVibrato = 10;
        [SerializeField]
        private ShakeRandomnessMode _positionRandomnessMode = ShakeRandomnessMode.Full;

        [SerializeField]
        private float _rotationDuration = 5f;
        [SerializeField]
        private Vector3 _rotationStrength = Vector3.one;
        [SerializeField]
        private int _rotationVibrato = 10;
        [SerializeField]
        private ShakeRandomnessMode _rotationRandomnessMode = ShakeRandomnessMode.Full;
        
        private Rigidbody _rb;
        
        private readonly List<Tween> Tweens = new();
        
        private Vector3 _tweenedPosition;
        private Vector3 _tweenedRotation;
        private Quaternion _refRotation;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;

            _tweenedPosition = _rb.position;
            _tweenedRotation = Vector3.zero;
            _refRotation = _rb.rotation;
            
            Tweens.Add(
                DOTween.Shake(
                    () => _tweenedPosition,
                    v => _tweenedPosition = v,
                    _positionDuration,
                    _positionStrength,
                    _positionVibrato,
                    fadeOut: false,
                    randomnessMode: _positionRandomnessMode)
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(UpdateType.Fixed)
                .SetEase(Ease.Linear));
            Tweens.Add(
                DOTween.Shake(
                    () => _tweenedRotation,
                    v => _tweenedRotation = v,
                    _rotationDuration,
                    _rotationStrength,
                    _rotationVibrato,
                    fadeOut: false,
                    randomnessMode: _rotationRandomnessMode)
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(UpdateType.Fixed)
                .SetEase(Ease.Linear));
			
            foreach (var tween in Tweens)
                tween.SetLink(gameObject);
        }

        private void FixedUpdate()
        {
            _rb.MovePosition(_tweenedPosition);
            _rb.MoveRotation(Quaternion.Euler(_tweenedRotation) * _refRotation);
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