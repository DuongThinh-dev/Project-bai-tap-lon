using UnityEngine;
using System.Collections;

public class DeskDrawerInteract : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    public Vector3 closedLocalPosition;

    [Tooltip("Khoảng cách kéo ra theo trục Z")]
    public float openDistance = 0.5f;

    [Tooltip("Đảo chiều kéo")]
    public bool invertDirection = false;

    [Header("Opening/Closing Speed")]
    public float speed = 2f;

    private Vector3 openLocalPosition;
    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        closedLocalPosition = transform.localPosition;
        Vector3 dir = invertDirection ? Vector3.back : Vector3.forward;
        openLocalPosition = closedLocalPosition + dir * openDistance;
    }

    public void Interact()
    {
        ToggleDeskDrawer();
    }

    public void ToggleDeskDrawer()
    {
        if (isMoving) return;
        isOpen = !isOpen;

        StopAllCoroutines();
        StartCoroutine(MoveDeskDrawer(isOpen ? openLocalPosition : closedLocalPosition));
    }

    IEnumerator MoveDeskDrawer(Vector3 targetPosition)
    {
        isMoving = true;

        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                targetPosition,
                Time.deltaTime * speed
            );
            yield return null;
        }

        transform.localPosition = targetPosition;
        isMoving = false;
    }
}