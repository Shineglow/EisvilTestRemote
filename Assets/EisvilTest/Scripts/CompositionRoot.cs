using EisvilTest.Scripts.Characters;
using EisvilTest.Scripts.Configuration;
using EisvilTest.Scripts.Configuration.Characters;
using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.Configuration.Quests;
using EisvilTest.Scripts.Configuration.Quests.ColorScheme;
using EisvilTest.Scripts.Configuration.Weapon;
using EisvilTest.Scripts.General;
using EisvilTest.Scripts.GUI.Quests;
using EisvilTest.Scripts.Quests;
using EisvilTest.Scripts.ResourcesManagement;
using EisvilTest.Scripts.ResourcesManagement.Enums;
using EisvilTest.Scripts.ResourcesManagement.Pools;
using EisvilTest.Scripts.Weapon;
using UnityEngine;

namespace EisvilTest.Scripts
{
    public class CompositionRoot : MonoBehaviour
    {
        private static ResourceManager ResourceManager;
        private static CharactersSystem CharactersSystem;
        private static WeaponSystem WeaponSystem;
        private static CameraController MainCamera;
        private static PoolsAggregator PoolsAggregator;
        private static GuiMain GuiMain;
        private static QuestSystem QuestSystem;
        private static QuestsGUIShort QuestsGUIShort;
        private static IQuestColorScheme QuestColorScheme;
        
        private static ConfigurationBase<EWeapons, WeaponConfiguration> WeaponsConfiguration;
        private static ConfigurationBase<ECharacter, ICharacterConfigurationData> CharactersConfiguration;
        private static ConfigurationBase<EQuests, QuestConfiguration> QuestsConfiguration;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public static ResourceManager GetResourceManager()
        {
            return ResourceManager ??= new ResourceManager();
        }

        public static CharactersSystem GetCharactersSystem()
        {
            return CharactersSystem ??= new CharactersSystem();
        }

        public static WeaponSystem GetWeaponSystem()
        {
            return WeaponSystem ??= new WeaponSystem();
        }

        public static CameraController GetMainCamera()
        {
            return MainCamera ??= GetResourceManager()
                .CreatePrefabInstance<CameraController, EGeneralPrefabs>(EGeneralPrefabs.MainCamera);
        }

        public static PoolsAggregator GetPoolsAggregator()
        {
            return PoolsAggregator ??= new PoolsAggregator();
        }

        public static ConfigurationBase<EWeapons, WeaponConfiguration> GetWeaponsConfiguration()
        {
            return WeaponsConfiguration ??= new WeaponsConfiguration();
        }
        
        public static ConfigurationBase<ECharacter, ICharacterConfigurationData> GetCharactersConfiguration()
        {
            return CharactersConfiguration ??= new CharactersConfiguration();
        }

        public static ConfigurationBase<EQuests, QuestConfiguration> GetQuestsConfiguration()
        {
            return QuestsConfiguration ??= new QuestsConfiguration();
        }

        public static GuiMain GetGuiMain()
        {
            return GuiMain ??= GetResourceManager()
                .CreatePrefabInstance<GuiMain, EGeneralPrefabs>(EGeneralPrefabs.GuiMain);
        }

        public static QuestsGUIShort GetQuestsGUIShort()
        {
            return QuestsGUIShort ??= GetResourceManager()
                .CreatePrefabInstance<QuestsGUIShort, EGuiQuestsPrefabs>(EGuiQuestsPrefabs.QuestsGuiShort);
        }

        public static QuestSystem GetQuestsSystem()
        {
            return QuestSystem ??= new QuestSystem();
        }

        public static IQuestColorScheme GetQuestsColorScheme()
        {
            return QuestColorScheme ??= new QuestColorScheme();
        }
    }
}