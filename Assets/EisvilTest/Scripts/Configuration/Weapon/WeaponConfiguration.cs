using EisvilTest.Scripts.ResourcesManagement.Enums;
using UnityEngine;

namespace EisvilTest.Scripts.Configuration.Weapon
{
    public class WeaponConfiguration
    {
        public EWeapons Weapon;
        public EWeaponPrefabs Prefab;
        public float Damage;
        public float AttackTime;
        public float AttacksDellay;
        public Vector3 Offset;
        public Quaternion Orientation;
    }
}