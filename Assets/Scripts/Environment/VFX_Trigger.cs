using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX_Trigger : MonoBehaviour
{
    private bool HasActivated;
    public VisualEffect Target;
    public Env_AmbLightning1 lightning;

    private void OnTriggerEnter(Collider other)
    {
        if (!HasActivated && other.gameObject.name == "Player")
        {
            Target.Play();
            lightning.activateStrike();
            HasActivated = true;
        }
    }
}
