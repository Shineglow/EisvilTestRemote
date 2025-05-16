using EisvilTest.Scripts.Configuration.Characters.CharactersData;
using EisvilTest.Scripts.Configuration.Quests.Conditions.Inheritors;
using UnityEngine;

namespace EisvilTest.Scripts.Configuration.Quests
{
    public class QuestsConfiguration : ConfigurationBase<EQuests, QuestConfiguration>
    {
        public QuestsConfiguration()
        {
            _keyToData = new()
            {
                { EQuests.Kill10RedEnemies , new QuestConfiguration()
                {
                    Name = "First steps.",
                    Description = "You need to deal with the bad guys.",
                    GoalConfiguration = new KillsGoalConfiguration()
                    {
                        DescriptionSetter = "Kill 10 reds",
                        EnemyType = ECharacter.EnemyRed,
                        KillsCount = 10,
                    }
                }},
                { EQuests.Kill10AnyEnemies , new QuestConfiguration()
                {
                    Name = "The indiscriminate killings",
                    Description = "You need to deal with the bad guys.",
                    GoalConfiguration = new KillsGoalConfiguration()
                    {
                        DescriptionSetter = "Kill 10 any enemies",
                        EnemyType = ECharacter.Any,
                        KillsCount = 10,
                    }
                }},
                { EQuests.Spend2MinutesInGame , new QuestConfiguration()
                {
                    Name = "First minutes are best.",
                    Description = "Just spend some time with us.",
                    GoalConfiguration = new SpendTimeGoalConfiguration()
                    {
                        DescriptionSetter = "Spend two minutes in the game.",
                        TimeToSpendInSeconds = 2 * 60,
                    }
                }}
            };
        }
    }
}