using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TempBellaDeleteMe : MonoBehaviour
{
    public float Max = 40000;
    public float Min = 0;
    public float Number;

    public float t = 0.0f ;
    public float VarNumb = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Number = Max;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    Number = Mathf.Lerp(Max, Min, t);
    //}


    void Update()
    {
        // animate the position of the game object...
        Number = Mathf.Lerp(Max, Min, t);

        // .. and increase the t interpolater
        t += VarNumb * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the opposite direction.
        if (t > 1.0f)
        {
            float temp = Min;
            Min = Max;
            Max = temp;
            t = 0.0f;
        }
    }
}
