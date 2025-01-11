using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelmanager : MonoBehaviour
{
   

    private void Awake()
    {
        
        Vector3 Angles = transform.eulerAngles;
        Angles.y = Random.Range(0, 360);
        transform.eulerAngles = Angles;
    }

   

}
