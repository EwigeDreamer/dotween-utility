using DG.Tweening;
using UnityEngine;

namespace EwigeDreamer.Tweening
{
    public class Spinner : MonoBehaviour
    {
        [SerializeField]
        private Vector3 axis = Vector3.forward;
        [SerializeField]
        private float speed = 1f;

        private Tween tween;

        private void Awake() {
            tween = DOTween.To(
                    () => 0f,
                    SetAngle,
                    360f * Mathf.Sign(speed),
                    Mathf.Abs(360f / speed))
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }

        private void OnEnable() => tween?.Play();
        private void OnDisable() => tween?.Pause();

        private void OnDestroy() {
            tween?.Kill();
            tween = null;
        }

        private void SetAngle(float angle) {
            transform.localRotation = Quaternion.AngleAxis(angle, axis);
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