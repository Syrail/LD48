using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour
{
    public AnimationCurve curve = null;
    public float animationTime = 0.5f;
    public GameObject goReference = null;

    bool isAnimating = false;

    public void StartAnimation()
    {
        if (!isAnimating)
        {
            StartCoroutine(Animate());
        }
    }

    IEnumerator Animate()
    {
        isAnimating = true;
        GameObject go = goReference != null ? goReference : gameObject;
        float duration = 0.0f;
        Vector3 originalPosition = go.transform.localPosition;
        Vector3 localUp = go.transform.up;
        while (duration < animationTime)
        {
            float value = curve.Evaluate(duration / animationTime);            
            Vector3 pos = originalPosition + (localUp * value * Time.fixedDeltaTime);
            go.transform.localPosition = pos;
            duration += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        go.transform.localPosition = originalPosition;
        isAnimating = false;
    }
}
