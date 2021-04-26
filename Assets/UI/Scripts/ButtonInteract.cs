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
        isAnimating = true;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        
        GameObject go = goReference != null ? goReference : gameObject;
        float duration = 0.0f;
        Vector3 originalPosition = go.transform.localPosition;
        while(duration < animationTime)
        {
            float value = curve.Evaluate(duration / animationTime);
            Vector3 localUp = go.transform.InverseTransformPoint(go.transform.up).normalized;
            Vector3 pos = go.transform.localPosition + (localUp * value);
            go.transform.localPosition = pos;
            duration += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        go.transform.localPosition = originalPosition;
        isAnimating = false;
    }
}
