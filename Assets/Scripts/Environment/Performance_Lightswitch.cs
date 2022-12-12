using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Performance_Lightswitch : MonoBehaviour
{
    public List<GameObject> LightsOn;
    public List<GameObject> LightsOff;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            foreach(GameObject light in LightsOn)
            {
                light.SetActive(true);
            }

            foreach (GameObject light in LightsOff)
            {
                light.SetActive(false);
            }
        }
    }
}
