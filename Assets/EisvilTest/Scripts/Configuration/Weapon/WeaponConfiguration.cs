namespace EisvilTest.Scripts.Configuration.Weapon
{
    public class WeaponConfiguration : IWeaponConfiguration
    {
        public EWeapon Weapon { get; set; }
        public float BaseDamage { get; set; }
        public float AttackTime { get; set; }
    }
}