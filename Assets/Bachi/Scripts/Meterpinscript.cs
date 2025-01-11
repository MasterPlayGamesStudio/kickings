using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Meterpinscript : MonoBehaviour
{

    public float Speed = 2;
    public int Multiplier = 1;
    public Transform Pinobj;
    Quaternion Pinrotation;
    public float Hitpower = 50;
    public Text Hitpowertext;
    public float Initialpower;
    public bool Stopchecking = false;
    float Zvalue = 0;

    public Gamemanager Currentgamemanager;



    public GraphicRaycaster _Raycaster;
    public EventSystem _Eventsystemref;
    PointerEventData _pointereventdata;

    float Delaytimervalue = 0;

    public Canvas Metercanvas;
    public CanvasScaler Metercanvasscaler;
    //public GraphicRaycaster Metergraphicraycaster;
    private void Awake()
    {
        Stopchecking = false;      
    }


    private void Start()
    {
        if (Gamemanager.Instance)
        {
            Currentgamemanager = Gamemanager.Instance;
        }


        _pointereventdata = new PointerEventData(_Eventsystemref);

        Delaytimervalue = 0;

        if (Database.Levelsnumber>10)
        {
         //   Delaytimervalue = Random.Range(0, 0.15f);
        }
        //Debug.Log("Delaytimervalue : " + Delaytimervalue);

        gameObject.SetActive(false);

        Metercanvas.worldCamera = UIcontrol.Obj.UIcamera;
        Metercanvas.enabled = false;
        Metercanvasscaler.enabled = false;
    }
    private void OnEnable()
    {
        Stopchecking = false;


        if (Currentgamemanager && Currentgamemanager._UIcontrolref)
        {
            Currentgamemanager._UIcontrolref.Enablepowebutton();
            Currentgamemanager._UIcontrolref.Showincreasepowerbuttons();
        }


        pinpos= Pinobj.transform.localPosition;
        pinpos.y = 80;
        Pinobj.transform.localPosition= pinpos;

        Metercanvas.enabled = true;
        Metercanvasscaler.enabled = true;

    }
    public void Getvalues()
    {
        if (Gamemanager.Instance)
        {
            Hitpower = Database.GetPlayerpowervalue;
            Initialpower = Hitpower;
        }

        Hitpowertext.text = Hitpower.ToString();
    }

    Vector3 pinpos;
    private void FixedUpdate()
    {
        if (Currentgamemanager == null || (Currentgamemanager && Currentgamemanager.Player==null) )
            return;

        if (!Stopchecking)
        {
            /*
            Pinrotation = Pinobj.transform.localRotation;
            Zvalue = Pinrotation.z;
            if (Zvalue >= 0.35f && Multiplier == 1)
            {
                Multiplier = -1;
            }
            if (Zvalue <= -0.35f && Multiplier == -1)
            {
                Multiplier = 1;
            }


            Pinobj.transform.Rotate(0, 0, Speed * Multiplier);

            Hitpower = Initialpower - (Initialpower * Mathf.Abs(Zvalue*2));
            Hitpower = Mathf.CeilToInt(Hitpower)* Currentgamemanager.Player.Powerattackvalue;
            Hitpowertext.text = Database.GetPlayerpowervalue.ToString();
            */


            pinpos = Pinobj.transform.localPosition;
            pinpos.y += Multiplier*7.5f;
            if(pinpos.y<-80)
            {
                Multiplier = 1;
            }
            if(pinpos.y>240)
            {
                Multiplier = -1;

            }

            Pinobj.transform.localPosition = pinpos;

            if(pinpos.y>80)
            {
                Hitpower = Initialpower - (Initialpower * ((pinpos.y - 80) / 320));

            }
            else
            {
                Hitpower = Initialpower - (Initialpower * ((80- pinpos.y) / 320));

            }
            Hitpower = Mathf.CeilToInt(Hitpower) * Currentgamemanager.Player.Powerattackvalue;


            Hitpowertext.text =  Database.GetPlayerpowervalue.ToString();


        }

       

       // Debug.Log("Rot " + transform.localRotation);
       
    }

    private void LateUpdate()
    {
        if (Currentgamemanager == null || (Currentgamemanager && Currentgamemanager.Player == null))
            return;


        if (Gamemanager.Stopchecking || Gamemanager.Inputrestricted)
            return;


        if (Input.GetMouseButtonDown(0))
        {

            _pointereventdata.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            _Raycaster.Raycast(_pointereventdata, results);



            for (int j = 0; j < results.Count; j++)
            {
                if (results[j].gameObject.transform.gameObject.layer == 5 || results[j].gameObject.transform.gameObject.layer == 12)
                {
                    #if UNITY_EDITOR

                      Debug.Log("Hit obj" + results[j].gameObject.transform.name);

                    #endif

                    return;
                }
            }

            if (!Stopchecking)
            {

                CancelInvoke("CheckInput");
                CancelInvoke("disableobjnow");

                Invoke("CheckInput",Delaytimervalue);               
                Invoke("disableobjnow", 1);

                CancelInvoke("Checkbonuslevelstuff");
                Invoke("Checkbonuslevelstuff", 1);
               
                
                if (Currentgamemanager && Currentgamemanager._UIcontrolref && Currentgamemanager.Isgamestarted)
                {
                    Currentgamemanager._UIcontrolref.Disableincreasebuttons();
                   
                    if(Currentgamemanager._UIcontrolref.Isbonuslevel)
                    {
                        Currentgamemanager._UIcontrolref.Enablenoofavailablekicksstrip();
                    }
                }
            }
        }
    }

    void CheckInput()
    {
        Currentgamemanager.AIplayer.Applydamagetome = Mathf.CeilToInt(Hitpower);
        if (Mathf.Abs(Zvalue) <= 0.1f)
        {
            // Debug.Log("High kick");
            Currentgamemanager.Player.Highkick();
        }
        else if (Mathf.Abs(Zvalue) > 0.1f && Mathf.Abs(Zvalue) <= 0.3f)
        {
            // Debug.Log("Mid kick");
            Currentgamemanager.Player.Midkick();

        }
        else
        {
            // Debug.Log("Low kick");
            Currentgamemanager.Player.Lowkick();

        }
        Currentgamemanager.Player.Isplayerperformedaction = true;

        if (Currentgamemanager.AIplayer.Checkprediemethod(Currentgamemanager.AIplayer.Applydamagetome))
        {
            Currentgamemanager.rotationvvalue = Currentgamemanager.Player.transform.eulerAngles.y + 90;//90
            Currentgamemanager.Checkfinishmove = true;
            Time.timeScale = 0.75f;


        }

        Hitpowertext.text = Hitpower.ToString();
        Stopchecking = true;


    }

    void disableobjnow()
    {
        gameObject.SetActive(false);

    }

    void Checkbonuslevelstuff()
    {

        if (Currentgamemanager._UIcontrolref.Isbonuslevel == false)
            return;
        Currentgamemanager._UIcontrolref.Checknoofavialblekicks--;
    }
    private void OnDisable()
    {
        Pinrotation.z = 0;
        Pinobj.transform.localRotation = Pinrotation;          
        Stopchecking = false;

        Metercanvas.enabled = false;
        Metercanvasscaler.enabled = false;

        // Debug.Log("exist in this button...");

    }
}
