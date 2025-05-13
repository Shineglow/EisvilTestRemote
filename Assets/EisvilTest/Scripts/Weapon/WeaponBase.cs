namespace EisvilTest.Scripts.Weapon
{
    public class WeaponBase : WeaponMono
    {
        private void OnValidate()
        {
            RecalculateInitialsAndPlaceWeapon();
        }
    }
}
