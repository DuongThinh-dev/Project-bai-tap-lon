using UnityEngine;

public class LampSwitchInteract : MonoBehaviour, IInteractable
{
    public Light lampLight;

    public bool startOn = false;

    private bool isOn;

    void Start()
    {
        isOn = startOn;
        ApplyLightState();
    }

    public void Interact()
    {
        isOn = !isOn;
        ApplyLightState();
    }

    void ApplyLightState()
    {
        if (lampLight != null)
        {
            lampLight.enabled = isOn;
        }
    }
}