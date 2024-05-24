using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace EwigeDreamer.Tweening
{
    public class Shaker : MonoBehaviour
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

        [SerializeField]
        private float _scaleDuration = 5f;
        [SerializeField]
        private Vector3 _scaleStrength = Vector3.one;
        [SerializeField]
        private int _scaleVibrato = 10;
        [SerializeField]
        private ShakeRandomnessMode _scaleRandomnessMode = ShakeRandomnessMode.Full;


        private readonly List<Tween> Tweens = new();

        private void Awake()
        {
            Tweens.Add(transform.DOShakePosition(
                    _positionDuration,
                    _positionStrength,
                    _positionVibrato,
                    fadeOut: false,
                    randomnessMode: _positionRandomnessMode)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear));
            Tweens.Add(transform.DOShakeRotation(
                    _rotationDuration,
                    _rotationStrength,
                    _rotationVibrato,
                    fadeOut: false,
                    randomnessMode: _rotationRandomnessMode)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear));
            Tweens.Add(transform.DOShakeScale(
                    _scaleDuration,
                    _scaleStrength,
                    _scaleVibrato,
                    fadeOut: false,
                    randomnessMode: _scaleRandomnessMode)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear));
			
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