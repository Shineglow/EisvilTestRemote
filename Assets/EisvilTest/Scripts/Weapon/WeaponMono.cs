using System;
using UnityEngine;

namespace EisvilTest.Scripts.Weapon
{
    [RequireComponent(typeof(Collider))]
    public class WeaponMono : MonoBehaviour
    {
        [SerializeField] private new Collider collider;

        public event Action<GameObject> WeaponHitTarget;

        private void Awake()
        {
            collider.isTrigger = true;
        }

        public void SetCollisionEnabled(bool isActive)
        {
            collider.enabled = isActive;
        }

        public void SetLayerMask(LayerMask mask)
        {
            collider.includeLayers = mask;
        }

        private void OnTriggerEnter(Collider other)
        {
            WeaponHitTarget?.Invoke(other.gameObject);
        }
    }
}