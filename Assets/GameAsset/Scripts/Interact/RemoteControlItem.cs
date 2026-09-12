using UnityEngine;

public class RemoteControlItem : MonoBehaviour, IPickupable
{
    [Header("UI icon hiển thị khi đang cầm")]
    public GameObject remoteUIIcon;

    public Transform dropZone;

    public static bool IsHeld { get; private set; } = false;

    private Renderer[] renderers;
    private Collider myCollider;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        myCollider = GetComponent<Collider>();

        if (remoteUIIcon != null)
            remoteUIIcon.SetActive(false);
    }

    public void OnPickup()
    {
        IsHeld = true;

        SetVisible(false);

        if (myCollider != null)
            myCollider.enabled = false;

        if (remoteUIIcon != null)
            remoteUIIcon.SetActive(true);
    }

    public void OnDrop()
    {
        IsHeld = false;

        SetVisible(true);

        if (myCollider != null)
            myCollider.enabled = true;

        if (remoteUIIcon != null)
            remoteUIIcon.SetActive(false);
    }

    void SetVisible(bool visible)
    {
        foreach (Renderer r in renderers)
        {
            if (r != null)
                r.enabled = visible;
        }
    }

    public bool CanDropAt(Transform hitTransform)
    {
        if (dropZone == null || hitTransform == null) return false;
        return hitTransform == dropZone || hitTransform.IsChildOf(dropZone);
    }
}