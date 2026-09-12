using UnityEngine;

public class PlayerSit : MonoBehaviour
{
    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Giới hạn góc nhìn khi đang ngồi")]
    public float yawClamp = 50f;     
    public float pitchMin = -30f;     
    public float pitchMax = 30f;      
    public float lookSensitivity = 2f;

    [Header("Phím đứng dậy")]
    public KeyCode standUpKey = KeyCode.Q;

    private PlayerMovement playerMovement;
    private CharacterController characterController;
    private bool isSitting = false;
    private bool isUsingComputer = false;

    public bool IsUsingComputer => isUsingComputer;

    private float currentYaw;
    private float currentPitch;

    private Vector3 standPosition;
    private Quaternion standRotation;

    public bool IsSitting => isSitting;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!isSitting) return;
        if (ComputerUIController.IsOpen) return; 

        HandleSittingLook();

        if (Input.GetKeyDown(standUpKey))
        {
            StandUp();
        }
    }
    public void SitAt(Transform sitPoint, Transform standPoint)
    {
        if (isSitting) return;

        isSitting = true;

        if (standPoint != null)
        {
            standPosition = standPoint.position;
            standRotation = standPoint.rotation;
        }
        else
        {
            standPosition = transform.position;
            standRotation = transform.rotation;
        }

        if (characterController != null)
            characterController.enabled = false;

        transform.position = sitPoint.position;
        transform.rotation = sitPoint.rotation;

        if (characterController != null)
            characterController.enabled = true;

        currentYaw = 0f;
        currentPitch = 0f;

        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.identity;

        if (playerMovement != null)
            playerMovement.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void HandleSittingLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        currentYaw = Mathf.Clamp(currentYaw + mouseX, -yawClamp, yawClamp);
        currentPitch = Mathf.Clamp(currentPitch - mouseY, pitchMin, pitchMax);

        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }

    void StandUp()
    {
        isSitting = false;

        if (characterController != null)
            characterController.enabled = false;

        transform.position = standPosition;
        transform.rotation = standRotation;

        if (characterController != null)
            characterController.enabled = true;

        if (cameraTransform != null)
            cameraTransform.localRotation = Quaternion.identity;

        if (playerMovement != null)
            playerMovement.enabled = true;
    }
}