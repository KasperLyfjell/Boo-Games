using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public void PickUpKey()
    {
        Destroy(this.gameObject);
    }
}
