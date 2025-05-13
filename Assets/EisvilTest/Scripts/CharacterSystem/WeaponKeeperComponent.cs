using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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

        public void Attack()
        {
            if (IsAttacking) return;
            IsAttacking = true;
            
            _ = AttackAsync();
            IsAttacking = false;
        }
        
        private async UniTask AttackAsync()
        {
            cts = new CancellationTokenSource();
            
            await _animation(transform, distantPoint, _weaponTransform, cts.Token);
            
            _weaponTransform.localRotation = _initialLocalRotation;
            distantPoint.localPosition = _initialLocalPosition;
            transform.localRotation = Quaternion.identity;
        }
    }
}