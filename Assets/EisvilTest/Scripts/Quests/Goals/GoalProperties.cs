namespace EisvilTest.Scripts.Quests.Goals
{
    public class GoalProperties
    {
        public IObservableValueReadOnly<string> Description { get; }
        public IObservableValueReadOnly<bool> GoalAchieved { get; }

        public GoalProperties(GoalPropertiesSetter setter)
        {
            Description  = setter.Description;
            GoalAchieved = setter.GoalAchieved;
        }
    }
    
    public class GoalPropertiesSetter
    {
        public ObservableValue<string> Description { get; } = new ();
        public ObservableValue<bool> GoalAchieved { get; } = new ();
    }
}