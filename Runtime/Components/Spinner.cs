using DG.Tweening;
using UnityEngine;

namespace ED.Tweening.Components
{
    public class Spinner : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _axis = Vector3.forward;
        [SerializeField]
        private float _speed = 1f;

        private Tween _tween;

        private void Awake() {
            _tween = DOTween.To(
                    () => 0f,
                    SetAngle,
                    360f * Mathf.Sign(_speed),
                    Mathf.Abs(360f / _speed))
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }

        private void OnEnable() => _tween?.Play();
        private void OnDisable() => _tween?.Pause();

        private void OnDestroy() {
            _tween?.Kill();
            _tween = null;
        }

        private void SetAngle(float angle) {
            transform.localRotation = Quaternion.AngleAxis(angle, _axis);
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