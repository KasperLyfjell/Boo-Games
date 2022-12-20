using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Performance_Lightswitch : MonoBehaviour
{
    public List<GameObject> LightsOn;
    public List<GameObject> LightsOff;

    private void Start()
    {
        foreach (GameObject light in LightsOn)
        {
            light.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            foreach(GameObject light in LightsOn)
            {
                if(light.activeSelf == false)
                    light.SetActive(true);
            }

            foreach (GameObject light in LightsOff)
            {
                if(light.activeSelf == true)
                    light.SetActive(false);
            }
        }
    }
}
