using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace EisvilTest.Scripts.Weapon
{
    public abstract class WeaponMono : MonoBehaviour
    {
        [SerializeField] protected Transform weaponFoundation;
        [SerializeField] protected Collider weaponCollider;
        [SerializeField] protected float weaponRange = 1f;
        [SerializeField] protected float attackAngle = 90f;
        [SerializeField] protected float attackTime = 0.2f;
        [SerializeField] protected LayerMask hitLayerMask;

        protected Transform weaponKeeper = null;
        protected Quaternion initialRotation;
        protected float degreesPerSecond = 10f;
        private bool isAttacking = true;
        protected bool IsAttacking
        {
            get => isAttacking;
            set
            {
                if(IsAttacking == value) return;
                weaponCollider.enabled = value;
                isAttacking = value;
            }
        }

        public event Action<IDamagable> HitTarget; 

        private void Awake()
        {
            RecalculateInitialsAndPlaceWeapon();
            IsAttacking = false;
        }

        public void Init(float weaponRange, float attackAngle, float attackTime)
        {
            this.weaponRange = weaponRange;
            this.attackAngle = attackAngle;
            this.attackTime = attackTime;
            RecalculateInitialsAndPlaceWeapon();
        }
        
        public void EquipWeapon(Transform weaponKeeper)
        {
            this.weaponKeeper = weaponKeeper;
            weaponFoundation.SetParent(weaponKeeper);
            weaponFoundation.localPosition = Vector3.zero;

            this.hitLayerMask = hitLayerMask;
            weaponCollider.includeLayers = hitLayerMask;
        }
        
        protected void RecalculateInitialsAndPlaceWeapon()
        {
            transform.localPosition = Vector3.forward * weaponRange;
            weaponFoundation.rotation = initialRotation = Quaternion.AngleAxis(-attackAngle/2, Vector3.up);
            degreesPerSecond = attackAngle / attackTime;
        }
        
        public async Task FireAnimation(CancellationToken token)
        {
            try
            {
                IsAttacking = true;
                await PlayAnimation(token);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log("Cancel requested from tokenSource");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                weaponFoundation.rotation = initialRotation;
                IsAttacking = false;
            }
        }
        protected virtual async Task PlayAnimation(CancellationToken token)
        {
            if (IsAttacking) return;
            IsAttacking = true;
            
            float movedDegrees = 0f;

            while (movedDegrees < attackAngle)
            {
                token.ThrowIfCancellationRequested();
                float step = Mathf.Min(degreesPerSecond, attackAngle - movedDegrees);
                weaponFoundation.Rotate(Vector3.up, step);
                movedDegrees += step;
                await Task.Yield();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamagable>(out var damagable))
            {
                HitTarget?.Invoke(damagable);
            }
        }
    }
}