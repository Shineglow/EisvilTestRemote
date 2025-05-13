using System.Collections.Generic;
using Unity.Burst.Intrinsics;

namespace EisvilTest.Scripts.Configuration.Weapon
{
    public class WeaponsConfigurations : BaseConfiguration<EWeapon, IWeaponConfiguration>
    {
        public WeaponsConfigurations()
        {
            Dictionary<EWeapon, IWeaponConfiguration> data = new()
            {
                { EWeapon.Hand , new WeaponConfiguration()
                {
                    Weapon = EWeapon.Hand,
                    BaseDamage = 1,
                    AttackTime = 1,
                }},
                { EWeapon.Stick , new WeaponConfiguration()
                {
                    Weapon = EWeapon.Stick,
                    BaseDamage = 1.5f,
                    AttackTime = 1,
                }}
            };
            AddMany(data);
        }
    }
}