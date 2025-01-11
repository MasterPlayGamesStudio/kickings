using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatescript : MonoBehaviour
{
    // Start is called before the first frame update
    public float Xvalue, Yvalue, Zvalue;
   
    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(Xvalue, Yvalue, Zvalue);

    }
}
