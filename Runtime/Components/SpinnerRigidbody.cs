using DG.Tweening;
using UnityEngine;

namespace ED.Tweening
{
    [RequireComponent(typeof(Rigidbody))]
    public class SpinnerRigidbody : MonoBehaviour
    {
        [SerializeField]
        private Vector3 axis = Vector3.forward;
        [SerializeField]
        private float speed = 1f;

        private Tween tween;

        private Rigidbody _rb;
        
        private float _tweenedAngle;
        private Quaternion _refRotation;

        private void Awake() {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
            _refRotation = _rb.rotation;
            
            tween = DOTween.To(
                    () => 0f,
                    v => _tweenedAngle = v,
                    360f * Mathf.Sign(speed),
                    Mathf.Abs(360f / speed))
                .SetLoops(-1, LoopType.Restart)
                .SetUpdate(UpdateType.Fixed)
                .SetEase(Ease.Linear);
        }

        private void FixedUpdate()
        {
            _rb.MoveRotation(Quaternion.AngleAxis(_tweenedAngle, axis) * _refRotation);
        }

        private void OnEnable() => tween?.Play();
        private void OnDisable() => tween?.Pause();

        private void OnDestroy() {
            tween?.Kill();
            tween = null;
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos() {
            var parent = transform.parent;
            var parentRotation = parent != null ? parent.rotation : Quaternion.identity;
            var normal = parentRotation * axis.normalized;
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