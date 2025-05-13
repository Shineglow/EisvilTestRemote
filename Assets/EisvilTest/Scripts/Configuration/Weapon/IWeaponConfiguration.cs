namespace EisvilTest.Scripts.Configuration.Weapon
{
    public interface IWeaponConfiguration
    {
        EWeapon Weapon { get; }
        float BaseDamage { get; }
        float AttackTime { get; }
    }

    public enum EWeapon
    {
        Hand,
        Stick,
    }
}