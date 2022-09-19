using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternColorChange : MonoBehaviour
{
    public Animator flameColor;
    public RuntimeAnimatorController flameBlue;
    public RuntimeAnimatorController flameGreen;
    public Light flameLight;

    public ParticleSystem psBlue;
    public ParticleSystem psGreen;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void LanternBlue()
    {
            flameColor.runtimeAnimatorController = flameBlue;
            flameLight.color = Color.blue;
        psBlue.Play();
    }

    public void LanternGreen()
    {
        flameColor.runtimeAnimatorController = flameGreen;
        flameLight.color = Color.green;
        psGreen.Play();
    }
}
