using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public PlayerAim playerAim;

    [Header("Phím tương tác")]
    public KeyCode interactKey = KeyCode.E;
    public KeyCode secondaryKey = KeyCode.R;

    void Update()
    {
        if (playerAim == null) return;

        if (Input.GetKeyDown(interactKey) && playerAim.CurrentInteractable != null)
        {
            playerAim.CurrentInteractable.Interact();
        }

        if (Input.GetKeyDown(secondaryKey) &&
            playerAim.CurrentInteractable is ISecondaryInteractable secondary)
        {
            secondary.SecondaryInteract();
        }
    }
}