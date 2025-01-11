using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Buttonsoundscript : MonoBehaviour,IPointerDownHandler
{
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = Vector3.one;
    }
    public float Scalevaluenew = 1f;
    public  void OnPointerDown(PointerEventData e)
    {
       if(Gamesoundmanager.Instance)
        {
            Gamesoundmanager.Instance.PlayButtonsound();
        }
           
       iTween.PunchScale(this.gameObject, iTween.Hash("x", Scalevaluenew, "y", Scalevaluenew, "time", 0.1f, "delay", 0f, "easetype", "easeoutback"));
        
    }
}
