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
        Vector3 originalPosition = go.transform.position;
        while(duration < animationTime)
        {
            Vector3 pos = go.transform.position;
            pos.y += curve.Evaluate(duration / animationTime);
            go.transform.position = pos;
            duration += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        go.transform.position = originalPosition;
        isAnimating = false;
    }
}
