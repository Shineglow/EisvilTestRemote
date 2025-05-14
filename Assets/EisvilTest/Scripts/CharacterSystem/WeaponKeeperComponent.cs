using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace EisvilTest.Scripts.CharacterSystem
{
    public class WeaponKeeperComponent : MonoBehaviour
    {
        [SerializeField] private Transform distantPoint;
        private Transform _weaponTransform;
        private Func<Transform, Transform, Transform, CancellationToken, UniTask> _animation = (keeperTransform, distantPointTransform, weaponTransform, cancellationToken) => UniTask.CompletedTask;
        private CancellationTokenSource cts;
        public bool IsAttacking { get; private set; }
        private Quaternion _initialLocalRotation;
        private Vector3 _initialLocalPosition;

        private void Awake()
        {
            if (distantPoint == null)
            {
                distantPoint = new GameObject("DistantPoint").transform;
                distantPoint.localPosition = Vector3.zero;
            }
        }

        public void PutWeapon(Transform obj, Vector3 localPosition, Quaternion localRotation, Func<Transform, Transform, Transform, CancellationToken, UniTask> animationFunction)
        {
            _weaponTransform = obj;
            _weaponTransform.SetParent(distantPoint);
            obj.localPosition = Vector3.zero;
            _weaponTransform.localRotation = _initialLocalRotation = localRotation;
            distantPoint.localPosition = _initialLocalPosition = localPosition;
            _animation = animationFunction;
        }

        public void Attack(float delay)
        {
            if (IsAttacking) return;
            _ = AttackAsync(delay);
        }
        
        private async UniTask AttackAsync(float delay)
        {
            IsAttacking = true;
            cts = new CancellationTokenSource();
            
            await _animation(transform, distantPoint, _weaponTransform, cts.Token);

            var returnRotation = _weaponTransform.DOLocalRotate(_initialLocalRotation.eulerAngles, 0.2f).AsyncWaitForCompletion().AsUniTask();
            var returnDistantPointPosition = distantPoint.DOLocalMove(_initialLocalPosition, 0.2f).AsyncWaitForCompletion().AsUniTask();
            var returnSelfRotation = transform.DOLocalRotate(Vector3.zero, 0.2f).AsyncWaitForCompletion().AsUniTask();

            await UniTask.WhenAll(UniTask.WaitForSeconds(delay), returnRotation, returnDistantPointPosition, returnSelfRotation);

            IsAttacking = false;
        }
    }
}