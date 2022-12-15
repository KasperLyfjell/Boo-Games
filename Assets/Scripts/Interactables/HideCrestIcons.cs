using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCrestIcons : MonoBehaviour
{
    public GameObject crossIcon;
    public GameObject horseIcon;
    public GameObject bottleIcon;
    public GameObject magnifierIcon;
    public GameObject heartIcon;


    // Start is called before the first frame update
    void Start()
    {
        crossIcon.SetActive(false);
        horseIcon.SetActive(false);
        bottleIcon.SetActive(false);
        magnifierIcon.SetActive(false);
        heartIcon.SetActive(false);
    }
}
