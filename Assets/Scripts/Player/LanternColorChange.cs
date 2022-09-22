using UnityEngine;

public class LanternColorChange : MonoBehaviour
{
    public Animator flameColor;
    public RuntimeAnimatorController flameBlue;
    public RuntimeAnimatorController flameGreen;
    public Light flameLight;

    public ParticleSystem psBlue;
    public ParticleSystem psGreen;

    public LanternWheelController lwCon;
    bool playedBlue;
    bool playedGreen;

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

        if (playedBlue == false)
        {
            playedBlue = true;
            lwCon.blueCollected = true;
            lwCon.buttonBlue.interactable = true;
            lwCon.iconBlue.SetActive(true);
        }
    }

    public void LanternGreen()
    {
        flameColor.runtimeAnimatorController = flameGreen;
        flameLight.color = Color.green;
        psGreen.Play();

        if (playedGreen == false)
        {
            playedGreen = true;
            lwCon.greenCollected = true;
            lwCon.buttonGreen.interactable = true;
            lwCon.iconGreen.SetActive(true);
        }
    }
}
