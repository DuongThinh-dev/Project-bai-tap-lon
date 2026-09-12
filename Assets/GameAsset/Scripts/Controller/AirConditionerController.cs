using UnityEngine;
using System.Collections;

public class AirConditionerController : MonoBehaviour
{
    [Header("Cánh gió")]
    public Transform canhGio;
    public float openAngle = 40f; 
    public Vector3 hingeAxis = Vector3.right; 
    public float rotateSpeed = 3f;

    [Header("Đèn báo điều hòa")]
    public GameObject[] denBao;

    public bool IsOn { get; private set; } = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isMoving = false;

    void Start()
    {
        if (canhGio != null)
        {
            closedRotation = canhGio.localRotation;
            openRotation = closedRotation * Quaternion.AngleAxis(openAngle, hingeAxis);
        }

        SetDenBao(false);
    }

    public void ToggleAirConditioner()
    {
        if (isMoving) return;
        IsOn = !IsOn;

        SetDenBao(IsOn);

        if (canhGio != null)
        {
            StopAllCoroutines();
            StartCoroutine(RotateCanhGio(IsOn ? openRotation : closedRotation));
        }
    }
    void SetDenBao(bool active)
    {
        if (denBao == null) return;
        foreach (GameObject den in denBao)
        {
            if (den != null)
                den.SetActive(active);
        }
    }

    IEnumerator RotateCanhGio(Quaternion targetRotation)
    {
        isMoving = true;

        while (Quaternion.Angle(canhGio.localRotation, targetRotation) > 0.1f)
        {
            canhGio.localRotation = Quaternion.Slerp(canhGio.localRotation, targetRotation, Time.deltaTime * rotateSpeed);
            yield return null;
        }

        canhGio.localRotation = targetRotation;
        isMoving = false;
    }
}