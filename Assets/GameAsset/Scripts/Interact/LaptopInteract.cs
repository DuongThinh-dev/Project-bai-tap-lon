using UnityEngine;
using System.Collections;

public class LaptopInteract : MonoBehaviour, ISecondaryInteractable
{
    [Header("Màn hình laptop")]
    public Renderer screenRenderer;      
    public int screenMaterialIndex = 1;  
    public Material screenOffMaterial;   
    public Material screenOnMaterial;    

    [Header("Gập/mở laptop")]
    public Transform lidTransform;       
    public Vector3 closedLocalEuler;     
    public Vector3 openLocalEuler;       
    public float lidSpeed = 3f;

    [Header("Trạng thái ban đầu")]
    public bool startScreenOn = false;
    public bool startLidOpen = true;

    private bool isScreenOn;
    private bool isLidOpen;
    private bool isLidMoving;

    void Start()
    {
        isScreenOn = startScreenOn;
        isLidOpen = startLidOpen;

        ApplyScreenState();

        if (lidTransform != null)
            lidTransform.localRotation = Quaternion.Euler(isLidOpen ? openLocalEuler : closedLocalEuler);
    }

    public void Interact()
    {
        if(!isLidOpen) return;

        isScreenOn = !isScreenOn;
        ApplyScreenState();
    }

    public void SecondaryInteract()
    {
        if (isLidMoving || lidTransform == null) return;

        isLidOpen = !isLidOpen;

        if (!isLidOpen)
        {
            isScreenOn = false;
            ApplyScreenState();
        }

        StopAllCoroutines();
        StartCoroutine(RotateLid(isLidOpen ? openLocalEuler : closedLocalEuler));
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

    IEnumerator RotateLid(Vector3 targetEuler)
    {
        isLidMoving = true;
        Quaternion targetRotation = Quaternion.Euler(targetEuler);

        while (Quaternion.Angle(lidTransform.localRotation, targetRotation) > 0.5f)
        {
            lidTransform.localRotation = Quaternion.Slerp(lidTransform.localRotation, targetRotation, Time.deltaTime * lidSpeed);
            yield return null;
        }

        lidTransform.localRotation = targetRotation;
        isLidMoving = false;
    }
}