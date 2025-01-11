using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watchvideocallback : MonoBehaviour
{
    // Start is called before the first frame update

    private static Watchvideocallback _instance;
    public static Watchvideocallback Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake() => _instance = this;


    public delegate void Videowatched();
    public event Videowatched Watchedevent;

    public bool Isvideowatched;
   
    void LateUpdate()
    {
        if(Isvideowatched)
        {
            Isvideowatched = false;
            if(Watchedevent!=null)
            Watchedevent.Invoke();
            Debug.Log("No fo times");
        }
        
    }


}
