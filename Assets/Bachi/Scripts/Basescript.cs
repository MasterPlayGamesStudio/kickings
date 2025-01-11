using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public abstract class Basescript : MonoBehaviour
{


    #region Attributes

        public int Playerindexvalue = 0;
        public int Healthvalue=100;
        public int Initialhealthvalue;
        public int Powervalue = 0;
        public float Forcevalue=0.2f;
        public float Healthbarscalevalue;
        [HideInInspector]
        public Image Healthfillimg;
        [HideInInspector]
        public Text Healthvaluetext;
        public Sprite Playeravatar;

        public int Applydamagetootherplayer=20;
        public int Applydamagetome;
     
        public bool Isdead;
        public bool Isplayermoving;
        public bool Isplayerready;
        public bool Isplayerperformedaction;
        public bool Isactionstarted;
        public bool IsIdle;
        public bool Checkfunnyanimtrigger;

        public Animator Playeranim;

        [SerializeField]
        public Transform Leftfoot, Rightfoot;

        public GameObject[] Fireeffects,Defenceeffects;
        public enum Playertype
        {
        MalePlayer,Femaletype
        }

        public enum Playerphysique
        {
            Slim, Normal,Heavy
        }

        public Playerphysique Currentplayerphysique;
        public Playertype Currentplayertype;

        public Rigidbody Currentplayerrigidbody;
        public Rigidbody[] Allbodyparts;
        public CapsuleCollider Currentplayercollider;
        public float Noramlradius = 2f;
        public float Actionradius = 1.5f;
        [SerializeField]
        public int Highkickcount, Midkickcount, Lowkickcount;
        [SerializeField]
        public int Highkickreactioncount, Midkickreactioncount, Lowkickreactioncount;


        public Vector3 Hiteffectposition;
        public Vector3 Initialposition;
        public Vector3 Initialrotation;

        public int Powerattackvalue = 1;
        public float Damagevaluefactor = 1;
        public int Powerattackreceivevalue = 1;


    #endregion



    public virtual void Initialze()
    {
        Initialhealthvalue = Healthvalue;
        Healthbarscalevalue = 1;
        Enableragdollstatus(false);

        Highkickreactioncount = Midkickreactioncount = Lowkickreactioncount = -1;
        Highkickcount = Midkickcount = Lowkickcount = -1;
        Isplayermoving = false;
        Resetall();

        if (Playeranim)
        {
            Leftfoot = Playeranim.GetBoneTransform(HumanBodyBones.LeftFoot);
            Rightfoot = Playeranim.GetBoneTransform(HumanBodyBones.RightFoot);

        }
        Initialposition = transform.position;
        Initialrotation = transform.eulerAngles;
        Powerattackvalue = 1;
        Damagevaluefactor = 1;
        Powerattackreceivevalue = 1;

        Currentplayerrigidbody = GetComponent<Rigidbody>();
        Currentplayerrigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;



    }



    #region VirtualMethods


    public virtual void Endofaction()=> Resetall();
      
    

        public virtual void Lowkick()
        {
            if (Playeranim.enabled == false)
                return;
        
            Resetanimalues();       
            Lowkickcount = Random.Range(0, 4);
            Playeranim.SetInteger("Lowkickcount", Lowkickcount);
            Isplayerready = false;
            if(Currentplayercollider)
            Currentplayercollider.radius = Actionradius;
            Isactionstarted = true;

        }

        public virtual void Midkick()
        {
            if (Playeranim.enabled == false)
                return;
       
            Resetanimalues();

            Midkickcount = Random.Range(0, 17);

        if (Powerattackvalue != 1)
            Midkickcount = 14;

            Playeranim.SetInteger("Midkickcount", Midkickcount);
            Isplayerready = false;
        if (Currentplayercollider)
            Currentplayercollider.radius = Actionradius;
             Isactionstarted = true;

        // for power kick 14


    }

    public virtual void Highkick()
        {
            if (Playeranim.enabled == false)
                return;
    

            Resetanimalues();

            Highkickcount = Random.Range(0, 12);

        if (Powerattackvalue != 1)
            Highkickcount = 11;// Random.Range(8, 12);

            Playeranim.SetInteger("Highkickcount", Highkickcount);
            Isplayerready = false;
            Currentplayercollider.radius = Actionradius;
            Isactionstarted = true;

        // for power kick 8-11


        }

        public virtual void Funnyactions()
        {
            if (Playeranim && Playeranim.enabled == false)
                return;
            Resetanimalues();
            Playeranim.SetInteger("Idlecount", Random.Range(6,14));
   

        }

        public virtual void Resetall()
        {
                if (Playeranim && Playeranim.enabled == false)
                return;

                Resetanimalues();

                Playeranim.SetInteger("Idlecount", Random.Range(0, 6));
                Isplayerready = true;
                if (Currentplayercollider)
                Currentplayercollider.radius = Noramlradius;
                Isactionstarted = false;

                for(int i=0;i<Fireeffects.Length;i++)
                {
                Fireeffects[i].SetActive(false);
                }

                for (int i = 0; i < Defenceeffects.Length; i++)
                {
                Defenceeffects[i].SetActive(false);
                }

                Powerattackvalue = 1;
                Damagevaluefactor = 1;
                Powerattackreceivevalue = 1;
         }

        public virtual void Resetanimalues()
        {
            if (Playeranim && Playeranim.enabled == false)
                return;

             Playeranim.SetInteger("Idlecount", -1);

            Highkickcount = Midkickcount = Lowkickcount = -1;
            Playeranim.SetInteger("Highkickcount", Highkickcount);
            Playeranim.SetInteger("Midkickcount", Midkickcount);
            Playeranim.SetInteger("Lowkickcount", Lowkickcount);

            Highkickreactioncount = Midkickreactioncount = Lowkickreactioncount = -1;
            Playeranim.SetInteger("Lowkickhit", Lowkickreactioncount);
            Playeranim.SetInteger("Midkickhit", Midkickreactioncount);
            Playeranim.SetInteger("Highkickhit", Highkickreactioncount);

            Playeranim.SetInteger("Victory", -1);

        }

        public virtual void Lowkickreaction()
            {
                if (Playeranim.enabled == false)
                    return;
                Resetanimalues();
                Lowkickreactioncount = Random.Range(0, 7);  

                if(Powerattackreceivevalue!=1)
                Lowkickreactioncount = Random.Range(5, 7);

                Playeranim.SetInteger("Lowkickhit", Lowkickreactioncount);        

                CHeckhealthvalue(Applydamagetome);
                Isplayerready = false;

             //power attack reaction 5-7


        }


    public virtual void Midkickreaction()
        {
            if (Playeranim.enabled == false)
                return;
            Resetanimalues();
            Midkickreactioncount = Random.Range(0, 11);

            if (Powerattackreceivevalue != 1)
                Midkickreactioncount = 10;

            Playeranim.SetInteger("Midkickhit", Midkickreactioncount);
       

            CHeckhealthvalue(Applydamagetome);
            Isplayerready = false;

        //power attack reaction 10


    }

    public virtual void Highkickreaction()
        {
            if (Playeranim.enabled == false)
                return;

            Resetanimalues();
            Highkickreactioncount = Random.Range(0, 17);


            if (Powerattackreceivevalue != 1)
                Highkickreactioncount = Random.Range(11, 17);


            Playeranim.SetInteger("Highkickhit", Highkickreactioncount);
            CHeckhealthvalue(Applydamagetome);
            Isplayerready = false;

        //power attack reaction 11-17

        }

        public virtual void CHeckhealthvalue(int damagevalue)
        {
            if (Isdead)
                return;


            Healthvalue -= Mathf.CeilToInt( damagevalue* Damagevaluefactor);
            Healthbarscalevalue = (Healthvalue + 0f) / Initialhealthvalue;
            if (Healthvalue<=0)
            {
                Isdead = true;
              if(Playeranim)
                Playeranim.enabled = false;
            if (Currentplayercollider)
                Currentplayercollider.enabled = false;
                Physics.IgnoreLayerCollision(8, 8, true);
                Physics.IgnoreLayerCollision(8, 9, true);
                Physics.IgnoreLayerCollision(8, 10, true);
                Enableragdollstatus(true);
    

                for (int i=0;i<Allbodyparts.Length;i++)
                {                
                    Invoke("Applyforcenow", 0.1f);                
                }
                OnDeadmethod();
          

            }


        }

        public virtual bool Checkprediemethod(int damagevalue) => (Healthvalue - damagevalue) <= 0;
        

        void Applyforcenow()
        {
            for (int i = 0; i < Allbodyparts.Length; i++)
            {
                Allbodyparts[i].AddForce(Vector3.up * Allbodyparts[i].mass * Forcevalue, ForceMode.Impulse);
                Allbodyparts[i].AddForce(Camera.main.transform.forward * 1 * Allbodyparts[i].mass * 1f, ForceMode.Impulse);
            }
        }
        public virtual void Showvitoryanimatin()
        {
      
            Resetanimalues();

            if(Playeranim)
            Playeranim.SetInteger("Victory", Random.Range(0,4));
        }

        public virtual void Resetplayer()
        {
            Isdead = false;

            if(Playeranim)
            Playeranim.enabled = true;


            Physics.IgnoreLayerCollision(8, 8, false);
            Physics.IgnoreLayerCollision(8, 9, true);
            Physics.IgnoreLayerCollision(8, 10, true);

            Enableragdollstatus(false);
            Isplayermoving = false;
            Isplayerready = false;


        if (Currentplayercollider)
            Currentplayercollider.enabled = true;

        Resetall();

        }

        public virtual void Fallsound()
        {

        }

        public virtual void Playmovesound()
        {

        }

    #endregion

    #region Abstractmethods

    public abstract void OnDeadmethod();


        public abstract void CheckDistancebetweenopponent();


        public abstract void Stopplayermovement();


        public abstract void Update();

        public abstract void CheckHitanim();

        public abstract void EnablePowerattack();

        public abstract void EnablePowerdefense();

    #endregion

    void Enableragdollstatus(bool Status)
    {
       
        for (int i = 0; i < Allbodyparts.Length; i++)
        {
            Allbodyparts[i].useGravity = Status;
            Allbodyparts[i].isKinematic = !Status;

        }
    }

}
