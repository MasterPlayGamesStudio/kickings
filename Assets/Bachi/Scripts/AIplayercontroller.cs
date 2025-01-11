using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIplayercontroller : Basescript
{
    private static AIplayercontroller _instance;

    public static AIplayercontroller Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AIplayercontroller();
            }
            return _instance;
        }
    }

    private AIplayercontroller()
    {
        

    }

    public Gamemanager Currentgamemanager;
    private Playersoundmanager Currentplayersoundmanager;
    public Leveldatahandler CurrentLeveldatahandler;
    public enum AIplayerLevel
    {
        Easy,
        Medium,
        Hard
    }

    public AIplayerLevel currentplayerlevel;

    private void Awake() => _instance = this;


    private void Start()
    {
        if (Gamemanager.Instance)
        {
            Currentgamemanager = Gamemanager.Instance;
        }
       
        
        Currentgamemanager.AIplayer = this;
        Currentgamemanager.Distancevalue = 5f;


        for (int i = 0; i < Allbodyparts.Length; i++)
        {
            Allbodyparts[i].gameObject.layer = 10;
        }


        base.Initialze();

        Healthfillimg = Currentgamemanager._UIcontrolref.AIplayerhealthbar;
        Healthvaluetext = Currentgamemanager._UIcontrolref.AIplayerheathtext;
        Healthvaluetext.text = Healthvalue.ToString();
        Currentgamemanager._UIcontrolref.AIplayerpicture.sprite = Playeravatar;


        Vector3 localscale = transform.localScale;
        localscale.x = -1;
        transform.localScale = localscale;


       
        CHeckhealthvalue(0);

        if (Playersoundmanager.Instance)
        {
            Currentplayersoundmanager = Playersoundmanager.Instance;
        }


    }
    float timervalue = 0;
    float actiontimervalue = 2;
    bool Performactionnow;
    public override void Update()
    {
        if (!Currentgamemanager.Isgamerunning)
            return;

        if (Currentgamemanager.AIplayerturn)
        {
            if (Isplayerready  && Currentgamemanager.Player.Isplayerready)
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


        if (!Isplayermoving && Isplayerready && Currentgamemanager.AIplayerturn == true && Isplayerperformedaction)
        {
            AIplayerturnfinished();
        }

        if (!Isplayerready || !Currentgamemanager.AIplayerturn || Isplayerperformedaction)
        {
            return;
        }

        timervalue += Time.deltaTime;
        if(timervalue>=actiontimervalue)
        {
            Performactionowmethod();
            timervalue = 0;
            actiontimervalue = Random.Range(1f, 2f);

        }

    }


    void Performactionowmethod()
    {
        Isplayerperformedaction = true;
        int randomnum = randomnumber;
        if(currentplayerlevel==AIplayerLevel.Easy)
        {
            if(randomnum<=30)
            {
                Highkick();
            }
            else if(randomnum>30 && randomnum<60)
            {
                Midkick();
            }
            else
            {
                Lowkick();
            }


        }

        if (currentplayerlevel == AIplayerLevel.Medium)
        {
            if (randomnum <= 50)
            {
                Highkick();
            }
            else if (randomnum > 50 && randomnum < 70)
            {
                Midkick();
            }
            else
            {
                Lowkick();
            }

        }
        if (currentplayerlevel == AIplayerLevel.Hard)
        {
            if (randomnum <= 60)
            {
                Highkick();
            }
            else if (randomnum > 60 && randomnum < 80)
            {
                Midkick();
            }
            else
            {
                Lowkick();
            }

        }

        Currentgamemanager._UIcontrolref.Disableincreasebuttons();
#if UNITY_EDITOR

        // Highkick();

#endif
    }

    private bool Onlyoncepowerkick, Onlyoncedefence;
    public override void Lowkick()
    {
        base.Lowkick();
        Currentgamemanager.Player.Applydamagetome = Mathf.CeilToInt(Initialhealthvalue / 5f);

        if (Onlyoncepowerkick)
            return;

        if(randomnumber>80)
        {
            Onlyoncepowerkick = true;
            EnablePowerattack();
        }
    }

    public override void Midkick()
    {
        base.Midkick();
        Currentgamemanager.Player.Applydamagetome = Mathf.CeilToInt(Initialhealthvalue / 4f);


        if (randomnumber > 70)
        {
            Onlyoncepowerkick = true;
            EnablePowerattack();
        }
    }

    public override void Highkick()
    {
        base.Highkick();
        Currentgamemanager.Player.Applydamagetome = Mathf.CeilToInt(Initialhealthvalue / 3.5f);


        if (randomnumber > 60)
        {
            Onlyoncepowerkick = true;
            EnablePowerattack();
        }
    }

    public override void Lowkickreaction()
    {
        base.Lowkickreaction();
        Currentplayersoundmanager.PlayHitsound(0);        
        StartCoroutine(Currentplayersoundmanager.PlayerHurtsound(this));


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
        if (Checkfunnyanimtrigger && Currentgamemanager.Isplayerawayformrange)
        {
           // Funnyactions();
            Checkfunnyanimtrigger = false;
        }
    }


    public override void CHeckhealthvalue(int damagevalue)
    {
        base.CHeckhealthvalue(damagevalue);
        Currentgamemanager._UIcontrolref.CheckAIPlayerhealthbar(Healthbarscalevalue,damagevalue);
    }

    public override void OnDeadmethod()
    {
   
        Gamemanager.Stopchecking = true;
        Currentgamemanager.Player.Invoke("Showvitoryanimatin",2);
        Currentgamemanager._UIcontrolref.HideplayersHealthsetups();
        Currentplayersoundmanager.Playerdeadsound();


    }

    public override void CheckHitanim()
    {
        if (Currentgamemanager)
        {

            Currentgamemanager.Player.Powerattackreceivevalue = Powerattackvalue;

            if (Highkickcount > 0)
            {
                Currentgamemanager.Player.Highkickreaction();
                Currentgamemanager.Shakecam(3);

            }
            else if (Midkickcount > 0)
            {
                Currentgamemanager.Player.Midkickreaction();
                Currentgamemanager.Shakecam(2);


            }
            else
            {
                Currentgamemanager.Player.Lowkickreaction();
                Currentgamemanager.Shakecam(1);


            }

            Currentgamemanager.GetHiteffectforplayer();
            Currentplayersoundmanager.CheckKickaction();


        }
        Checkfunnyanimtrigger = true;
    }

    public override void Resetall()
    {

        if (Highkickcount >= 0 || Lowkickcount >= 0 || Midkickcount >= 0)
        {
          
            CheckDistancebetweenopponent();

        }

        base.Resetall();

    }


    public override void CheckDistancebetweenopponent()
    {
        if (Gamemanager.Stopchecking)
            return;




        if (Currentgamemanager.Isplayerawayformrange)// if (Currentgamemanager.distancebetweenopponent > Currentgamemanager.Distancevalue)
        {
           
            //Playeranim.SetTrigger("Walk");
            Isplayermoving = true;
           

        }
        else
        {
          //  Playeranim.ResetTrigger("Walk");
          //  base.Resetall();
          //  CancelInvoke("CheckDistancebetweenopponent");
            Isplayermoving = false;
            //   Playerturnfinished();
        }


    }

    public override void Stopplayermovement()
    {
      

        Playeranim.ResetTrigger("Walk");       
        base.Resetall();
        CancelInvoke("CheckDistancebetweenopponent");
        Currentgamemanager.Playerismoving = false;
        Isplayermoving = false;
      
    }

    public override void Showvitoryanimatin() => base.Showvitoryanimatin();
    

    void AIplayerturnfinished()
    {
        Currentgamemanager.AIplayerturn = false;
        Currentgamemanager.Playerturn = true;

        Currentgamemanager.Player.Isplayerperformedaction = false;
       // Isplayerperformedaction = false;

        if(currentplayerlevel==AIplayerLevel.Easy)
        {
            if(randomnumber>80)
            {
                if (Onlyoncedefence)
                    return;
                Onlyoncedefence = true;
                EnablePowerdefense();
            }
        }


        if (currentplayerlevel == AIplayerLevel.Medium)
        {
            if (randomnumber > 70)
            {
                if (Onlyoncedefence)
                    return;
                Onlyoncedefence = true;
                EnablePowerdefense();
            }
        }

        if (currentplayerlevel == AIplayerLevel.Hard)
        {
            if (randomnumber > 60)
            {
                if (Onlyoncedefence)
                    return;
                Onlyoncedefence = true;
                EnablePowerdefense();
            }
        }
    }

    public override void EnablePowerattack()
    {
        for(int i=0;i<Fireeffects.Length;i++)
        {
            Fireeffects[i].SetActive(true);
        }
        Powerattackvalue = 2;

        Currentgamemanager.Enablepowerobject(transform.position);

    }


    public override void EnablePowerdefense()
    {
        if (Database.Levelsnumber <= 5)
            return;

        for (int i = 0; i < Defenceeffects.Length; i++)
        {
            Defenceeffects[i].SetActive(true);
        }
        Damagevaluefactor = 0.5f;

        Currentgamemanager.Enablepowerobject(transform.position);

    }

    public override void Fallsound()=> Currentplayersoundmanager.Playfallsound();
    
    public override void Playmovesound()=> Currentplayersoundmanager.Playmovesound();
    
    int randomnumber=> Random.Range(1, 100);
    
}
