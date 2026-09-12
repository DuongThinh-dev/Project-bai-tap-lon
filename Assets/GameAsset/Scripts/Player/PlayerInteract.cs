using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerAim playerAim;

    public PlayerSit playerSit;

    [Header("Phím tương tác")]
    public KeyCode interactKey = KeyCode.E;
    public KeyCode secondaryKey = KeyCode.R;
    public KeyCode useKey = KeyCode.F;

    void Update()
    {
        if (playerAim == null) return;
        if (ComputerUIController.IsOpen) return; 

        if (Input.GetKeyDown(interactKey) && playerAim.CurrentInteractable != null)
        {
            playerAim.CurrentInteractable.Interact();
        }

        if (Input.GetKeyDown(secondaryKey) &&
            playerAim.CurrentInteractable is ISecondaryInteractable secondary)
        {
            secondary.SecondaryInteract();
        }

        if (Input.GetKeyDown(useKey) &&
            playerAim.CurrentInteractable is IUsable usable && usable.CanUse &&
            playerSit != null && playerSit.IsSitting)
        {
            usable.Use();
        }
    }
}