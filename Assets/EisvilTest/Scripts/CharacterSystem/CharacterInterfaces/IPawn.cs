namespace EisvilTest.Scripts.CharacterSystem
{
    public interface IPawn : IMovable
    {
        IInteractable Interactable { get; }
        bool IsInteractionAvailable { get; }
    }
}