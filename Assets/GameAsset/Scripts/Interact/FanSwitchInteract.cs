using UnityEngine;

public class FanSwitchInteract : MonoBehaviour, IInteractable
{
    public CeilingFanController fanController;

    [Header("Cài đặt công tắc")]
    public int levelCount = 5;

    public float degreesPerLevel = -60f;

    [Header("Tốc độ vặn")]
    public float rotateSpeed = 5f;

    private int currentLevel = 0;
    private Quaternion targetRotation;
    private Vector3 baseEuler;

    void Start()
    {
        baseEuler = transform.localEulerAngles;
        targetRotation = transform.localRotation;
    }

    void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * rotateSpeed);
    }

    public void Interact()
    {
        currentLevel = (currentLevel + 1) % levelCount;

        float xRotation = degreesPerLevel * currentLevel;
        targetRotation = Quaternion.Euler(baseEuler.x + xRotation, baseEuler.y, baseEuler.z);

        if (fanController != null)
        {
            fanController.SetLevel(currentLevel);
        }
    }
}