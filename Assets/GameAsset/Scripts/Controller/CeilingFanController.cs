using UnityEngine;

public class CeilingFanController : MonoBehaviour
{
    [Header("Tốc độ quay")]
    public float[] speedLevels = { 0f, 5000f, 2000f, 1500f, 1000f, 600f };

    [Header("Làm mượt tăng/giảm tốc")]
    public float acceleration = 2000f;

    private float currentSpeed = 0f;
    private float targetSpeed = 0f;

    public int CurrentLevel { get; private set; } = 0;

    void Update()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        if (currentSpeed != 0f)
        {
            transform.Rotate(Vector3.up, currentSpeed * Time.deltaTime, Space.Self);
        }
    }

    public void SetLevel(int level)
    {
        level = Mathf.Clamp(level, 0, speedLevels.Length - 1);
        CurrentLevel = level;
        targetSpeed = speedLevels[level]; 
    }
}