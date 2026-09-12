using UnityEngine;

public class DualLightSwitchInteract : MonoBehaviour, IInteractable
{
    [Header("Đèn 1")]
    public Light light1;
    public Renderer bulbRenderer1;      // renderer của bóng đèn 1 (để đổi màu phát sáng, có thể để trống)
    public int bulbMaterialIndex1 = 0;
    public Material bulbOnMaterial1;
    public Material bulbOffMaterial1;

    [Header("Đèn 2")]
    public Light light2;
    public Renderer bulbRenderer2;
    public int bulbMaterialIndex2 = 0;
    public Material bulbOnMaterial2;
    public Material bulbOffMaterial2;

    [Header("UI prompt riêng cho từng phím")]
    public GameObject prompt1; 
    public GameObject prompt2; 

    public PlayerAim playerAim;

    private bool isOn1 = false;
    private bool isOn2 = false;

    void Start()
    {
        ApplyLight1();
        ApplyLight2();

        if (prompt1 != null) prompt1.SetActive(false);
        if (prompt2 != null) prompt2.SetActive(false);
    }
    public void Interact() { }

    void Update()
    {
        bool isAimed = playerAim != null && ReferenceEquals(playerAim.CurrentInteractable, this);

        if (prompt1 != null) prompt1.SetActive(isAimed);
        if (prompt2 != null) prompt2.SetActive(isAimed);

        if (!isAimed) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isOn1 = !isOn1;
            ApplyLight1();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isOn2 = !isOn2;
            ApplyLight2();
        }
    }

    void ApplyLight1()
    {
        if (light1 != null) light1.enabled = isOn1;
        SetBulbMaterial(bulbRenderer1, bulbMaterialIndex1, isOn1 ? bulbOnMaterial1 : bulbOffMaterial1);
    }

    void ApplyLight2()
    {
        if (light2 != null) light2.enabled = isOn2;
        SetBulbMaterial(bulbRenderer2, bulbMaterialIndex2, isOn2 ? bulbOnMaterial2 : bulbOffMaterial2);
    }

    void SetBulbMaterial(Renderer rend, int index, Material mat)
    {
        if (rend == null || mat == null) return;

        Material[] mats = rend.materials;
        if (index >= 0 && index < mats.Length)
        {
            mats[index] = mat;
            rend.materials = mats;
        }
    }
}