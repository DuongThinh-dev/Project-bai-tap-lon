using UnityEngine;

public class UIPictureController : MonoBehaviour
{
    public static bool IsPictureOpen { get; private set; } = false;

    [Header("Panel UI_Picture ")]
    public GameObject uiPicture;

    void Start()
    {
        if (uiPicture != null)
            uiPicture.SetActive(false);

        IsPictureOpen = false;
    }
    public void ShowPicture()
    {
        if (uiPicture != null)
            uiPicture.SetActive(true);

        IsPictureOpen = true;
    }
    public void HidePicture()
    {
        if (uiPicture != null)
            uiPicture.SetActive(false);

        IsPictureOpen = false;
    }
}