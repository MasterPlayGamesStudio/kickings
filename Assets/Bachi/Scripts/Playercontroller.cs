using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : Basescript
{
    // Start is called before the first frame update
    private static Playercontroller _instance;

    public static Playercontroller Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new Playercontroller();
            }
            return _instance;
        }
    }

    private Playercontroller()
    {

    }

    private void Awake()=>_instance = this;
    

    public Gamemanager Currentgamemanager;
    private Playersoundmanager Currentplayersoundmanager;
   
    private void Start()
    {
        if(Gamemanager.Instance)
        {
            Currentgamemanager = Gamemanager.Instance;
        }
        Currentgamemanager.Player = this;

        if(Currentgamemanager.Allplayers.Contains(this)==false)
        {
            Currentgamemanager.Allplayers.Add(this);
        }

        for (int i = 0; i < Allbodyparts.Length; i++)
        {
            Allbodyparts[i].gameObject.layer = 9;
        }


        base.Initialze();

        Healthfillimg = Currentgamemanager._UIcontrolref.Playerhealthbar;
        Healthvaluetext = Currentgamemanager._UIcontrolref.Playerhealthtext;
        Healthvaluetext.text = Healthvalue.ToString();
        Currentgamemanager._UIcontrolref.Playerpicture.sprite = Playeravatar;

       
        CHeckhealthvalue(0);


        if(Playersoundmanager.Instance)
        {
            Currentplayersoundmanager = Playersoundmanager.Instance;
        }

         #if UNITY_EDITOR

                // Healthvalue = 10000;

        #endif

    }

    private void OnEnable()
    {
        if (Gamemanager.Instance)
        {
            Currentgamemanager = Gamemanager.Instance;
        }
        if(Currentgamemanager)
        Currentgamemanager.Player = this;

       

    }
    private void OnDisable()
    {
        if (Currentgamemanager)
            Currentgamemanager.Player = null;
    }

    public override void Update()
    {

        if (!Currentgamemanager.Isgamerunning)
            return;


        if (Currentgamemanager.Playerturn)
        {
            if (Isplayerready  && Currentgamemanager.AIplayer.Isplayerready)
            {

                if (Currentgamemanager.Isplayerawayformrange)// if (Currentgamemanager.distancebetweenopponent > Currentgamemanager.Distancevalue)
                {
                    Playeranim.SetTrigger("Walk");
                    Isplayermoving = true;
                   
                }
                else
                {

                    if (Isplayermoving)
                    {
                        Stopplayermovement();
                        Isplayermoving = false;
                    }

                }
              

            }
            
        }
        if (Isplayermoving)
            return;

        if (!Isplayermoving &&  Isplayerready && Currentgamemanager.Playerturn==true && Isplayerperformedaction)
        {
            Playerturnfinished();
        }
    }

    public override void Lowkick()=>base.Lowkick();
    
    
    public override void Midkick()=> base.Midkick();

    
    public override void Highkick()=>base.Highkick();


    public override void Lowkickreaction()
    {
        base.Lowkickreaction();
        Currentplayersoundmanager.PlayHitsound(0);
        StartCoroutine( Currentplayersoundmanager.PlayerHurtsound(this));


    }

    public override void Midkickreaction()
    {
        base.Midkickreaction();
        Currentplayersoundmanager.PlayHitsound(1);
        StartCoroutine(Currentplayersoundmanager.PlayerHurtsound(this));


    }

    public override void Highkickreaction()
    {
        base.Highkickreaction();
        Currentplayersoundmanager.PlayHitsound(2);
        StartCoroutine(Currentplayersoundmanager.PlayerHurtsound(this));


    }

    public override void Endofaction()
    {
        base.Endofaction();
        if(Checkfunnyanimtrigger && Currentgamemanager.Isplayerawayformrange)
        {
            //Funnyactions();
            Checkfunnyanimtrigger = false;
        }
    }

    public override void CheckHitanim()
    {
   
       if(Currentgamemanager)
        {


            Currentgamemanager.AIplayer.Powerattackreceivevalue = Powerattackvalue;


            if (Highkickcount > 0)
            {
                Currentgamemanager.AIplayer.Highkickreaction();
                Currentgamemanager.Shakecam(3);

            }
            else if (Midkickcount > 0)
            {
                Currentgamemanager.AIplayer.Midkickreaction();
                Currentgamemanager.Shakecam(2);


            }
            else
            {
                Currentgamemanager.AIplayer.Lowkickreaction();
                Currentgamemanager.Shakecam(1);


            }

            Currentgamemanager.GetHiteffectforAIplayer();

            Currentplayersoundmanager.CheckKickaction();


        }

        Checkfunnyanimtrigger = true;
    }

    public override void Resetall()
    {

        if (Highkickcount >=0 || Lowkickcount>=0 || Midkickcount>=0)
        {
       
            CheckDistancebetweenopponent();
           

        }

        base.Resetall();
       

    }


    public override void CheckDistancebetweenopponent()
    {
        if (Gamemanager.Stopchecking)
            return;


        if(Currentgamemanager.Isplayerawayformrange)// if (Currentgamemanager.distancebetweenopponent > Currentgamemanager.Distancevalue)
        {
          
            //Playeranim.SetTrigger("Walk");
            Isplayermoving = true;
            //Funnyactions();
            
        }
        else
        {
          //  Playeranim.ResetTrigger("Walk");
          //  base.Resetall();
           // CancelInvoke("CheckDistancebetweenopponent");
            Isplayermoving = false;
         //   Playerturnfinished();
        }
    }

    public override void Stopplayermovement()
    {

   
        Playeranim.ResetTrigger("Walk");
        base.Resetall();       
        Currentgamemanager.Playerismoving = false;
        Isplayermoving = false;
       

    }
    public override void CHeckhealthvalue(int damagevalue)
    {
        base.CHeckhealthvalue(damagevalue);
        Currentgamemanager._UIcontrolref.CheckPlayerhealthbar(Healthbarscalevalue,damagevalue);
    }

    public override void OnDeadmethod()
    {
       
        Gamemanager.Stopchecking = true;
        Currentgamemanager.AIplayer.Invoke("Showvitoryanimatin", 2);
        Currentgamemanager._UIcontrolref.HideplayersHealthsetups();
        Currentplayersoundmanager.Playerdeadsound();


    }

    public override bool Checkprediemethod(int damagevalue)=>base.Checkprediemethod(damagevalue);    

    public override void Showvitoryanimatin()=> base.Showvitoryanimatin();
    

    void Playerturnfinished()
    {        

        Currentgamemanager.AIplayerturn = true;
        Currentgamemanager.Playerturn = false;
        Currentgamemanager.AIplayer.Isplayerperformedaction = false;
        Currentgamemanager._UIcontrolref.EnablepoweDefensebutton();
            
    }

    public override void EnablePowerattack()
    {
        for (int i = 0; i < Fireeffects.Length; i++)
        {
            Fireeffects[i].SetActive(true);
        }
        Powerattackvalue = 2;

        Currentgamemanager.Enablepowerobject(transform.position);
    }

    public override void EnablePowerdefense()
    {
        for (int i = 0; i < Defenceeffects.Length; i++)
        {
            Defenceeffects[i].SetActive(true);
        }
        Damagevaluefactor = 0.5f;

        Currentgamemanager.Enablepowerobject(transform.position);

    }

    public override void Fallsound()=>Currentplayersoundmanager.Playfallsound();
    
    public override void Playmovesound()=>Currentplayersoundmanager.Playmovesound();

    
}
