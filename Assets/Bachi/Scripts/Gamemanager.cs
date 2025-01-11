using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public Basescript Player, AIplayer;

   

    public GameObject Camerapoint;

    public UIcontrol _UIcontrolref;
    public GameObject Maincameraobj;


    public static bool Stopchecking=false;
    public static bool Inputrestricted=false;
    public  bool Checkfinishmove=false;
    public bool Isgamestarted;
    public float rotationvvalue = 0;

    public Animator Cameraaimator;

    public GameObject Playermeterobj;

    public GameObject Midpoint;

    public Poolmanager _poolmanagerref;
    public Vector3 Initalrotation;
    public Vector3 Initialposition;
    public Vector3 InitialMaincamerarotation;

    public Meterpinscript Playermeterscriptref;



    public float Distancevalue = 4.3f;
    public bool Dontchangecamera = false;
    public Createplayer Playercreator;
    public static int Createplayerindexvalue = -1;

    private float Playersrange = 10;

    public List<Basescript> Allplayers;

    public bool Isgamerunning;

    private static Gamemanager _instance;

    public static Gamemanager Instance
    {
        get
        {           
            return _instance;
        }
    }

    private Gamemanager()
    {
       
    }

    private void Awake()=> _instance = this;


    private void Start()
    {
        Time.timeScale = 1;
        Stopchecking = false;
        Inputrestricted = false;
        Isgamestarted = false;
        Isgamerunning = true;

        if (UIcontrol.Obj)
        {
            _UIcontrolref = UIcontrol.Obj;
        }

        if(Poolmanager.Obj)
        {
            _poolmanagerref = Poolmanager.Obj;
        }

        //Cameraaimator.enabled = false;

        //Debug.Log("Player ..." + Player + " AIplyer  " + AIplayer);

        Playerturn = AIplayerturn = false;
        Invoke("Startgamenow", 2);

        Invoke("CreateMidpoint", 0.1f);

        Currentlayermask = 1 << 11;
        Currentlayermask = ~Currentlayermask;

        Debug.Log("Level " + Database.Levelsnumber);
        Debug.Log("Character " + Database.GetAllcharacters);
        Debug.Log("Coins  " + Database.Totalcoins);


    }

    void CreateMidpoint()
    {
        Midpoint = new GameObject("Midpoint");

        Vector3 dirmidtoplayer = Player.transform.position - Midpoint.transform.position;
        Vector3 dirmidtoAIplayer = AIplayer.transform.position - Midpoint.transform.position;
        Midpoint.transform.position = new Vector3((dirmidtoplayer.x + dirmidtoAIplayer.x) / 2f, (dirmidtoplayer.y + dirmidtoAIplayer.y) / 2f, (dirmidtoplayer.z + dirmidtoAIplayer.z) / 2f);

    }

    void Startgamenow()
    {
        Playerturn = AIplayerturn = false;
        Playerturn = true;

        Isgamestarted = true;

        Initalrotation = Camerapoint.transform.localEulerAngles;
        Initialposition = Maincameraobj.transform.localPosition;
        InitialMaincamerarotation = Maincameraobj.transform.localEulerAngles;

        Playermeterscriptref.Getvalues();
       // _UIcontrolref.Incrasehealthvalue = Player.Healthvalue / 10;
       // _UIcontrolref.Increasepowervalue = Mathf.CeilToInt( Playermeterscriptref.Hitpower / 10);

    }

    Vector3 Playerpos, AIplayerpos;
    Vector3 Camerposition;

    Vector2 camdir,Cameraangles;

    Vector3 offset, offset1;

    public float distancebetweenopponent;
    float Timervalue;

    public bool Playerismoving;

    public bool Playerturn, AIplayerturn,Playerperformedaction;

    Vector3 maincampos, maincamrot;
    Vector3 Maincamerachildpos;

    private LayerMask Currentlayermask;

    float tempvalue;
    private void FixedUpdate()
    {

        if (Midpoint == null || Player == null || AIplayer == null)
            return;

            if (Checkfinishmove)
            {
                if (Cameraaimator.enabled)
                {
                    Cameraaimator.enabled = false;
                }
                tempvalue += Time.deltaTime;
                if (tempvalue > 1.5f)
                {
                    Checkfinishmove = false;
                    Time.timeScale = 1;
                }


                Vector3 localangle = Maincameraobj.transform.localEulerAngles;
                localangle.x = 0;
                Maincameraobj.transform.localRotation = Quaternion.Lerp(Maincameraobj.transform.localRotation, Quaternion.Euler(localangle), 1 * Time.deltaTime);

                Vector3 camerobjpos = Cameraaimator.transform.localPosition;
                camerobjpos.y = -5;
                Cameraaimator.transform.localPosition = Vector3.Lerp(Cameraaimator.transform.localPosition, camerobjpos, 3 * Time.deltaTime);

                Vector3 localangley = Camerapoint.transform.localEulerAngles;
                localangley.y = rotationvvalue;
                Camerapoint.transform.localRotation = Quaternion.Lerp(Camerapoint.transform.localRotation, Quaternion.Euler(localangley), 1 * Time.deltaTime);

                //  Cameraaimator.transform.LookAt(AIplayer.Playeranim.GetBoneTransform(HumanBodyBones.Head).transform);
                if (tempvalue > 1f)
                {
                    Vector3 dir = Player.Playeranim.GetBoneTransform(HumanBodyBones.Head).transform.position - Cameraaimator.transform.position;
                    dir.Normalize();
                    Cameraaimator.transform.rotation = Quaternion.Lerp(Cameraaimator.transform.rotation,
                                                            Quaternion.LookRotation(dir), 2 * Time.deltaTime);
                    // Cameraaimator.transform.LookAt(Player.Playeranim.GetBoneTransform(HumanBodyBones.Head).transform);
                }

            }

        if (Stopchecking)
        {
            Timervalue += Time.deltaTime;
            if (Timervalue >= 2)
            {
                if (Cameraaimator.enabled == false)
                {
                    Cameraaimator.enabled = true;
                }

                if (Cameraaimator)
                {
                    Cameraaimator.SetTrigger("Victory");
                }

                maincampos = Maincameraobj.transform.localPosition;


                if (Player.Isdead)
                {
                    Camerapoint.transform.position = AIplayer.transform.position;
                    maincampos.y = 10;
                    maincampos.x = -45f;
                    maincampos.z = -25;
                }
                else
                {
                    Camerapoint.transform.position = Player.transform.position;
                    maincampos.y = 10;
                    maincampos.x = -45f;
                    maincampos.z = 25;
                }
                Camerapoint.transform.Rotate(0, -1f, 0);



                Maincameraobj.transform.localPosition = Vector3.Lerp(Maincameraobj.transform.localPosition, maincampos, 3 * Time.deltaTime);


                maincamrot = Maincameraobj.transform.localEulerAngles;
                maincamrot.x = 8;
                Maincameraobj.transform.localRotation = Quaternion.Lerp(Maincameraobj.transform.localRotation, Quaternion.Euler(maincamrot), 3 * Time.deltaTime);

                Vector3 camerobjpos = Cameraaimator.transform.localPosition;
                camerobjpos.y = 0;
                Cameraaimator.transform.localPosition = Vector3.Lerp(Cameraaimator.transform.localPosition, camerobjpos, 2 * Time.deltaTime);

            }

            if (Playermeterobj.activeSelf)
            {
                Playermeterobj.SetActive(false);
            }
          
        }
    }
    private void LateUpdate()
    {
        if (Midpoint == null || Player==null || AIplayer==null )
            return;

        

        if (Stopchecking || Checkfinishmove)
            return;

        Timervalue = 0;
        if (Playerturn && Player.Isplayerready)
        {
            if(Playermeterobj.activeSelf==false && !Player.Isplayerperformedaction)
            {
                Playermeterobj.SetActive(true);
            }
            maincampos = Maincameraobj.transform.localPosition;
            maincampos.z = 7.5f;
            Maincameraobj.transform.localPosition = Vector3.Lerp(Maincameraobj.transform.localPosition, maincampos, 3 * Time.deltaTime);

            maincamrot = Maincameraobj.transform.localEulerAngles;
            maincamrot.y = 120;
            Maincameraobj.transform.localRotation = Quaternion.Lerp(Maincameraobj.transform.localRotation, Quaternion.Euler(maincamrot), 3 * Time.deltaTime);
        }
        if(AIplayerturn &&  AIplayer.Isplayerready )
        {
            if (Playermeterobj.activeSelf == true)
            {
                Playermeterobj.SetActive(false);                   

            }

            if (!Dontchangecamera)
            {
                maincampos = Maincameraobj.transform.localPosition;
                maincampos.z = -7.5f;
                Maincameraobj.transform.localPosition = Vector3.Lerp(Maincameraobj.transform.localPosition, maincampos, 3 * Time.deltaTime);

                maincamrot = Maincameraobj.transform.localEulerAngles;
                maincamrot.y = 60;
                Maincameraobj.transform.localRotation = Quaternion.Lerp(Maincameraobj.transform.localRotation, Quaternion.Euler(maincamrot), 3 * Time.deltaTime);
            }
        }


        Playerpos = Player.transform.position ;
        AIplayerpos = AIplayer.transform.position ;

        Camerposition = (Playerpos + AIplayerpos)/ 2f;
        Camerapoint.transform.position = Camerposition;     

        if(!Player.Isactionstarted)
        Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, Quaternion.LookRotation( Lookdirection(Player.transform, AIplayer.transform),Vector3.up), 5 * Time.deltaTime);

        if (!AIplayer.Isactionstarted)
        AIplayer.transform.rotation = Quaternion.Lerp(AIplayer.transform.rotation, Quaternion.LookRotation( Lookdirection(AIplayer.transform, Player.transform),Vector3.up), 5 * Time.deltaTime);

        Camerapoint.transform.rotation = Quaternion.Lerp(Camerapoint.transform.rotation, Quaternion.LookRotation(Lookdirection(Camerapoint.transform, Player.transform), Vector3.up), 5 * Time.deltaTime);

        offset = Playerpos - Midpoint.transform.position;          
        offset = Midpoint.transform.position + Vector3.ClampMagnitude(offset, Playersrange);
        Player.transform.position = offset;

        offset1 = AIplayerpos - Midpoint.transform.position; 
        offset1 = Midpoint.transform.position + Vector3.ClampMagnitude(offset1, Playersrange);
        AIplayer.transform.position = offset1;

        distancebetweenopponent = Vector3.Distance(Playerpos, AIplayerpos);

   
        Maincamerachildpos = Cameraaimator.transform.localPosition;
        diffvalue = distancebetweenopponent - Distancevalue;
        
        if(diffvalue>=0)
         Maincamerachildpos.z = diffvalue*-1;
        else
         Maincamerachildpos.z = 0;

        Maincamerachildpos.y =0f;
        Cameraaimator.transform.localPosition = Maincamerachildpos;
        if(Cameraaimator.enabled)
        {
            Cameraaimator.enabled = false;
        }
    }

    LayerMask Layermaskvalplayer;

    public void GetHiteffectforAIplayer()
    {
        Layermaskvalplayer = 1 << 9;
        Layermaskvalplayer = ~Layermaskvalplayer;
        Vector3 pos=Vector3.zero;
 
        Vector3 Playerleftfootpos = Player.Leftfoot.position;
        Vector3 Playerrighfootpos = Player.Rightfoot.position;
        if (Playerleftfootpos.y > Playerrighfootpos.y)
        {
           // Debug.Log(AIplayer.transform.position + " ...");

            if (Physics.Raycast(Playerleftfootpos, (AIplayer.transform.position+ Vector3.up* 1 ) -Playerleftfootpos, out hit, 10, Layermaskvalplayer))
            {
                pos = hit.point;              
            }        
        }
        else
        {
           // Debug.Log(AIplayer.transform.position + " ...");

            if (Physics.Raycast(Playerrighfootpos, (AIplayer.transform.position + Vector3.up * 1) - Playerrighfootpos, out hit, 10, Layermaskvalplayer))
            {
              pos = hit.point;
               
            }      
        }
        GameObject obj = _poolmanagerref.GetPoolobject(0);
        obj.transform.position = pos;
     
    }

    public void GetHiteffectforplayer()
    {
        Layermaskvalplayer = 1 << 10;
        Layermaskvalplayer = ~Layermaskvalplayer;
        Vector3 pos = Vector3.zero;
        Vector3 Playerleftfootpos = AIplayer.Leftfoot.position;
        Vector3 Playerrighfootpos = AIplayer.Rightfoot.position;
        if (Playerleftfootpos.y > Playerrighfootpos.y)
        {
           
            if (Physics.Raycast(Playerleftfootpos, (Player.transform.position + Vector3.up * 1) - Playerleftfootpos, out hit, 10, Layermaskvalplayer))
            {
                pos = hit.point;
            }
        }
        else
        {

            if (Physics.Raycast(Playerrighfootpos, (Player.transform.position + Vector3.up * 1) - Playerrighfootpos, out hit, 10, Layermaskvalplayer))
            {  
                pos = hit.point;
            }
        }
        GameObject obj = _poolmanagerref.GetPoolobject(0);
        obj.transform.position = pos;

        

    }

    public void Enablepowerobject(Vector3 position)
    {
        GameObject obj = _poolmanagerref.GetPoolobject(1);
        if (obj != null)
        {
            obj.transform.position = position;
            obj.gameObject.SetActive(true);
        }
    }

    public void Shakecam(int level=1)
    {
        Cameraaimator.SetTrigger("Shake"+level);
    }
    public float diffvalue;
    RaycastHit hit;
    float value=1f;
    Vector3 Lookdirection(Transform Reqiredobj,Transform Targetobj)
    {
        Vector3 angles = Reqiredobj.transform.eulerAngles;
        Vector3 temp = Targetobj.transform.position - Reqiredobj.transform.position;
        temp.Normalize();
        temp.y = 0;
        return temp;
        
    }

    public bool Isplayerawayformrange{get=> distancebetweenopponent > Distancevalue;}


}