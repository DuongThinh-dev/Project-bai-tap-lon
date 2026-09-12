using UnityEngine;
using UnityEngine.UI;

public class PartPictureController : MonoBehaviour
{
    public static bool IsOpen { get; private set; } = false;

    [Header("Panel UI_PartPicture")]
    public GameObject uiPartPicture;

    public Image pictureDisplay;

    public Sprite[] pictures = new Sprite[5];

    private int currentIndex = 0;

    void Start()
    {
        if (uiPartPicture != null)
            uiPartPicture.SetActive(false);

        IsOpen = false;
    }

    public void ShowPartPicture(int index)
    {
        if (pictures == null || pictures.Length == 0) return;

        currentIndex = Mathf.Clamp(index, 0, pictures.Length - 1);

        if (uiPartPicture != null)
            uiPartPicture.SetActive(true);

        IsOpen = true;

        UpdatePictureDisplay();
    }

    public void NextPicture()
    {
        if (pictures == null || pictures.Length == 0) return;

        currentIndex = (currentIndex + 1) % pictures.Length; 
        UpdatePictureDisplay();
    }

    public void PreviousPicture()
    {
        if (pictures == null || pictures.Length == 0) return;

        currentIndex = (currentIndex - 1 + pictures.Length) % pictures.Length; 
        UpdatePictureDisplay();
    }

    public void ClosePartPicture()
    {
        if (uiPartPicture != null)
            uiPartPicture.SetActive(false);

        IsOpen = false;
    }

    void UpdatePictureDisplay()
    {
        if (pictureDisplay != null && currentIndex >= 0 && currentIndex < pictures.Length)
        {
            pictureDisplay.sprite = pictures[currentIndex];
        }
    }
}