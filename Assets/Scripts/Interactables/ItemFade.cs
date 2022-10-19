using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFade : MonoBehaviour
{
    MeshRenderer rend;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine("FadeOut");
        Debug.Log("DHdwhu");
    }
}
