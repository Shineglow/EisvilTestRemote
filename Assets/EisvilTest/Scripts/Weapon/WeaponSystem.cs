using System.Collections.Generic;
using EisvilTest.Scripts.Configuration;
using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.ResourcesManagement;
using EisvilTest.Scripts.ResourcesManagement.Enums;

namespace EisvilTest.Scripts.Weapon
{
    public class WeaponSystem
    {
        private Dictionary<IWeaponLogic, WeaponLogic> _converter;
        private Dictionary<EWeapons, (HashSet<WeaponLogic> active, Stack<WeaponLogic> inactiveLogic, Stack<WeaponMono> inactiveMono)> _poolsByType = new();

        private readonly ConfigurationBase<EWeapons, WeaponConfiguration> _weaponsConfiguration;
        private readonly ResourceManager _resourceManager;

        public WeaponSystem()
        {
            _weaponsConfiguration = CompositionRoot.GetWeaponsConfiguration();
            _resourceManager = CompositionRoot.GetResourceManager();
        }

        public IWeaponLogic GetWeapon(EWeapons weapon)
        {
            HashSet<WeaponLogic> active;
            Stack<WeaponLogic> inactiveLogic;
            Stack<WeaponMono> inactiveMono;
            WeaponLogic weaponLogic;
            WeaponMono weaponMono;

            var weaponConfiguration = _weaponsConfiguration.GetData(weapon);

            if (_poolsByType.TryGetValue(weapon, out var pools))
            {
                (active, inactiveLogic, inactiveMono) = pools;
                weaponMono = inactiveMono.Count > 0 ? inactiveMono.Pop() : _resourceManager.CreatePrefabInstance<WeaponMono, EWeaponPrefabs>(weaponConfiguration.Prefab);
                weaponLogic = inactiveLogic.Count > 0 ? inactiveLogic.Pop() : new WeaponLogic(weaponMono, weaponConfiguration);
            }
            else
            {
                _poolsByType.Add(weapon, (active = new(), inactiveLogic = new(), inactiveMono = new()));
                weaponMono = _resourceManager.CreatePrefabInstance<WeaponMono, EWeaponPrefabs>(weaponConfiguration.Prefab);
                weaponLogic = new WeaponLogic(weaponMono, weaponConfiguration);
            }
            active.Add(weaponLogic);

            return weaponLogic;
        }

        public void ReturnWeapon(IWeaponLogic weaponLogic)
        {
            var weaponLogicFull = _converter[weaponLogic];
            var (active, inactiveLogic, inactiveMono) = _poolsByType[weaponLogicFull.Weapon];
            active.Remove(weaponLogicFull);
            inactiveLogic.Push(weaponLogicFull);
            inactiveMono.Push(weaponLogicFull.WeaponMono);
        }
    }
}