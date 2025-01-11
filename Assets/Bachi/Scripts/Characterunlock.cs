using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Characterunlock : MonoBehaviour
{
    // Start is called before the first frame update


    public int Characterindexvalue = 0;

    [Space(10)]

    public bool Isavailable;
    public enum Unlocktype
    {
        Watchvideotype,
        Puchasewithcoins
    }
    public Unlocktype currentcharacterunlock;

    [Space(5)]

    public Image Currentcharicon;

    [Space(5)]

    public Sprite currentcharavatar;

    [Space(5)]

    public int Priceofcharacter;

 

    [Space(5)]

    public Text Charpricetext;

    [Space(5)]

    public GameObject Purchasesetup, Watchvideosetup;
    [Space(5)]

    public Characterscontainer currentcharactercontainer;

    public GameObject Selectbuttonobj, Watchvideobuttonobj;

    public Text Watchvideotext;

    public int Noofvideostounlock = 1;

    public bool Iswatchvideoseen;

    private void OnEnable()
    {
        Currentcharicon.sprite = currentcharavatar;

        //Debug.Log("Before "+ Characterindexvalue+ " :: " + Database.Getcharactervideostatus(Characterindexvalue)+"  Noof videos "+Noofvideostounlock);
        if (Database.Getcharactervideostatus(Characterindexvalue) == 0)
        {
            Database.Assignwatchvideovalue(Characterindexvalue, Noofvideostounlock);
        }


        if (Characterindexvalue == Database.Playerselectedindex)
        {
           
            Checkcharacterstatus();

        }
    }

    public void Checkcharacterstatus()
    {
        currentcharactercontainer.Currentcharacterref = this;
        Selectbuttonobj.SetActive(false);
        Watchvideobuttonobj.SetActive(false);
        //Debug.Log(Characterindexvalue+ " Char locked..." + Database.Getcharacterstatus(Characterindexvalue));
        if (Database.Getcharacterstatus(Characterindexvalue) == false)
        {
            //Debug.Log("Char locked...");
            if (currentcharacterunlock == Unlocktype.Puchasewithcoins)
            {
                Purchasesetup.SetActive(true);
            }
            else
            {
              //  Watchvideosetup.SetActive(true);
                Watchvideobuttonobj.SetActive(true);
               
              //  Debug.Log("Char video ::: " + Database.Getcharactervideostatus(Characterindexvalue));
              //  Debug.Log("After  " + Database.GetAllcharactersvideokey);

                if (Database.Getcharactervideostatus(Characterindexvalue) ==1)
                {
                    Watchvideotext.fontSize = 35;
                    Watchvideotext.text = "WATCH AN AD TO UNLOCK";

                }
                else
                {
                    Watchvideotext.fontSize = 45;
                    Watchvideotext.text = "    WATCH " + Database.Getcharactervideostatus(Characterindexvalue) + " ADS TO UNLOCK";

                }
            }


        }
        else
        {
            Selectbuttonobj.gameObject.SetActive(true);
            Selectbuttonfunction();
        }

        currentcharactercontainer.Showselectedchar(Characterindexvalue);

    }

    private void Update()
    {
        if(Iswatchvideoseen)
        {
            Iswatchvideoseen = false;

            Database.Addwatchvideovalue(Characterindexvalue);
            if (Database.Getcharactervideostatus(Characterindexvalue) <= 0)
            {
                Characterunlocked();

            }
            else
            {
                Checkcharacterstatus();
            }
        }
    }
    public void Unlockcharacter()
    {
        if (!Isavailable)
            return;

        Iswatchvideoseen = false;

        if (Database.Getcharacterstatus(Characterindexvalue) == false)
        {
            if (currentcharacterunlock == Unlocktype.Puchasewithcoins)
            {
                if (Database.Checkavailabiltyofcoins(Priceofcharacter))
                {
                    Database.Unlockcharacter(Characterindexvalue);
                    Database.Totalcoins -= Priceofcharacter;

                    Characterunlocked();
                }
                else
                {
                    Debug.Log("Cash unavailable");
                }
               
            }
            else
            {
                // Admanager video callback method
#if Adsetup_On

                if (AdManager.instance)
                    AdManager.instance.ShowRewardVideoWithCallback((result) =>
                    {
                        if (result)
                        {

                            Iswatchvideoseen = true;
                        }

                    });
#endif

#if UNITY_EDITOR


                          Database.Addwatchvideovalue(Characterindexvalue);
                        
                         if (Database.Getcharactervideostatus(Characterindexvalue) <= 0)
                        {
                            Characterunlocked();

                        }
                        else
                        {
                            Checkcharacterstatus();
                        }
#endif

            }
            

        }
        else
        {
           
        }
    }

    public void Selectbuttonfunction()
    {
        Database.Playerselectedindex = Characterindexvalue;
        currentcharactercontainer.Showselectedchar(Characterindexvalue);

    }
    void Characterunlocked()
    {
        Database.Unlockcharacter(Characterindexvalue);
        Selectbuttonobj.SetActive(true);
        Watchvideobuttonobj.SetActive(false);

        Purchasesetup.SetActive(false);
        Watchvideosetup.SetActive(false);

        Database.Playerselectedindex = Characterindexvalue;
        currentcharactercontainer.Showselectedchar(Characterindexvalue);
    }
}
