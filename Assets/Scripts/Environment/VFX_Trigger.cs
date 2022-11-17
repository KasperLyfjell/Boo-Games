using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX_Trigger : MonoBehaviour
{
    private bool HasActivated;
    public VisualEffect Target;

    private void OnTriggerEnter(Collider other)
    {
        if (!HasActivated && other.gameObject.name == "Player")
        {
            Target.Play();
            HasActivated = true;
        }
    }
}
