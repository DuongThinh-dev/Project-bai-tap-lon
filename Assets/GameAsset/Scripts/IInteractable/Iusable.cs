public interface IUsable : IInteractable
{
    bool CanUse { get; }  
    void Use();
}