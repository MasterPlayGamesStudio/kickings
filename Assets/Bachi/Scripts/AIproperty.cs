using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIproperty : Basescript
{
    private static AIproperty _instance;

    public static AIproperty Instance
    {
        get;private set;

    }


    public Gamemanager Currentgamemanager;
    private Playersoundmanager Currentplayersoundmanager;
    public Leveldatahandler CurrentLeveldatahandler;

    public GameObject[] Objectstodisable;


    private void Awake() => _instance = this;
  
    // Start is called before the first frame update
    void Start()
    {
        if (Gamemanager.Instance)
        {
            Currentgamemanager = Gamemanager.Instance;
        }



        Currentgamemanager.AIplayer = this;
        Currentgamemanager.Distancevalue = 6f;
        Currentgamemanager.Dontchangecamera = true;
        Currentgamemanager._UIcontrolref.Isbonuslevel = true;
        

        if(Currentgamemanager._UIcontrolref)
        {
            Currentgamemanager._UIcontrolref.Leftset.SetActive(false);
            Currentgamemanager._UIcontrolref.Powerdefensebutton.transform.localScale = Vector3.zero;
        }

        for (int i = 0; i < Allbodyparts.Length; i++)
        {
            Allbodyparts[i].gameObject.layer = 10;
        }


        base.Initialze();

        Healthfillimg = Currentgamemanager._UIcontrolref.AIplayerhealthbar;
        Healthvaluetext = Currentgamemanager._UIcontrolref.AIplayerheathtext;
        Healthvaluetext.text = Healthvalue.ToString();
        Currentgamemanager._UIcontrolref.AIplayerpicture.sprite = Playeravatar;





       
        CHeckhealthvalue(0);

        if (Playersoundmanager.Instance)
        {
            Currentplayersoundmanager = Playersoundmanager.Instance;
        }

    }


    public override void CheckDistancebetweenopponent()
    {
         
    }

    public override void CheckHitanim()
    {
         
    }

    public override void EnablePowerattack()
    {
         
    }

    public override void EnablePowerdefense()
    {
         
    }

    public override void OnDeadmethod()
    {
        if (Currentplayercollider)
            Currentplayercollider.enabled = true;

        Gamemanager.Stopchecking = true;
        Currentgamemanager.Player.Invoke("Showvitoryanimatin", 2);
        Currentgamemanager._UIcontrolref.HideplayersHealthsetups();
        Currentgamemanager._UIcontrolref.Bounsulevelstrip.SetActive(false);


        for (int i=0;i< Objectstodisable.Length;i++)
        {
            Objectstodisable[i].SetActive(false);
        }
    }

    public override void Stopplayermovement()
    {
         
    }

    public override void CHeckhealthvalue(int damagevalue)
    {
        base.CHeckhealthvalue(damagevalue);
        CancelInvoke("Applyforcenow");
       
        for (int i = 0; i < Allbodyparts.Length; i++)
        {
            Allbodyparts[i].AddForce(Vector3.up * Allbodyparts[i].mass * Forcevalue, ForceMode.Impulse);
            //Allbodyparts[i].AddForce(Camera.main.transform.forward * Random.Range(-1f,1f) * Allbodyparts[i].mass * 1f, ForceMode.Impulse);
            Allbodyparts[i].AddForce(new Vector3(Random.Range(-5,5), Random.Range(-5, 5),Random.Range(-5, 5)) * Forcevalue * Allbodyparts[i].mass * 1f, ForceMode.Impulse);

        }

        Currentgamemanager._UIcontrolref.CheckAIPlayerhealthbar(Healthbarscalevalue, damagevalue);
    }

    float timervalue = 0;
    float actiontimervalue = 1;
    bool Performactionnow;

    public override void Update()
    {

        // Debug.Log("Readey :: " + Isplayerready + " AI turn :: " + Currentgamemanager.AIplayerturn + " action :: " + Isplayerperformedaction);




        Isplayerready = true;


        if (!Isplayermoving && Isplayerready && Currentgamemanager.AIplayerturn == true && Isplayerperformedaction)
        {
            AIplayerturnfinished();
        }

        if (!Isplayerready || !Currentgamemanager.AIplayerturn || Isplayerperformedaction)
        {
            return;
        }

        timervalue += Time.deltaTime;
        if (timervalue >= actiontimervalue)
        {
            Performactionowmethod();
            timervalue = 0;
            actiontimervalue = 1;

        }

    }


    public override void Lowkickreaction()
    {
        base.Lowkickreaction();
        Playeranim.SetTrigger("Lowkick");

    }

    public override void Midkickreaction()
    {
        base.Midkickreaction();
        Playeranim.SetTrigger("Midkick");


    }

    public override void Highkickreaction()
    {
        base.Highkickreaction();
        Playeranim.SetTrigger("Highkick");


    }

    void Performactionowmethod()
    {
        Isplayerperformedaction = true;
    }

    void AIplayerturnfinished()
    {
        Currentgamemanager.AIplayerturn = false;
        Currentgamemanager.Playerturn = true;

        Currentgamemanager.Player.Isplayerperformedaction = false;
        // Isplayerperformedaction = false;
    }
}

    
