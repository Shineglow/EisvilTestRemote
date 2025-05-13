namespace EisvilTest.Scripts.Weapon
{
    public interface IDamagable
    {
        float Health { get; }
        void DealDamage(float damage);
    }
}