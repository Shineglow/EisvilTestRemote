namespace EisvilTest.Scripts.CharacterSystem
{
    public interface IPawnSetter : IPawn
    {
        new IInteractable Interactable { get; set; }
        new bool IsInteractionAvailable { get; set; }
    }
}