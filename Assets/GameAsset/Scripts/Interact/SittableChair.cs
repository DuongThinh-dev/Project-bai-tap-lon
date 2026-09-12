using UnityEngine;

public class SittableChair : MonoBehaviour, IInteractable
{
    [Header("Điểm ngồi")]
    public Transform sitPoint;

    [Header("Điểm đứng dậy")]
    public Transform standPoint;

    public PlayerSit playerSit;

    public void Interact()
    {
        if (playerSit == null || sitPoint == null)
        {
            return;
        }

        playerSit.SitAt(sitPoint, standPoint);
    }
}