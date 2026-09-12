using UnityEngine;
public interface IPickupable
{
    void OnPickup();
    void OnDrop();
    bool CanDropAt(Transform hitTransform);
}