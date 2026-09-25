using UnityEngine;
using System.Collections;

public class StairLightTrigger : MonoBehaviour
{
    public Light stairLight;

    public float turnOffDelay = 10f;

    public string requiredTag = "Player";

    private Coroutine turnOffCoroutine;

    void Start()
    {
        if (stairLight != null)
            stairLight.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(requiredTag) && !other.CompareTag(requiredTag)) return;

        if (turnOffCoroutine != null)
        {
            StopCoroutine(turnOffCoroutine);
            turnOffCoroutine = null;
        }

        if (stairLight != null)
            stairLight.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(requiredTag) && !other.CompareTag(requiredTag)) return;

        if (turnOffCoroutine != null)
            StopCoroutine(turnOffCoroutine);

        turnOffCoroutine = StartCoroutine(TurnOffAfterDelay());
    }

    IEnumerator TurnOffAfterDelay()
    {
        yield return new WaitForSeconds(turnOffDelay);

        if (stairLight != null)
            stairLight.enabled = false;

        turnOffCoroutine = null;
    }
}