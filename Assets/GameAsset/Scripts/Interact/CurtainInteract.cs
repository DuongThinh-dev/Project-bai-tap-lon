using UnityEngine;
using System.Collections;

public class CurtainInteract : MonoBehaviour, IInteractable
{
    [Header("Trạng thái ĐÓNG")]
    public Vector3 closedLocalPosition;
    public Vector3 closedLocalScale;

    [Header("Trạng thái MỞ")]
    public Vector3 openLocalPosition;
    public Vector3 openLocalScale;

    public bool startOpen = false;

    [Header("Tốc độ kéo")]
    public float speed = 2f;

    private bool isOpen;
    private bool isMoving;

    void Start()
    {
        isOpen = startOpen;

        transform.localPosition = isOpen ? openLocalPosition : closedLocalPosition;
        transform.localScale = isOpen ? openLocalScale : closedLocalScale;
    }

    public void Interact()
    {
        if (isMoving) return;

        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(MoveCurtain(
            isOpen ? openLocalPosition : closedLocalPosition,
            isOpen ? openLocalScale : closedLocalScale
        ));
    }

    IEnumerator MoveCurtain(Vector3 targetPosition, Vector3 targetScale)
    {
        isMoving = true;

        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.001f ||
               Vector3.Distance(transform.localScale, targetScale) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * speed);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
            yield return null;
        }

        transform.localPosition = targetPosition;
        transform.localScale = targetScale;
        isMoving = false;
    }
}