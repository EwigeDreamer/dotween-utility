using System.Collections;
using DG.Tweening;
using ED.Tweening.Utilities;
using UnityEngine;
using UnityEngine.Pool;

namespace ED.Tweening.Samples.UtilitySample
{
    public class UtilitySample : MonoBehaviour
    {
        [SerializeField] private Transform _from;
        [SerializeField] private Transform _to;
        [SerializeField] private Transform _prefab;
        [SerializeField] private float _tangentLength = 5f;
        [SerializeField] private float _tweenDuration = 2.5f;
        [SerializeField] private float _tweenDelay = 0.01f;

        private ObjectPool<Transform> _objPool;

        private void Awake()
        {
            _objPool = new ObjectPool<Transform>(
                () => Instantiate(_prefab),
                a => a.gameObject.SetActive(true),
                a => a.gameObject.SetActive(false),
                Destroy
            );
        }

        private IEnumerator Start()
        {
            _from
                .DOMove(_from.position + Vector3.up * 5f, Random.Range(5f, 10f))
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
                .SetLink(_from.gameObject)
                .Play();
            _to
                .DOMove(_to.position + Vector3.up * 5f, Random.Range(5f, 10f))
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
                .SetLink(_to.gameObject)
                .Play();

            while (true)
            {
                var obj = _objPool.Get();
                obj.position = _from.position;
                DOTweenUtility.DoBezier4(
                        (position, tangent) => obj.SetPositionAndRotation(position, Quaternion.LookRotation(tangent)),
                        () => _from.position,
                        () => _from.position + Random.onUnitSphere * _tangentLength,
                        () => _to.position + (_from.position - _to.position).normalized * _tangentLength,
                        () => _to.position,
                        _tweenDuration)
                    .SetEase(Ease.InSine)
                    .OnComplete(() => _objPool.Release(obj))
                    .SetLink(obj.gameObject)
                    .Play();
                yield return new WaitForSeconds(_tweenDelay);
            }
        }
    }
}