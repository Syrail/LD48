using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Shake : MonoBehaviour
{

    float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;


    public IEnumerator StartShake(float duration)
    {
        shakeDuration = duration;
        Vector3 originalPos = transform.localPosition;
        while (shakeDuration > 0.0f)
        {
            Variables.Scene(gameObject).Set("CameraShake", Random.insideUnitSphere * shakeAmount);
            shakeDuration -= Time.fixedDeltaTime * decreaseFactor;
            yield return new WaitForFixedUpdate();
        }

        shakeDuration = 0f;
        //transform.localPosition = originalPos;
        Variables.Scene(gameObject).Set("CameraShake", Vector3.zero);
    }
}
