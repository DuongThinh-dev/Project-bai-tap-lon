using UnityEngine;

public class ComputerUIController : MonoBehaviour
{
    public static bool IsOpen { get; private set; } = false;

    [Header("UI gốc của màn hình PC")]
    public GameObject uiPC;

    [Header("Kéo object UI_PC")]
    public UIPictureController uiPictureController;

    [Header("Kéo object UI_PartPicture")]
    public PartPictureController partPictureController;

    void Start()
    {
        if (uiPC != null)
            uiPC.SetActive(false);
    }

    void Update()
    {
        if (!IsOpen) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PartPictureController.IsOpen)
            {
                if (partPictureController != null)
                    partPictureController.ClosePartPicture();
            }
            else if (UIPictureController.IsPictureOpen)
            {
                if (uiPictureController != null)
                    uiPictureController.HidePicture();
            }
            else
            {
                CloseComputer();
            }
        }
    }
    public void OpenComputer()
    {
        IsOpen = true;

        if (uiPC != null)
            uiPC.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CloseComputer()
    {
        IsOpen = false;

        if (uiPC != null)
            uiPC.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}