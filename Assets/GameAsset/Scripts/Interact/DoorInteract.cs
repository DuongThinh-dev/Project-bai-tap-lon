using UnityEngine;
using System.Collections;

public class DoorInteract : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    public float openAngle = 90f;

    public Vector3 rotationAxis = Vector3.up;

    public bool invertDirection = false;

    [Header("Opening/Closing Speed")]
    public float speed = 2f; 

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        closedRotation = transform.localRotation;

        float angle = invertDirection ? -openAngle : openAngle;
        openRotation = closedRotation * Quaternion.AngleAxis(angle, rotationAxis);
    }
    public void Interact()
    {
        ToggleDoor();
    }
    public void ToggleDoor()
    {
        if (isMoving) return;

        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        isMoving = true;

        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.5f)
        {
            transform.localRotation = Quaternion.Slerp(
                transform.localRotation,
                targetRotation,
                Time.deltaTime * speed
            );
            yield return null;
        }

        transform.localRotation = targetRotation;
        isMoving = false;
    }
}