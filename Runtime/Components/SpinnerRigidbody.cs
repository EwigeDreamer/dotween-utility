using DG.Tweening;
using UnityEngine;

namespace ED.Tweening.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpinnerRigidbody : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _axis = Vector3.forward;
        [SerializeField]
        private float _speed = 1f;

        private Tween _tween;

        private Rigidbody _rb;
        
        private float _tweenedAngle;
        private Quaternion _refRotation;

        private void Awake() {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
            _refRotation = _rb.rotation;
            
            _tween = DOTween.To(
                    () => 0f,
                    v => _tweenedAngle = v,
                    360f * Mathf.Sign(_speed),
                    Mathf.Abs(360f / _speed))
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(UpdateType.Fixed)
                .SetEase(Ease.Linear);
        }

        private void FixedUpdate()
        {
            _rb.MoveRotation(Quaternion.AngleAxis(_tweenedAngle, _axis) * _refRotation);
        }

        private void OnEnable() => _tween?.Play();
        private void OnDisable() => _tween?.Pause();

        private void OnDestroy() {
            _tween?.Kill();
            _tween = null;
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos() {
            var parent = transform.parent;
            var parentRotation = parent != null ? parent.rotation : Quaternion.identity;
            var normal = parentRotation * _axis.normalized;
            var point = transform.position;
            var color = Color.green;

            var gizmosColorTmp = Gizmos.color;
            var handlesColorTmp = UnityEditor.Handles.color;
            Gizmos.color = color;
            UnityEditor.Handles.color = color;
            
            UnityEditor.Handles.DrawWireDisc(point, normal, 0.5f);
            Gizmos.DrawRay(point, normal);
            
            Gizmos.color = gizmosColorTmp;
            UnityEditor.Handles.color = handlesColorTmp;
        }
        #endif
    }
}