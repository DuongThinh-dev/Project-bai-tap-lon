using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Camera & Raycast")]
    public Camera playerCamera;
    public float pickupRange = 3f;
    public LayerMask hitMask = ~0;

    [Header("UI gợi ý")]
    public GameObject pickupPrompt;      
    public GameObject dropPrompt;        

    private IPickupable heldItem;
    private IPickupable lookingAtPickupable;
    private Transform currentHitTransform;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (pickupPrompt != null) pickupPrompt.SetActive(false);
        if (dropPrompt != null) dropPrompt.SetActive(false);
    }

    void Update()
    {
        DetectRaycastTarget();

        if (Input.GetKeyDown(KeyCode.F))
        {
            HandleFPressed();
        }
    }

    void DetectRaycastTarget()
    {
        lookingAtPickupable = null;
        currentHitTransform = null;

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        bool didHit = Physics.Raycast(ray, out RaycastHit hit, pickupRange, hitMask);

        if (didHit)
            currentHitTransform = hit.transform;

        if (heldItem != null)
        {
            bool canDrop = didHit && heldItem.CanDropAt(currentHitTransform);

            if (dropPrompt != null) dropPrompt.SetActive(canDrop);
            if (pickupPrompt != null) pickupPrompt.SetActive(false);
            return;
        }

        if (didHit)
        {
            Transform check = currentHitTransform;
            while (check != null)
            {
                IPickupable pickupable = check.GetComponent<IPickupable>();
                if (pickupable != null)
                {
                    lookingAtPickupable = pickupable;
                    break;
                }
                check = check.parent;
            }
        }

        if (pickupPrompt != null) pickupPrompt.SetActive(lookingAtPickupable != null);
        if (dropPrompt != null) dropPrompt.SetActive(false);
    }

    void HandleFPressed()
    {
        if (heldItem != null)
        {
            if (currentHitTransform != null && heldItem.CanDropAt(currentHitTransform))
            {
                heldItem.OnDrop();
                heldItem = null;
            }
        }
        else if (lookingAtPickupable != null)
        {
            heldItem = lookingAtPickupable;
            heldItem.OnPickup();
        }
    }
}