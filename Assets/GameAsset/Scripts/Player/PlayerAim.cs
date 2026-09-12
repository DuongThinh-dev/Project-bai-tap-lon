using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("Camera")]
    public Camera playerCamera;

    [Header("Raycast Settings")]
    public float interactRange = 3f;
    public LayerMask hitMask = ~0;

    [Header("UI gợi ý tương tác")]
    public GameObject interactPrompt;

    [Header("UI dấu tâm")]
    public GameObject crosshair;

    public GameObject secondaryPrompt;

    public GameObject usePrompt;

    public PlayerSit playerSit;

    public IInteractable CurrentInteractable { get; private set; }

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (interactPrompt != null)
            interactPrompt.SetActive(false);
    }

    void Update()
    {
        if (ComputerUIController.IsOpen)
        {
            if (crosshair != null) crosshair.SetActive(false);
            if (interactPrompt != null) interactPrompt.SetActive(false);
            if (secondaryPrompt != null) secondaryPrompt.SetActive(false);
            if (usePrompt != null) usePrompt.SetActive(false);
            return;
        }

        HandleAimDetection();
    }

    void HandleAimDetection()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        IInteractable foundInteractable = null;

        if (Physics.Raycast(ray, out hit, interactRange, hitMask))
        {
            Transform checkTarget = hit.transform;
            while (checkTarget != null)
            {
                IInteractable interactable = checkTarget.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    foundInteractable = interactable;
                    break;
                }
                checkTarget = checkTarget.parent;
            }
        }

        CurrentInteractable = foundInteractable;

        bool isLookingAtInteractable = CurrentInteractable != null;

        if (interactPrompt != null)
            interactPrompt.SetActive(isLookingAtInteractable);

        if (crosshair != null)
            crosshair.SetActive(!isLookingAtInteractable);

        if (secondaryPrompt != null)
            secondaryPrompt.SetActive(CurrentInteractable is ISecondaryInteractable);

        if (usePrompt != null)
        {
            bool isSitting = playerSit != null && playerSit.IsSitting;
            usePrompt.SetActive(CurrentInteractable is IUsable usable && usable.CanUse && isSitting);
        }
    }
}