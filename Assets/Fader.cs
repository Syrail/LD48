using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public float fadeTime;
    private float totalTime;

    private void Awake()
    {
        totalTime = 0f;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        for(totalTime = 0; totalTime < fadeTime; totalTime += Time.deltaTime)
        {
            Material mat = GetComponentInChildren<TMPro.TextMeshPro>().material;
            float alpha = Mathf.Lerp(0f, 1f, totalTime / fadeTime);
            mat.SetColor("_EmitColor", new Color (mat.color.r, mat.color.g, mat.color.g, alpha));
            SpriteRenderer spr = GetComponentInChildren<SpriteRenderer>();
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, alpha);
            yield return null;
        }
    }
}
