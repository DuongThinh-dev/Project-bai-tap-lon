using UnityEngine;

public class AirConditionerInteract : MonoBehaviour
{
    [Header("Điều hoà")]
    public AirConditionerController controller;
    public bool requireRemote = true;

    public float remoteRange = 8f;
    public PlayerAim playerAim; 
    public KeyCode remoteToggleKey = KeyCode.E;

    void Update()
    {
        if (!requireRemote || !RemoteControlItem.IsHeld) return;
        if (playerAim == null || playerAim.playerCamera == null) return;

        bool isAiming = IsAimingAtThis();

        if (isAiming && playerAim.interactPrompt != null)
            playerAim.interactPrompt.SetActive(true);

        if (isAiming && Input.GetKeyDown(remoteToggleKey))
        {
            if (controller != null)
                controller.ToggleAirConditioner();
        }
    }

    bool IsAimingAtThis()
    {
        Ray ray = playerAim.playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, remoteRange))
        {
            Transform check = hit.transform;
            while (check != null)
            {
                if (check == transform) return true;
                check = check.parent;
            }
        }

        return false;
    }
}