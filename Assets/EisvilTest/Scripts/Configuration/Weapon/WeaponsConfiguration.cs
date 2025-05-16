using UnityEngine;

namespace EisvilTest.Scripts.Configuration.Weapon
{
    public class WeaponsConfiguration : ConfigurationBase<EWeapons, WeaponConfiguration>
    {
        public WeaponsConfiguration()
        {
            _keyToData = new()
            {
                { EWeapons.Stick, new WeaponConfiguration()
                {
                    Damage = 1.5f,
                    AttackTime = 0.2f,
                    AttacksDellay = 0.3f,
                    Offset = (Vector3.right + Vector3.forward).normalized,
                    Orientation = Quaternion.Euler(90, 45,0),
                }},
            };
        }
    }
}