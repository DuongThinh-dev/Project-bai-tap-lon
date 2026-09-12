using UnityEngine;

public class PCInteract : MonoBehaviour, IUsable
{
    [Header("Màn hình PC")]
    public Renderer screenRenderer;
    public int screenMaterialIndex = 1;
    public Material screenOffMaterial;
    public Material screenOnMaterial;

    [Header("Trạng thái ban đầu")]
    public bool startScreenOn = false;

    public ComputerUIController computerUI;

    private bool isScreenOn;

    public bool CanUse => isScreenOn;

    void Start()
    {
        isScreenOn = startScreenOn;
        ApplyScreenState();
    }
    public void Interact()
    {
        isScreenOn = !isScreenOn;
        ApplyScreenState();
    }
    public void Use()
    {
        if (computerUI != null)
            computerUI.OpenComputer();
    }

    void ApplyScreenState()
    {
        if (screenRenderer == null) return;

        Material[] mats = screenRenderer.materials;
        if (screenMaterialIndex >= 0 && screenMaterialIndex < mats.Length)
        {
            mats[screenMaterialIndex] = isScreenOn ? screenOnMaterial : screenOffMaterial;
            screenRenderer.materials = mats;
        }
    }
}