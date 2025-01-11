using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enableobjectscript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Allobjects;
    private void Awake()
    {
        for (int i = 0; i < Allobjects.Length; i++)
            Allobjects[i].SetActive(true);
    }
}
