using Cysharp.Threading.Tasks;
using EisvilTest.Scripts.CharacterSystem;
using EisvilTest.Scripts.Configuration.Characters;
using EisvilTest.Scripts.Triggers;
using UnityEngine;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.Controllers;
using EisvilTest.Scripts.Input;
using EisvilTest.Scripts.Weapon;

namespace EisvilTest.Scripts
{
    public class BootstrapMain : MonoBehaviour
    {
        private CharactersConfiguration _charactersConfiguration;
        private ICharacterController _characterController;

        [SerializeField] private Character character;
        [SerializeField] private WeaponMono _weaponMono;
        private WeaponsConfiguration _weaponsConfiguration;

        private void Awake()
        {
            // initialization here
            int mask = LayerMask.GetMask("Player");
        }

        private void Start()
        {
            _charactersConfiguration = new CharactersConfiguration();
            _weaponsConfiguration = new WeaponsConfiguration();
            var characterControllerPC = new GameObject("CharacterController").AddComponent<CharacterControllerPC>();
            
            InputCreator.CreateBoundedInstances(out var setter, out var getter);
            characterControllerPC.Init(new PlayerControls(), setter);
            character.Init(_charactersConfiguration.GetData(ECharacter.Player));
            character.SetInput(getter);
            var weaponConfiguration = _weaponsConfiguration.GetData(EWeapons.Stick);
            WeaponLogic weapon = new WeaponLogic(_weaponMono, weaponConfiguration);
            character.SetWeapon(weapon, weaponConfiguration,
                async (weaponKeeper, distantPoint, self, token) =>
                {
                    float hitAngle = 90f;
                    float angleTraveled = 0f;
                    float anglePerSecond = hitAngle / weaponConfiguration.AttackTime;
                    float startAngleY = weaponKeeper.localEulerAngles.y;

                    while (angleTraveled < hitAngle)
                    {
                        token.ThrowIfCancellationRequested();

                        float step = anglePerSecond * Time.deltaTime;
                        step = Mathf.Min(step, hitAngle - angleTraveled);
                        angleTraveled += step;
                        weaponKeeper.localRotation = Quaternion.Euler(0f, startAngleY - angleTraveled, 0f);

                        await UniTask.Yield();
                    }
                });
        }

        private void OnUniversalTriggerEnter(GameObject obj)
        {
            Debug.Log("Enter");
        }
        
        private void OnUniversalTriggerExit(GameObject obj)
        {
            Debug.Log("Exit");
        }
    }
}
