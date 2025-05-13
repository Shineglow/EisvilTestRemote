using System.Threading.Tasks;
using EisvilTest.Scripts.Configuration.Weapon;

namespace EisvilTest.Scripts.Weapon
{
    public interface IWeapon
    {
        IWeaponConfiguration WeaponProperties { get; }
        Task Fire();
    }
}